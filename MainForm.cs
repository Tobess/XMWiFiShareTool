using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VirtualRouterPlus.Web;

namespace VirtualRouterPlus
{
    public partial class MainForm : Form
    {
        WlanManager wlanManager = new WlanManager();
        IcsManager icsManager = new IcsManager();

        bool isStarted;
        string SSID = "ZA_PRINTER";
        string PWD = "123456789";
        string identify = null;
        bool custom = false;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0 && args.Length <=3)
            {
                identify = args[0];
                if (args.Length > 1)
                {
                    SSID = args[1];
                    if (args.Length > 2)
                    {
                        PWD = args[2];
                    }
                }
                custom = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            wlanManager.HostedNetworkAvailable += wlanManager_HostedNetworkAvailable;
            wlanManager.StationJoin += wlanManager_StationJoin;

            RefreshConnection();

            ssidTextBox.Text = SSID;
            passwordTextBox.Text = PWD;

            if (custom)
            {
                startButton.PerformClick();
            }
        }

        void wlanManager_StationJoin(object sender, EventArgs e)
        {

        }

        void wlanManager_HostedNetworkAvailable(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            ssidTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            connectionComboBox.Enabled = false;
            refreshConnectionButton.Enabled = false;
            startButton.Text = "等待硬件响应中...";

            if (isStarted)
            {
                if (Stop())
                {
                    isStarted = false;
                    notifyIcon.ShowBalloonTip(5000, "成功", "成功停止无线虚拟路由器！", ToolTipIcon.Info);
                    if (custom)
                    {
                        try
                        {
                            RegistryKey key = Registry.CurrentUser;
                            RegistryKey rp = key.CreateSubKey("Software\\RemotePrinter");
                            rp.SetValue("RP_HOTSPOT", "");
                        }
                        catch
                        {
                            //
                        }
                    }
                }
                else
                {
                    notifyIcon.ShowBalloonTip(5000, "错误", "无法停止！", ToolTipIcon.Error);
                }
            }
            else
            {
                if (ValidateFields())
                {
                    if (Start(ssidTextBox.Text, passwordTextBox.Text, (IcsConnection)connectionComboBox.SelectedItem, 16))
                    {
                        isStarted = true;
                        WindowState = FormWindowState.Minimized;
                        notifyIcon.ShowBalloonTip(5000, "成功", "成功启动无线虚拟路由器！", ToolTipIcon.Info);

                        if (custom)
                        {
                            try
                            {
                                RegistryKey key = Registry.CurrentUser;
                                RegistryKey rp = key.CreateSubKey("Software\\RemotePrinter");
                                rp.SetValue("RP_HOTSPOT", identify.ToString());
                            }
                            catch
                            {
                                //
                            }
                        }
                    }
                    else
                    {
                        notifyIcon.ShowBalloonTip(5000, "错误", "无法启动虚拟无线路由器，未找到支持的硬件或无线网卡被禁用！", ToolTipIcon.Error);
                    }
                }
            }
            string AppDir = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            notifyIcon.Icon = new Icon(AppDir + (isStarted ? "VirtualRouterPlusStarted.ico" : "VirtualRouterPlusStopped.ico"));
            startButton.Text = isStarted ? "停止虚拟无线路由器" : "启动虚拟无线路由器";

            ssidTextBox.Enabled = !isStarted;
            passwordTextBox.Enabled = !isStarted;
            connectionComboBox.Enabled = !isStarted;
            refreshConnectionButton.Enabled = !isStarted;
            startButton.Enabled = true;
        }

        private bool Start(string ssid, string password, IcsConnection connection, int maxClients)
        {
            try
            {
                Stop();

                wlanManager.SetConnectionSettings(ssid, 32);
                wlanManager.SetSecondaryKey(password);

                wlanManager.StartHostedNetwork();

                var privateConnectionGuid = wlanManager.HostedNetworkInterfaceGuid;

                icsManager.EnableIcs(connection.Guid, privateConnectionGuid);

                DnsServerManager.Start();

                WebServer.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool Stop()
        {
            try
            {
                if (this.icsManager.SharingInstalled)
                {
                    this.icsManager.DisableIcsOnAll();
                }

                this.wlanManager.StopHostedNetwork();

                DnsServerManager.Stop();

                WebServer.Stop();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateFields()
        {
            if (ssidTextBox.Text.Length <= 0)
            {
                errorProvider.SetError(ssidTextBox, "无线名称 (SSID)必须填写！");
                return false;
            }

            if (ssidTextBox.Text.Length > 32)
            {
                errorProvider.SetError(ssidTextBox, "无线名称 (SSID)不能超过32个字符！");
                return false;
            }

            if (passwordTextBox.Text.Length < 8)
            {
                errorProvider.SetError(ssidTextBox, "无线密码至少为8位！");
                return false;
            }

            if (passwordTextBox.Text.Length > 64)
            {
                errorProvider.SetError(ssidTextBox, "无线密码不能超过46个字符！");
                return false;
            }

            return true;
        }

        private void refreshConnectionButton_Click(object sender, EventArgs e)
        {
            RefreshConnection();
        }

        private void RefreshConnection()
        {
            connectionComboBox.Items.Clear();

            foreach (var connection in icsManager.Connections)
            {
                if (connection.IsSupported)
                {
                    connectionComboBox.Items.Add(connection);
                }
            }

            var noSelected = true;
            for (var i = 0; i <= connectionComboBox.Items.Count; i++)
            {
                IcsConnection conn = (IcsConnection)connectionComboBox.Items[i];
                if (conn.Name.Contains("Ethernet"))
                {
                    connectionComboBox.SelectedIndex = i;
                    noSelected = false;
                    break;
                }
            }

            if (noSelected)
            {
                if (connectionComboBox.Items.Count > 0)
                {
                    connectionComboBox.SelectedIndex = (connectionComboBox.Items.Count - 1);
                }
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
    }
}

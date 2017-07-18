namespace VirtualRouterPlus
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ssidTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.ssidLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.connectionLabel = new System.Windows.Forms.Label();
            this.refreshConnectionButton = new System.Windows.Forms.Button();
            this.connectionComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ssidTextBox
            // 
            this.ssidTextBox.Location = new System.Drawing.Point(174, 38);
            this.ssidTextBox.Name = "ssidTextBox";
            this.ssidTextBox.Size = new System.Drawing.Size(197, 20);
            this.ssidTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(174, 73);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(197, 20);
            this.passwordTextBox.TabIndex = 1;
            // 
            // ssidLabel
            // 
            this.ssidLabel.AutoSize = true;
            this.ssidLabel.Location = new System.Drawing.Point(46, 41);
            this.ssidLabel.Name = "ssidLabel";
            this.ssidLabel.Size = new System.Drawing.Size(95, 13);
            this.ssidLabel.TabIndex = 2;
            this.ssidLabel.Text = "无线名称 (SSID): ";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(112, 76);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(37, 13);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "密码: ";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(104, 153);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(267, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "启动虚拟无线路由器";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Virtual Router Plus";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Location = new System.Drawing.Point(88, 113);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(61, 13);
            this.connectionLabel.TabIndex = 4;
            this.connectionLabel.Text = "共享网络: ";
            // 
            // refreshConnectionButton
            // 
            this.refreshConnectionButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("refreshConnectionButton.BackgroundImage")));
            this.refreshConnectionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.refreshConnectionButton.Location = new System.Drawing.Point(339, 103);
            this.refreshConnectionButton.Margin = new System.Windows.Forms.Padding(0);
            this.refreshConnectionButton.Name = "refreshConnectionButton";
            this.refreshConnectionButton.Size = new System.Drawing.Size(32, 33);
            this.refreshConnectionButton.TabIndex = 7;
            this.refreshConnectionButton.UseVisualStyleBackColor = true;
            this.refreshConnectionButton.Click += new System.EventHandler(this.refreshConnectionButton_Click);
            // 
            // connectionComboBox
            // 
            this.connectionComboBox.FormattingEnabled = true;
            this.connectionComboBox.Location = new System.Drawing.Point(174, 111);
            this.connectionComboBox.Name = "connectionComboBox";
            this.connectionComboBox.Size = new System.Drawing.Size(133, 21);
            this.connectionComboBox.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 222);
            this.Controls.Add(this.refreshConnectionButton);
            this.Controls.Add(this.connectionComboBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.ssidLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.ssidTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "迷你虚拟无线路由器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ssidTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label ssidLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button refreshConnectionButton;
        private System.Windows.Forms.ComboBox connectionComboBox;
        private System.Windows.Forms.Label connectionLabel;
    }
}


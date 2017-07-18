using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualRouterPlus.Web
{
    class WebServer
    {
        private static string[] prefixes = { "http://127.0.0.1/", "http://localhost/", "http://hotspot.me/" };
        private static HttpListener listener = null;
        private static bool _isRunning = false;

        public static void Start()
        {
            Stop();

            try
            {
                _isRunning = true;

                // 创建监听器.
                listener = new HttpListener();
                // 增加监听的前缀.
                foreach (string s in prefixes)
                {
                    listener.Prefixes.Add(s);
                }
                // 开始监听
                listener.Start();

                Thread t = new Thread(ListenFun);
                t.Start();
                
            }
            catch (SocketException)
            {
                Stop();
            }
            catch (FormatException)
            {
                Stop();
            }
            catch (Exception)
            {
                Stop();
            }
        }

        private static void ListenFun()
        {
            while (_isRunning)
            {
                try
                {
                    // 注意: GetContext 方法将阻塞线程，直到请求到达
                    HttpListenerContext context = listener.GetContext();
                    // 取得请求对象
                    HttpListenerRequest request = context.Request;

                    string method = request.HttpMethod;
                    string[] pathArr = request.RawUrl.Split('?');
                    string path = pathArr[0];
                    string query = pathArr.Length > 1 ? pathArr[1] : "";

                    // 取得回应对象
                    HttpListenerResponse response = context.Response;
                    string responseString = "";
                    if (path.Equals("/") && method.Equals("GET"))
                    {
                        string handle = "";
                        string headerUA = request.Headers.Get("User-Agent");
                        if (headerUA.Contains("iphone") || headerUA.Contains("ipad"))
                        {
                            handle = "fromai://remote-printer";
                        }
                        else if (headerUA.Contains("android"))
                        {
                            handle = "fromai://remote-printer";
                        }
                        // 构造回应内容
                        responseString = @"<html><head><title>Remote Printer Client</title></head><body><center><h1>请打开【金算大师】设置无线网络</h1></center>"+(handle.Length > 0 ? "<script>window.open('"+ handle + "')</script>" : "") +"</body></html>";
                        // 设置回应头部内容，长度，编码
                        response.ContentType = "text/html; charset=UTF-8";

                    }
                    else if (path.Equals("/") && method.Equals("POST"))
                    {
                        string ssid = request.QueryString.Get("ssid");
                        string password = request.QueryString.Get("password");

                        bool state = false;
                        string msg = "";
                        if (null != ssid && ssid.Length > 0 && null != password && password.Length > 0)
                        {
                            Result rst = WlanSetting.ScanSSIDAndConnect(ssid, password);
                            if (rst.state)
                            {
                                state = true;
                                msg = "wifi配置成功正在连接！";
                            }
                            else
                            {
                                msg = rst.msg;
                            }
                        }
                        else
                        {
                            msg = "SSID和密码不能为空！";
                        }

                        // 构造回应内容
                        responseString = "{\"state\":"+(state ? "true" : "false") +",\"message\":\""+ msg + "\"}";
                        // 设置回应头部内容，长度，编码
                        response.Headers.Add("Access-Control-Allow-Origin", "*");
                        response.ContentType = "application/json; charset=UTF-8";
                    }
                    else
                    {
                        response.StatusCode = 200;
                        responseString = "Not Found!" + request.RawUrl + request.Url;
                        response.ContentType = "text/html; charset=UTF-8";
                    }

                    response.ContentEncoding = Encoding.UTF8;
                    response.ContentLength64 = Encoding.UTF8.GetByteCount(responseString);

                    // 输出回应内容
                    System.IO.Stream output = response.OutputStream;
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(output);
                    writer.Write(responseString);

                    // 必须关闭输出流
                    writer.Close();
                }
                catch
                {
                }
            }

            Stop();
        }

        public static void Stop()
        {
            _isRunning = false;
            if (null != listener)
            {
                try
                {
                    listener.Stop();
                }
                catch
                {
                }
            }
        }

        public static bool isRunning()
        {
            return _isRunning.Equals(true);
        }
    }
}

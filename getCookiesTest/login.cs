using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using HtmlAgilityPack;


namespace getCookiesTest
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        //登录地址
        public static string loginurl = "https://xui.ptlogin2.qq.com/cgi-bin/xlogin?appid=716027609&daid=383&pt_no_auth=1&style=33&login_text=%E6%8E%88%E6%9D%83%E5%B9%B6%E7%99%BB%E5%BD%95&hide_title_bar=1&hide_border=1&target=self&s_url=https%3A%2F%2Fgraph.qq.com%2Foauth%2Flogin_jump&pt_3rd_aid=101174343&pt_feedback_link=http%3A%2F%2Fsupport.qq.com%2Fwrite.shtml%3Ffid%3D780%26SSTAG%3D90sheji.com.appid101174343";
        //登录成功后所需要验证的代码信息
        public static string incomeDetail = "xlogin";
        public static int px = 720;
        public static int py = 130;

        CookieCollection cc = new CookieCollection();

        ExtendedWebBrowser ieBrowser = new ExtendedWebBrowser();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Controls.Add(ieBrowser);
            ieBrowser.ScriptErrorsSuppressed = true;
            ieBrowser.BeforeNewWindow += new EventHandler<WebBrowserExtendedNavigatingEventArgs>(ieBrowser_BeforeNewWindow);
            ieBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(ieBrowser_DocumentCompleted);

            ieBrowser.Dock = DockStyle.Fill;

            ieBrowser.Navigate(loginurl);
            while (ieBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                System.Windows.Forms.Application.DoEvents();
                tools.Delay(100);
            }
        }
        void ieBrowser_BeforeNewWindow(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            ((ExtendedWebBrowser)sender).Navigate(e.Url);
        }

        private void ieBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
           // string taobaourl = "https://xui.ptlogin2.qq.com/cgi-bin/xlogin?appid=716027609&daid=383&pt_no_auth=1&style=33&login_text=%E6%8E%88%E6%9D%83%E5%B9%B6%E7%99%BB%E5%BD%95&hide_title_bar=1&hide_border=1&target=self&s_url=https%3A%2F%2Fgraph.qq.com%2Foauth%2Flogin_jump&pt_3rd_aid=101174343&pt_feedback_link=http%3A%2F%2Fsupport.qq.com%2Fwrite.shtml%3Ffid%3D780%26SSTAG%3D90sheji.com.appid101174343";
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
            ieBrowser.Document.Window.ScrollTo(px, py);
            //HtmlElement logindiv = ieBrowser.Document.GetElementById("login");
            //if (logindiv != null)
            //{

            //    px = logindiv.OffsetParent.OffsetRectangle.Right - 315;
            //    py = logindiv.OffsetParent.OffsetRectangle.Top + 100;
            //}

            //ieBrowser.Document.Window.ScrollTo(px, py);
            string str = GetCookies(loginurl);
            if (str.Length > 0)
            {
                string[] strs = str.Split(';');
                for (int i = 0; i < strs.Length; i++)
                {
                    try
                    {
                        Cookie ck = new Cookie();
                        ck.Name = strs[i].Split('=')[0].Trim();
                        ck.Value = strs[i].Split('=')[1].Trim();
                        ck.Domain = ieBrowser.Document.Domain;
                        cc.Add(ck);
                    }
                    catch
                    {

                    }
                }
            }
            frmMain.CC = cc;
            frmMain.CK = str;
            //getUrla("250");
            if (e.Url.ToString().Contains(incomeDetail))
            {
                this.Dispose();
            }
           // string str1 = GetCookies(taobaourl);
           // if (str1.Length > 0)
           // {
           //     string[] strs = str1.Split(';');
           //     for (int i = 0; i < strs.Length; i++)
           //     {
           //         if (str.Contains(strs[i]))
           //         {
           //             str1.Replace(strs[i], "");
           //             continue;
           //         }
           //         try
           //         {
           //             Cookie ck = new Cookie();
           //             ck.Name = strs[i].Split('=')[0].Trim();
           //             ck.Value = strs[i].Split('=')[1].Trim();
           //             //ck.Domain = ".taobao.com";
           //             ck.Domain = ".qq.com";
           //             cc.Add(ck);
           //         }
           //         catch
           //         {

           //         }
           //     }

           // }
           // frmMain.CC = cc;
           // frmMain.CK = str;
           //if (e.Url.ToString().Contains(incomeDetail) || e.Url.ToString().Contains("www.taobao.com"))
           // if (e.Url.ToString().Contains(incomeDetail) || e.Url.ToString().Contains("www.90sheji.com"))
           // {
           //     this.Dispose();
           // }
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData,
        ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int InternetSetCookieEx(string lpszURL, string lpszCookieName, string lpszCookieData, int dwFlags,
        IntPtr dwReserved);

        private static string GetCookies(string url)
        {
            uint datasize = 256;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;

                cookieData = new StringBuilder((int)datasize);
                if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }
    }
}

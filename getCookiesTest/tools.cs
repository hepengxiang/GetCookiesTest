using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using MODLE;




namespace getCookiesTest
{
    public class ExtendedWebBrowser : System.Windows.Forms.WebBrowser
    {
        System.Windows.Forms.AxHost.ConnectionPointCookie cookie;
        WebBrowserExtendedEvents events;

        //This method will be called to give you a chance to create your own event sink

        protected override void CreateSink()
        {
            //MAKE SURE TO CALL THE BASE or the normal events won't fire

            base.CreateSink();
            events = new WebBrowserExtendedEvents(this);
            cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(this.ActiveXInstance, events, typeof(DWebBrowserEvents2));
        }

        protected override void DetachSink()
        {
            if (null != cookie)
            {
                cookie.Disconnect();
                cookie = null;
            }
            base.DetachSink();
        }

        //This new event will fire when the page is navigating

        public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNavigate;
        public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNewWindow;

        protected void OnBeforeNewWindow(string url, out bool cancel)
        {
            EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNewWindow;
            WebBrowserExtendedNavigatingEventArgs args = new WebBrowserExtendedNavigatingEventArgs(url, null);
            if (null != h)
            {
                h(this, args);
            }
            cancel = args.Cancel;
        }

        protected void OnBeforeNavigate(string url, string frame, out bool cancel)
        {
            EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNavigate;
            WebBrowserExtendedNavigatingEventArgs args = new WebBrowserExtendedNavigatingEventArgs(url, frame);
            if (null != h)
            {
                h(this, args);
            }
            //Pass the cancellation chosen back out to the events

            cancel = args.Cancel;
        }
        //This class will capture events from the WebBrowser

        class WebBrowserExtendedEvents : System.Runtime.InteropServices.StandardOleMarshalObject, DWebBrowserEvents2
        {
            ExtendedWebBrowser _Browser;
            public WebBrowserExtendedEvents(ExtendedWebBrowser browser) { _Browser = browser; }

            //Implement whichever events you wish

            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
            {
                _Browser.OnBeforeNavigate((string)URL, (string)targetFrameName, out cancel);
            }

            public void NewWindow3(object pDisp, ref bool cancel, ref object flags, ref object URLContext, ref object URL)
            {
                _Browser.OnBeforeNewWindow((string)URL, out cancel);
            }

        }
        [System.Runtime.InteropServices.ComImport(), System.Runtime.InteropServices.Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
        System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch),
        System.Runtime.InteropServices.TypeLibType(System.Runtime.InteropServices.TypeLibTypeFlags.FHidden)]
        public interface DWebBrowserEvents2
        {

            [System.Runtime.InteropServices.DispId(250)]
            void BeforeNavigate2(
                [System.Runtime.InteropServices.In,
                System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In] ref object URL,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object targetFrameName, [System.Runtime.InteropServices.In] ref object postData,
                [System.Runtime.InteropServices.In] ref object headers,
                [System.Runtime.InteropServices.In,
                System.Runtime.InteropServices.Out] ref bool cancel);
            [System.Runtime.InteropServices.DispId(273)]
            void NewWindow3(
                [System.Runtime.InteropServices.In,
                System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.IDispatch)] object pDisp,
                [System.Runtime.InteropServices.In, System.Runtime.InteropServices.Out] ref bool cancel,
                [System.Runtime.InteropServices.In] ref object flags,
                [System.Runtime.InteropServices.In] ref object URLContext,
                [System.Runtime.InteropServices.In] ref object URL);

        }
    }
    public class WebBrowserExtendedNavigatingEventArgs : System.ComponentModel.CancelEventArgs
    {
        private string _Url;
        public string Url
        {
            get { return _Url; }
        }

        private string _Frame;
        public string Frame
        {
            get { return _Frame; }
        }

        public WebBrowserExtendedNavigatingEventArgs(string url, string frame)
            : base()
        {
            _Url = url;
            _Frame = frame;
        }
    }
    public partial class tools : Component
    {
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }
        //用于实现CookieContainer的复制操作
        private CookieContainer GetCookieContainerCopy(CookieContainer CC)
        {
            CookieContainer newCC = new CookieContainer();
            newCC.Capacity = CC.Capacity;
            newCC.MaxCookieSize = CC.MaxCookieSize;
            newCC.PerDomainCapacity = CC.PerDomainCapacity;
            newCC.Add(CC.GetCookies(new Uri("http://www.90sheji.com")));
            return newCC;
        }
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int InternetSetCookieEx(string lpszURL, string lpszCookieName, string lpszCookieData, int dwFlags, IntPtr dwReserved);
        public static string GetWebBrowsersCookieString(string url)
        {
            uint datasize = 256;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x2000000, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                cookieData = new StringBuilder((int)datasize);
                if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }

        public static string CreateWebBrowser(string url)
        {
            Rectangle screen = Screen.PrimaryScreen.Bounds;

            WebBrowser wb = new WebBrowser();
            wb.Width = screen.Width;
            wb.Height = screen.Height;
            wb.ScriptErrorsSuppressed = true;
            wb.ScrollBarsEnabled = true;

            // Console.WriteLine("正在加载页面...");
            wb.Navigate(url);

            //等待页面加载完毕
            while (wb.ReadyState != WebBrowserReadyState.Complete)
            {
                System.Windows.Forms.Application.DoEvents();
            }
            // Console.WriteLine("页面加载完成");

            //计算网页Document的宽和高
            Rectangle body = wb.Document.Body.ScrollRectangle;
            Size size = new Size(body.Width > screen.Width ? body.Width : screen.Width,
                 body.Height > screen.Height ? body.Height : screen.Height);
            wb.Width = size.Width;
            wb.Height = size.Height;
            string html = wb.Document.Body.InnerText;
            return html;
        }
        public static string GetHtmltextByWebClient(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据  
            Byte[] pageData = webClient.DownloadData(url);
            //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句         
            string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句  

            string htmlText = pageHtml;
            return htmlText;
        }
        public static void Delay(int DelayTime = 100)
        {
            int time = Environment.TickCount;
            while (true)
            {
                if (Environment.TickCount - time >= DelayTime)
                {
                    break;
                }
                Application.DoEvents();
                Thread.Sleep(10);
            }
        }
        //Url解码
        public static string MyUrlDeCode(string str)
        {
            Encoding encoding;

            Encoding utf8 = Encoding.UTF8;
            //首先用utf-8进行解码                    
            string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
            //将已经解码的字符再次进行编码.
            string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
            if (str == encode)
                encoding = Encoding.UTF8;
            else
                encoding = Encoding.GetEncoding("gb2312");

            return HttpUtility.UrlDecode(str, encoding);
        }

        //Url编码
        public static string MyUrlEnCode(string str)
        {
            return HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("GB2312")).ToUpper();
        }
    }
}


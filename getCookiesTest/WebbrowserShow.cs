using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using CsharpHttpHelper;
using System.Threading;

namespace getCookiesTest
{
    public partial class WebbrowserShow : Form
    {
        public WebbrowserShow()
        {
            InitializeComponent();
        }
        private static string answer = "";
        public void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //设置编码
            WebBrowser webBrowser = (WebBrowser)sender;

            if (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                return;
            if (e.Url.ToString() != webBrowser1.Url.ToString())
                return;

            //获取文档编码
            Encoding encoding = Encoding.GetEncoding(webBrowser.Document.Encoding);
            StreamReader stream = new StreamReader(webBrowser.DocumentStream, encoding);
            string htmlMessage = stream.ReadToEnd();

            webBrowser1.DocumentCompleted -= new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            this.timer1.Start();
            answer = getAnswer();//获取验证码答案
            if (answer == "")
            {
                sendEmail(frmMain.QQEmailStr);
                EmailWindowsShow.yzmState = "开始获取验证码";
            }
            else
                clickAnswer(answer);//执行答案点击事件
            
        }
        //获取答案
        public string getAnswer()
        {
            string answer = "";
            string cookiess = this.webBrowser1.Document.Cookie;
            string[] cookie = cookiess.Split(new char[] { ';' });
            foreach (string rempStr in cookie)
            {
                if (rempStr.Contains("answer="))
                {
                    answer = rempStr.Replace("answer=", "").Trim();
                    break;
                }
            }
            return answer;
        }
        //点击答案
        public void clickAnswer(string answer) 
        {
            //获取窗体相对于桌面的位置
            int locationX = this.Location.X;
            int locaiontY = this.Location.Y;
            //获取答案图标相对于webbrowser左上角的位置
            try 
            {
                HtmlElement ulTag = this.webBrowser1.Document.GetElementById("captcha");
                HtmlElementCollection aTags = ulTag.GetElementsByTagName("a");
                HtmlElement aTag = aTags[int.Parse(answer) - 1];
                Point temp = GetOffset(aTag);
                int clickPointx = temp.X + locationX + 25;//下偏移40像素
                int clickPointy = temp.Y + locaiontY + 50;//右偏移20像素
                Thread.Sleep(frmMain.ptyzminteval);//间隔时间秒后再点击
                MyClick(clickPointx, clickPointy);
            }
            catch { return; }
        }
        public void clickSubmit() 
        {
            HtmlElementCollection divTags = this.webBrowser1.Document.GetElementsByTagName("div");
            //获取窗体相对于桌面的位置
            int locationX = this.Location.X;
            int locaiontY = this.Location.Y;
            //获取答案图标相对于webbrowser左上角的位置
            foreach (HtmlElement submitTag in divTags) 
            {
                if (submitTag.InnerHtml == "提交") 
                {
                    Point temp = GetOffset(submitTag);
                    int clickPointx = temp.X + locationX + 25;//右偏移25像素
                    int clickPointy = temp.Y + locaiontY + 50;//下偏移50像素
                    MyClick(clickPointx, clickPointy);
                }
            }
        }
        /// <summary>
        /// 获取元素在窗体中坐标的位置
        /// </summary>
        /// <param name="el"></param>
        /// <returns></returns>
        public Point GetOffset(HtmlElement el)
        {
            Point pos = new Point(el.OffsetRectangle.Left, el.OffsetRectangle.Top);
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                pos.X += tempEl.OffsetRectangle.Left;
                pos.Y += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }
            return pos;
        }
        
        /// <summary>
        /// 鼠标点击mouse_event
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MyClick(int x, int y)//点击
        {
            SetCursorPos(x,y);//鼠标移至指定位置
            this.TopMost = true;//窗口置顶
            mouse_event(MouseEventFlag.LeftDown| MouseEventFlag.Absolute, x, y, 0, UIntPtr.Zero);//按下左键
            mouse_event(MouseEventFlag.LeftUp | MouseEventFlag.Absolute, x, y, 0, UIntPtr.Zero);//释放左键
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string downUrl = frmMain.detailQKHtmlText(this.webBrowser1.DocumentText);
            if (downUrl != "下载限制")
            {
                timer1.Enabled = false;
                frmMain.sleepState_qtyzm = false;
                frmMain.tempDownUrl = downUrl;
                this.Close();
            }
            if (EmailWindowsShow.yzmState == "破解邮箱验证码成功")
            {
                /*
                 * 1；拿到验证码
                 * 获取input输入框
                 * 2：将验证码放到输入框
                 * 3：点击提交
                 */
                HtmlElement inputTag = this.webBrowser1.Document.GetElementById("code");
                inputTag.SetAttribute("value", EmailWindowsShow.yzmStr);
                EmailWindowsShow.yzmStr = "";
                EmailWindowsShow.yzmState = "无需破解";
                clickSubmit();
            }
        }
        //破解普通验证码
        private void button1_Click(object sender, EventArgs e)
        {
            clickAnswer(answer);//执行答案点击事件
        }
        //破解邮箱验证码
        private void button2_Click(object sender, EventArgs e)
        {

        }
        
        public void clickSendEmail()
        {
            //获取窗体相对于桌面的位置
            int locationX = this.Location.X;
            int locaiontY = this.Location.Y;
            //获取答案图标相对于webbrowser左上角的位置
            try
            {
                HtmlElement ulTag = this.webBrowser1.Document.GetElementById("captcha");
                HtmlElementCollection aTags = ulTag.GetElementsByTagName("a");
                HtmlElement aTag = aTags[int.Parse(answer) - 1];
                Point temp = GetOffset(aTag);
                int clickPointx = temp.X + locationX + 25;//下偏移40像素
                int clickPointy = temp.Y + locaiontY + 50;//右偏移20像素
                MyClick(clickPointx, clickPointy);
            }
            catch { return; }
        }

        //发送验证码邮件，邮件发送成功就通知获取邮箱中的验证码
        public string sendEmail(string qq)
        {
            string url = "http://www.58pic.com/index.php?m=verify&a=index";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//改URL
                Host = "www.58pic.com",
                Cookie = this.webBrowser1.Document.Cookie,
                Method = "POST",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                //Referer = "http://90sheji.com/?m=Inspire&a=download&id=" + datakey,
                Postdata = "qq=" + qq + "&type=1",//"{\"id\":\"250\"}",//Post数据     可选项GET时不需要写  
                AutoRedirectCookie = true,
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            if (htmlText.Contains("ok"))
                return "发送成功";
            else
                return "发送失败";
        }
        //声明两个函数        
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);
        [Flags]
        enum MouseEventFlag : uint
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }
    }
}

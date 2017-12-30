using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.IO;
using System.Text.RegularExpressions;
using CsharpHttpHelper;

namespace getCookiesTest
{
    public partial class EmailWindowsShow : Form
    {
        public EmailWindowsShow()
        {
            InitializeComponent();
        }
        public static string yzmStr = "";
        public static string yzmState = "无需破解";
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
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
        }
        //点击验证码未读邮件
        private void getEmailYZM()   
        {
            string htmlText = this.webBrowser1.DocumentText;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlText);
            HtmlNode node = doc.DocumentNode;
            //未读取邮件
            var divTags = node.SelectNodes("//div[@class='maillist_listItem maillist_listItem_Unread']");
            if (divTags == null)
                return;
            var aTags = divTags[0].SelectNodes("//a[@class='maillist_listItemRight']");
            string href = "http://w.mail.qq.com"+aTags[0].Attributes["href"].Value;
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = href,
                Cookie = this.webBrowser1.Document.Cookie,
                Allowautoredirect = false,
            };
            HttpResult result = http.GetHtml(item);
            string yamHtmlText = result.Html;
            string verifyCode = "";
            verifyCode = new Regex(@"(?<=验证码：)\d{6}").Match(yamHtmlText).Value;
            if (verifyCode != "") 
            {
                yzmStr = verifyCode;
                yzmState = "破解邮箱验证码成功";
                //1：将页面回到原始界面
                this.webBrowser1.GoBack(); //后退
                //this.webBrowser1.Url = new Uri("http://w.mail.qq.com/cgi-bin/mail_list?fromsidebar=1&sid=lJXzoqHlV5B4BhRMz6DT6vN8,4,qQmNqMlFjNWNyVnFvMFB2NEt0QTFtdloqcTUwMkZ6N2o5eHQ4TW9iNVVNVV8.&folderid=1&page=0&pagesize=10&sorttype=time&t=mail_list&loc=today,,,151&version=html");
                //this.webBrowser1.Refresh();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (yzmState == "开始获取验证码")
            {
                yzmStr = "";
                this.webBrowser1.Document.ExecCommand("Refresh", false, null);
                getEmailYZM();
            }
        }
        //点击未读邮件
        private void button1_Click(object sender, EventArgs e)
        {
            //getEmailYZM();
            MessageBox.Show(yzmStr);
        }


        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using MODLE;
using System.Collections;
using System.Runtime.InteropServices;





namespace getCookiesTest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            
            InitializeComponent();
        }
        public static string CK = "";//登陆后cookie
        public static string flag = "正在下载";//默认cookie
        public static int thNum = 0;
        public static CookieCollection CC = new CookieCollection();//默认CookieCollection
        public static int pro1Value = 0;
        public static int pro1Max = 0;
        public static int pro3Value = 0;
        public static int pro3Max = 0;
        private static List<IpAdr> lstip = new List<IpAdr>();
        public static bool sleepState = false;//暂停续继续本次下载   true 暂停
        public static bool sleepState_qtyzm = false;
        private static bool exitState = false;//退出本次下载
        public static int ptyzminteval = 2000;
        //private static WebbrowserShow ws = new WebbrowserShow();
        //90设计登陆按钮
        private void button1_Click(object sender, EventArgs e)
        {
            CK = "";
            this.label7.Text = "千库网未登陆";
            this.label14.Text = "千图网未登录";
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label14.ForeColor = System.Drawing.Color.Black;
            //login.loginurl = "https://login.taobao.com/member/login.jhtml";
            login.loginurl = "http://90sheji.com/";
            //login.incomeDetail = "my_taobao.htm";
            login.incomeDetail = "https://xui.ptlogin2.qq.com/cgi-bin/xlogin?appid=716027609&daid=383&pt_no_auth=1&style=33&login_text=%E6%8E%88%E6%9D%83%E5%B9%B6%E7%99%BB%E5%BD%95&hide_title_bar=1&hide_border=1&target=self&s_url=https%3A%2F%2Fgraph.qq.com%2Foauth%2Flogin_jump&pt_3rd_aid=101174343&pt_feedback_link=http%3A%2F%2Fsupport.qq.com%2Fwrite.shtml%3Ffid%3D780%26SSTAG%3D90sheji.com.appid101174343";
            login.px = 0;
            login.py = 0;


            login lg = new login();
            lg.StartPosition = FormStartPosition.CenterParent;
            lg.Text = "登录90设计";
            lg.ShowDialog();
            if (CK.Contains("sns"))
            {
                this.label6.Text = "登录90设计成功";
                this.label6.ForeColor = System.Drawing.Color.Blue;
            }
            else 
            {
                this.label6.Text = "登录90设计失败";
                this.label6.ForeColor = System.Drawing.Color.Red;
            }
        }
        //千库网登陆按钮
        private void button2_Click(object sender, EventArgs e)
        {
            CK = "";
            this.label6.Text = "90设计未登陆";
            this.label14.Text = "千图网未登录";
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label14.ForeColor = System.Drawing.Color.Black;
            login.loginurl = "http://588ku.com/";
            login.incomeDetail = "https://graph.qq.com/oauth/show?which=Login&display=pc&client_id=101252414&redirect_uri=http%3A%2F%2F588ku.com%2Fdlogin%2Fcallback%2Fqq&response_type=code&scope=get_user_info%2Cadd_share%2Cadd_pic_t";
            login.px = 0;
            login.py = 0;


            login lg = new login();
            lg.StartPosition = FormStartPosition.CenterParent;
            lg.Text = "登录千库网";
            lg.ShowDialog();
            if (CK.Contains("sns"))
            {
                this.label7.Text = "登录千库网成功";
                this.label7.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                this.label7.Text = "登录千库网失败";
                this.label7.ForeColor = System.Drawing.Color.Red;
            }
        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                textBox1.Text = folderBrowserDialog1.SelectedPath;

            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {

                textBox5.Text = folderBrowserDialog2.SelectedPath;

            }
        }
        //千图网登陆
        private void button7_Click(object sender, EventArgs e)
        {
            CK = "";
            this.label6.Text = "90设计未登陆";
            this.label7.Text = "千库网未登陆";
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label7.ForeColor = System.Drawing.Color.Black;
            login.loginurl = "http://58pic.com/";
            login.incomeDetail = "https://graph.qq.com/oauth/show?which=Login&display=pc&client_id=100414805&redirect_uri=http%3A%2F%2Fwww.58pic.com%2Findex.php%3Fm%3Dlogin%26a%3Dcallback%26type%3Dqq&response_type=code&scope=get_user_info%2Cadd_share%2Cadd_pic_t";
            login.px = 0;
            login.py = 0;


            login lg = new login();
            lg.StartPosition = FormStartPosition.CenterParent;
            lg.Text = "登录千图网";
            lg.ShowDialog();
            if (CK.Contains("auth_id"))
            {
                this.label14.Text = "登录千图网成功";
                this.label14.ForeColor = System.Drawing.Color.Blue;
                
            }
            else
            {
                this.label14.Text = "登录千图网失败";
                this.label14.ForeColor = System.Drawing.Color.Red;
            }
        }
        //千图网保存地址
        private void button9_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox14.Text = folderBrowserDialog3.SelectedPath;
            }
        }




        //千图网下载
        private void button8_Click(object sender, EventArgs e)
        {
            sleepState = false;//不暂停
            exitState = false;//不退出
            this.dataGridView3.DataSource = null;
            if (!CK.Contains("auth_id"))
            {
                MessageBox.Show("请先登录千图网");
                //return;
            }
            if (this.textBox10.Text == "")
            {
                MessageBox.Show("请输入页面连接");
                return;
            }
            if (this.textBox14.Text == "")
            {
                MessageBox.Show("请选择保存地址");
                return;
            }
            if (this.textBox11.Text == "" || this.textBox12.Text == "")
            {
                MessageBox.Show("请输入起始页和结束页");
                return;
            }
            if (this.textBox13.Text == "")
            {
                MessageBox.Show("请输入名称号码");
                return;
            }
            if (this.textBox26.Text.Trim() == "") 
            {
                MessageBox.Show("请先在弹出邮箱上方输入QQ号码！");
                return;
            }
            if (this.textBox27.Text.Trim() == "")
            {
                MessageBox.Show("请输入破解验证码间隔时间，1000为1秒！");
                return;
            }
            frmMain.ptyzminteval = int.Parse(this.textBox27.Text.Trim());
            int minPage = int.Parse(this.textBox11.Text);
            int maxPage = int.Parse(this.textBox12.Text);
            if (minPage > maxPage)
            {
                MessageBox.Show("起始页和终止页不符合要求");
                return;
            }
            string downUrl = this.textBox20.Text;
            string saveUrl = this.textBox14.Text;
            int backnum = int.Parse(this.textBox13.Text.Substring(1, this.textBox13.Text.Length - 1));
            string backchar = this.textBox13.Text.Substring(0, 1);
            this.progressBar3.Value = 0;
            this.progressBar3.Step = 1;
            this.progressBar3.Minimum = 0;

            Dictionary<string, string> IDictionary = new Dictionary<string, string>();
            int[] pageNum = new int[5];
            int page = 0;
            while (minPage <= maxPage)
            {
                string pageMess = getQTPageMess(this.textBox10.Text + minPage.ToString() + ".html");
                if (pageMess == "")
                {
                    MessageBox.Show("获取第" + minPage + "页面信息失败");
                    break;
                }
                pageNum[page] = getQTTitleAndID(pageMess,IDictionary);
                minPage++;
                page++;
            }
            //将数组指针移至首位
            page = 0;
            minPage = int.Parse(this.textBox11.Text);
            this.progressBar3.Maximum = IDictionary.Count;
            frmMain.pro3Max = IDictionary.Count;
            this.dataGridView3.Rows.Clear();
            if (IDictionary.Count == 0)
            {
                MessageBox.Show("此页不能解析，请更换");
                return;
            }
            this.dataGridView3.Rows.Add(IDictionary.Count);
            int rowCountda = 0;
            int tem = 0;
            foreach (var item in IDictionary)
            {
                this.dataGridView3.Rows[rowCountda].Cells[0].Value = minPage.ToString();
                this.dataGridView3.Rows[rowCountda].Cells[1].Value = backchar + backnum.ToString() + "-" + item.Value;
                this.dataGridView3.Rows[rowCountda].Cells[2].Value = "";
                this.dataGridView3.Rows[rowCountda].Cells[3].Value = "";
                this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = Color.Black;
                rowCountda++;
                //名称数字加一
                backnum++;
                tem++;
                if (tem == pageNum[page])
                {
                    minPage++;
                    page++;
                    tem = 0;
                }
            }
            backnum = int.Parse(this.textBox13.Text.Substring(1, this.textBox13.Text.Length - 1));
            //异步调用thread1Sharp控制下载的主线程
            ThreadQTSharp threadQTSharp = threadQT;
            IAsyncResult thread1SharpResult = threadQTSharp.BeginInvoke(backchar, backnum, saveUrl, IDictionary,int.Parse(this.textBox22.Text), null, null);
            
        }
        //控制千图下载的主线程
        public delegate void ThreadQTSharp(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary, int sleepTime);
        public void threadQT(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary, int sleepTime)
        {
            string dowUrl = "";
            int rowCountda = -1;
            foreach (var item in IDictionary)
            {
                rowCountda++;

                this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "正在获取下载链接~");//"获取中~~~"
                this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Blue);
                //千图核心方法
                dowUrl = getQTDownUrl(item.Key,frmMain.CK);

                this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[2].Value = s; }), dowUrl);

                while (dowUrl == "下载限制")
                {
                    this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "下载限制");
                    this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Red);
                    //MessageBox.Show("下载限制，程序已自动暂停，等待一重验证码破解");

                    tempDownUrl = item.Key;
                    this.button14.Invoke(new Action<string>(s => { yzmWindowsShow(); }), "");
                    //yzmWindowsShow();

                    sleepState_qtyzm = true;
                    while (true)
                    {
                        if (sleepState_qtyzm)//暂停
                        {
                            continue;
                        }
                        else //继续
                        {
                            break;
                        }
                    }
                    this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "正在获取下载链接~");//"获取中~~~"
                    this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Blue);
                    dowUrl = tempDownUrl;
                    this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[2].Value = s; }), dowUrl);
                }

                if (dowUrl == "失败")
                {

                    //更新下载状态
                    if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "获取下载链接失败");//"下载失败"
                        this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Orange);

                    }
                    //更新进度条
                    if (this.progressBar3.InvokeRequired)//找到创建此控件的线程
                    {
                        frmMain.pro3Value++;
                        this.progressBar3.Invoke(new Action<int>(s => { this.progressBar3.Value = s; }), frmMain.pro3Value);
                        if (frmMain.pro3Value == frmMain.pro3Max)
                        {
                            MessageBox.Show("下载完成");
                        }
                    }
                }
                else
                {
                    string backname = ".zip";
                    if (dowUrl.Contains("rar?"))
                        backname = ".rar";
                    if (dowUrl.Contains("zip?"))
                        backname = ".zip";
                    if (dowUrl.Contains("psd?"))
                        backname = ".psd";
                    if (dowUrl.Contains("jpg?"))
                        backname = ".jpg";
                    if (dowUrl.Contains("png?"))
                        backname = ".png";

                    double milTime = downAndTimeQT(dowUrl, saveUrl, backchar + backnum.ToString() + "-" + item.Value + backname, rowCountda);
                    if (milTime < sleepTime)
                    {
                        Thread.Sleep((int)(sleepTime - milTime));
                    }

                }
                //每次遍历一个就将数字加一
                backnum++;
                while (true)
                {
                    if (exitState)//退出
                    {
                        IDictionary.Clear();
                        if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView3.Invoke(new MethodInvoker(() => this.dataGridView3.Rows.Clear()));
                        }
                        return;
                    }
                    if (sleepState)//暂停
                    {
                        continue;
                    }
                    else //继续
                    {
                        break;
                    }
                }

            }
        }
        //获取千图页面源码
        private string getQTPageMess(string qtPageUrl)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = qtPageUrl,
                Allowautoredirect = false,
                Cookie = frmMain.CK
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            return htmlText;
        }
        //千图网解析方法。获取千图网页面的标题和ID
        private int getQTTitleAndID(string pageMess, Dictionary<string, string> IDictionary)
        {
            int count = 0;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageMess);
            HtmlNode node = doc.DocumentNode;
            if (this.textBox10.Text.Contains("haibao啊啊啊啊啊啊"))//此处会多出下载两个字
            {
                var divs = node.SelectNodes("//div[@class='flow-info2 nounderline']");
                for (int i = 0; i < divs.Count; i++)
                {
                    var aTags = divs[i].ChildNodes[0].ChildNodes[0];
                    string href = aTags.Attributes["href"].Value;
                    string title = aTags.Attributes["title"].Value;
                    string[] ids = href.Split(new char[] { '/' });
                    string id = ids[ids.Length - 1];
                    id = id.Replace(".html", "");

                    href = "http://www.58pic.com/index.php?m=show&a=download&id=" + id;
                    try
                    {
                        IDictionary.Add(href, title);
                        count++;
                    }
                    catch
                    {

                    }
                }
            }

            else//网页UI使用本解析
            {
                var aTags = node.SelectNodes("//a[@class='thumb-box']");
                for (int i = 0; i < aTags.Count; i++)
                {
                    string href = aTags[i].Attributes["href"].Value;
                    string title = aTags[i].ChildNodes[0].Attributes["title"].Value;
                    string[] ids = href.Split(new char[] { '/' });
                    string id = ids[ids.Length - 1];
                    id = id.Replace(".html", "");

                    href = "http://www.58pic.com/index.php?m=show&a=download&id=" + id;
                    try
                    {
                        IDictionary.Add(href, title);
                        count++;
                    }
                    catch
                    {

                    }
                }
            }
            return count;
        }
        //获取千图下载链接所在页面的源码
        private string getQTDownUrl(string downUrlStr, string cookies)
        {
            CookieContainer ceer = new CookieContainer();
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = downUrlStr,
                Host = "www.58pic.com",
                Cookie = cookies,
                Method = "GET",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                AutoRedirectCookie = true,
                ContentType = "application/x-www-form-urlencoded",
                KeepAlive = true
            };
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Header.Add("ContentLength", "6");
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            string downUrl = detailQKHtmlText(htmlText);
            return downUrl;
        }
        //取出千图下载链接
        public static string detailQKHtmlText(string htmlText)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlText);
            HtmlNode node = doc.DocumentNode;
            string href = "";
            var lisTags = node.SelectNodes("//li[@class='sb_li']");
            if (lisTags == null)
               return "下载限制";
            foreach (var liTags in lisTags)
            {
                try { href = liTags.ChildNodes[0].Attributes["href"].Value; }
                catch { href = ""; }
                if (href == "")
                    continue;
                else
                    break;
            }
            if (!href.Contains("proxy"))
                return "下载限制";
            return href;
        }
        //返回下载千图图片所用时间的下载函数
        public double downAndTimeQT(string downUrl, string saveUrl, string saveName, int rowCountda)
        {
            DateTime a = DateTime.Now;
            downFileallQT(downUrl, saveUrl, saveName, rowCountda);
            DateTime b = DateTime.Now;
            TimeSpan c = b - a;
            return c.TotalMilliseconds;
        }
        //千图下载方法，携带更新进度条，跟新下载状态
        public void downFileallQT(string downUrl, string saveUrl, string saveName, int rowCountda)
        {
            {
                //打开上次下载的文件
                //long SPosition = 0;
                //实例化流对象
                saveName = saveName.Replace("/", "");
                saveName = saveName.Replace("*", "");
                saveName = saveName.Replace("\\", "");
                saveName = saveName.Replace("+", "");
                //判断要下载的文件是否存在
                if (File.Exists(saveUrl + "\\" + saveName))
                {
                    //更新下载状态
                    if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "文件已存在");
                        this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Yellow);
                    }
                    //更新进度条
                    if (this.progressBar3.InvokeRequired)//找到创建此控件的线程
                    {
                        frmMain.pro3Value++;
                        this.progressBar3.Invoke(new Action<int>(s => { this.progressBar3.Value = s; }), frmMain.pro3Value);
                        if (frmMain.pro3Value == frmMain.pro3Max)
                        {
                            MessageBox.Show("下载完成");
                        }
                    }
                }
                else
                {
                    //更新下载状态
                    if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "下载中~~~");
                        this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.YellowGreen);
                    }
                    FileStream FStream;
                    FStream = new FileStream(saveUrl + "\\" + saveName, FileMode.Create);
                    try
                    {
                        //打开网络连接
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(downUrl);
                        //if (SPosition > 0)
                        //    myRequest.AddRange((int)SPosition);             //设置Range值
                        //向服务器请求,获得服务器的回应数据流
                        Stream myStream = myRequest.GetResponse().GetResponseStream();
                        //定义一个字节数据
                        byte[] btContent = new byte[512];
                        int intSize = 0;
                        intSize = myStream.Read(btContent, 0, 512);
                        //finishedsize = intSize;
                        while (intSize > 0)
                        {
                            FStream.Write(btContent, 0, intSize);
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize += intSize;
                        }
                        //关闭流
                        FStream.Close();
                        myStream.Close();
                        //更新下载状态
                        if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                            this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                        }
                        //更新进度条
                        if (this.progressBar3.InvokeRequired)//找到创建此控件的线程
                        {
                            frmMain.pro3Value++;
                            this.progressBar3.Invoke(new Action<int>(s => { this.progressBar3.Value = s; }), frmMain.pro3Value);
                            if (frmMain.pro3Value == frmMain.pro3Max)
                            {
                                MessageBox.Show("下载完成");
                            }
                        }
                    }
                    catch 
                    {
                        //第一次打开链接失败，就重新再试一次！
                        try
                        {
                            //打开网络连接
                            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(downUrl);
                            //if (SPosition > 0)
                            //    myRequest.AddRange((int)SPosition);             //设置Range值
                            //向服务器请求,获得服务器的回应数据流
                            Stream myStream = myRequest.GetResponse().GetResponseStream();
                            //定义一个字节数据
                            byte[] btContent = new byte[512];
                            int intSize = 0;
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize = intSize;
                            while (intSize > 0)
                            {
                                FStream.Write(btContent, 0, intSize);
                                intSize = myStream.Read(btContent, 0, 512);
                                //finishedsize += intSize;
                            }
                            //关闭流
                            FStream.Close();
                            myStream.Close();
                            //更新下载状态
                            if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                            {
                                this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                                this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                            }
                            //更新进度条
                            if (this.progressBar3.InvokeRequired)//找到创建此控件的线程
                            {
                                frmMain.pro3Value++;
                                this.progressBar3.Invoke(new Action<int>(s => { this.progressBar3.Value = s; }), frmMain.pro3Value);
                                if (frmMain.pro3Value == frmMain.pro3Max)
                                {
                                    MessageBox.Show("下载完成");
                                }
                            }
                        }
                        catch 
                        {
                            FStream.Close();
                            //更新下载状态
                            if (this.dataGridView3.InvokeRequired)//找到创建此控件的线程
                            {
                                this.dataGridView3.Invoke(new Action<string>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Value = s; }), "访问链接出错");
                                this.dataGridView3.Invoke(new Action<Color>(s => { this.dataGridView3.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Red);
                            }
                            //更新进度条
                            if (this.progressBar3.InvokeRequired)//找到创建此控件的线程
                            {
                                frmMain.pro3Value++;
                                this.progressBar3.Invoke(new Action<int>(s => { this.progressBar3.Value = s; }), frmMain.pro3Value);
                                if (frmMain.pro3Value == frmMain.pro3Max)
                                {
                                    MessageBox.Show("下载完成");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static string tempDownUrl = "";

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);
        Point p;
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetCursorPos(out p);
            label3.Text = "X坐标" + p.X.ToString() + "---" + p.Y.ToString();//Y坐标
        }
        public static EmailWindowsShow ews;
        private void button14_Click(object sender, EventArgs e)//弹出邮箱
        {
            if (this.textBox26.Text.Trim() == "") 
            {
                MessageBox.Show("请先在弹出邮箱上方输入QQ号码！");
                return;
            }
            QQEmailStr = this.textBox26.Text.Trim();
            ews = new EmailWindowsShow();
            ews.webBrowser1.Url = new Uri("http://w.mail.qq.com/cgi-bin/mail_list?fromsidebar=1&sid=lJXzoqHlV5B4BhRMz6DT6vN8,4,qQmNqMlFjNWNyVnFvMFB2NEt0QTFtdloqcTUwMkZ6N2o5eHQ4TW9iNVVNVV8.&folderid=1&page=0&pagesize=10&sorttype=time&t=mail_list&loc=today,,,151&version=html");
            ews.Show();
            ews.Refresh();
        }
        public static string QQEmailStr = "";
        private void yzmWindowsShow() 
        {
            WebbrowserShow yamws = new WebbrowserShow();
            yamws.webBrowser1.Url = new Uri(tempDownUrl);
            yamws.Show();
            yamws.Refresh();
        }

        



        //千库网下载
        private void button6_Click(object sender, EventArgs e)
        {
            sleepState = false;//不暂停
            exitState = false;//不退出
            this.dataGridView2.DataSource = null;
            frmMain.pro1Value = 0;
            frmMain.thNum = 0;
            if (!CK.Contains("sns"))
            {
                MessageBox.Show("请先登录千库网");
                return;
            }
            if(this.textBox4.Text=="")
            {
                MessageBox.Show("请输入页面连接");
                return;
            }
            if (this.textBox5.Text == "")
            {
                MessageBox.Show("请选择保存地址");
                return;
            }
            if (this.textBox6.Text == "" || this.textBox7.Text == "")
            {
                MessageBox.Show("请输入起始页和结束页");
                return;
            }
            if (this.textBox8.Text == "")
            {
                MessageBox.Show("请输入名称号码");
                return;
            }
            if (this.textBox25.Text == "")
            {
                MessageBox.Show("请输入代理网站后面的数字");
                return;
            }
            //取出所有输入框中输入的数值
            int minPage = int.Parse(this.textBox6.Text);
            int maxPage = int.Parse(this.textBox7.Text);
            int backnum = int.Parse(this.textBox8.Text.Substring(1, this.textBox8.Text.Length - 1));
            string backchar = this.textBox8.Text.Substring(0, 1);
            string saveUrl = this.textBox5.Text;
            if (minPage > maxPage)
            {
                MessageBox.Show("起始页和终止页不符合要求");
                return;
            }
            //获取代理网站数据
            if (this.checkBox2.Checked)
            {
                if (this.textBox25.Text == "")
                {
                    MessageBox.Show("请输入代理网站后面的数字");
                    return;
                }
                GetProxyIPPage("http://www.youdaili.net/Daili/guonei/" + this.textBox25.Text + ".html");
                if (lstip == null)
                {
                    MessageBox.Show("获取代理网站IP失败，请重新输入");
                    return;
                }
            }
            //解析出输入的所有页面中的title和ID，将其存如pageTitID集合中
            string pageMess = "";
            int[] itemCount = new int[5];
            int index = 0;
            Dictionary<string, string> pageTitID = new Dictionary<string,string>();
            while(minPage<=maxPage)
            {
                pageMess = getQKPageMess(this.textBox4.Text + minPage.ToString()+"/");
                if (pageMess == "")
                {
                    MessageBox.Show("获取页面信息失败,请重试");
                    break;
                }
                itemCount[index] = qkstr2list(pageTitID,pageMess);
                minPage++;
                index++;
            }
            if (pageTitID.Count == 0)
            {
                MessageBox.Show("解析失败，网页是否更新？");
                return;
            }
            minPage = int.Parse(this.textBox6.Text);
            this.progressBar2.Minimum = 0;
            this.progressBar2.Maximum = pageTitID.Count;
            this.progressBar2.Value = 2;
            this.progressBar2.Step = 1;
            frmMain.pro1Max = pageTitID.Count;
            //初始化表格,取出集合中的所有信息 ，取出title和标题名称合并，放入表格
            index = 0;
            this.dataGridView2.Rows.Clear();
            
            this.dataGridView2.Rows.Add(pageTitID.Count);
            int rowCountda = 0;
            int tem = 0;
            foreach(var item in pageTitID)
            {
                this.dataGridView2.Rows[rowCountda].Cells[0].Value = minPage;//页码
                this.dataGridView2.Rows[rowCountda].Cells[1].Value = backchar + backnum.ToString()+ "-" + item.Value;//文件名称
                this.dataGridView2.Rows[rowCountda].Cells[2].Value = "";//下载地址
                this.dataGridView2.Rows[rowCountda].Cells[3].Value = "";//下载状态
                this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = Color.Black;
                tem++;
                if(itemCount[index]==tem)/////////////////////////////////////////////////////////////
                {
                    minPage++;
                    index++;
                    tem = 0;
                }
                rowCountda++;
                backnum++;
            }
            //异步调用下载线程
            backnum = int.Parse(this.textBox8.Text.Substring(1, this.textBox8.Text.Length - 1));
            minPage = int.Parse(this.textBox6.Text);
            ThreadQKSharp threadQKSharp = threadQK;
            IAsyncResult threadQKSharpResult = threadQKSharp.BeginInvoke(backchar, backnum, saveUrl, pageTitID, minPage,itemCount,this.comboBox4.Text,this.textBox4.Text,int.Parse(this.textBox21.Text), null, null);
        }
        public delegate void ThreadQKSharp(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary, int minPage, int[] itemCount, string typeName, string pageUrl, int seelpTime);
        //千库网控制下载的线程
        public void threadQK(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary,int minPage,int[] itemCount,string typeName,string pageUrl,int seelpTime) 
        {
            string dowUrl = "";
            int rowCountda = -1;
            int index = 0;
            foreach (var item in IDictionary)
            {
                rowCountda++;
                
                if((rowCountda+1)==itemCount[index])
                {
                    index++;
                    minPage++;
                }
                this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "正在获取下载链接~");//"获取中~~~"
                this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Blue);

                dowUrl = getQKUrl(item.Key, typeName, pageUrl + minPage);

                this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[2].Value = s; }), dowUrl);               
                int httpNum = 0;
                while (dowUrl == "无法连接到远程服务器")
                {
                    dowUrl = getQKUrl(item.Key, this.comboBox4.Text, this.textBox4.Text + minPage);
                    httpNum++;
                    if (httpNum == 2)
                    {
                        dowUrl = "失败";
                        break;
                    }
                }
                if (dowUrl == "下载限制")
                {
                    MessageBox.Show("下载限制");
                    break;
                }
                if (dowUrl == "失败")
                {

                    //更新下载状态
                    if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "获取下载链接失败");//"下载失败"
                        this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Orange);

                    }
                    //更新进度条
                    if (this.progressBar2.InvokeRequired)//找到创建此控件的线程
                    {
                        frmMain.pro1Value++;
                        this.progressBar2.Invoke(new Action<int>(s => { this.progressBar2.Value = s; }), frmMain.pro1Value);
                        if (frmMain.pro1Value == frmMain.pro1Max)
                        {
                            MessageBox.Show("下载完成");
                        }
                    }
                }
                else
                {
                    string itemType = ".rar";
                    if (dowUrl.Contains("rar?"))
                        itemType = ".rar";
                    if (dowUrl.Contains("zip?"))
                        itemType = ".zip";
                    if (dowUrl.Contains("psd?"))
                        itemType = ".psd";
                    if (dowUrl.Contains("jpg?"))
                        itemType = ".jpg";
                    if (dowUrl.Contains("png?"))
                        itemType = ".png";
                    double milTime = downAndTimeQK(dowUrl, saveUrl, backchar + backnum.ToString() + "-" + item.Value + itemType, rowCountda);
                    if (milTime < seelpTime)
                    {
                        Thread.Sleep((int)(seelpTime - milTime));
                    }

                }
                //每次遍历一个就将数字加一
                backnum++;
                while (true)
                {
                    if (exitState)//退出
                    {
                        IDictionary.Clear();
                        if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView2.Invoke(new MethodInvoker(() => this.dataGridView2.Rows.Clear()));
                        }
                        return;
                    }
                    if (sleepState)//暂停
                    {
                        continue;
                    }
                    else //继续
                    {
                        break;
                    }
                }
            }
        }
        //返回下载千库图片的下载时间
        public double downAndTimeQK(string downUrl, string saveUrl, string saveName, int rowCountda) 
        {
            DateTime a = DateTime.Now;
            downFileallQK(downUrl, saveUrl, saveName, rowCountda);
            DateTime b = DateTime.Now;
            TimeSpan c = b - a;
            return c.TotalMilliseconds;
        }
        //千库下载方法，携带更新进度条，跟新下载状态
        public void downFileallQK(string downUrl, string saveUrl, string saveName, int rowCountda) 
        {
            {
                //打开上次下载的文件
                //long SPosition = 0;
                //实例化流对象
                saveName = saveName.Replace("/", "");
                saveName = saveName.Replace("*", "");
                saveName = saveName.Replace("\\", "");
                saveName = saveName.Replace("+", "");
                //判断要下载的文件是否存在
                if (File.Exists(saveUrl + "\\" + saveName))
                {
                    //更新下载状态
                    if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "文件已存在");
                        this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Yellow);
                    }
                    //更新进度条
                    if (this.progressBar2.InvokeRequired)//找到创建此控件的线程
                    {
                        frmMain.pro1Value++;
                        this.progressBar2.Invoke(new Action<int>(s => { this.progressBar2.Value = s; }), frmMain.pro1Value);
                        if (frmMain.pro1Value == frmMain.pro1Max)
                        {
                            MessageBox.Show("下载完成");
                        }
                    }
                }
                else
                {
                    //更新下载状态
                    if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "下载中~~~");
                        this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.YellowGreen);
                    }
                    FileStream FStream;
                    FStream = new FileStream(saveUrl + "\\" + saveName, FileMode.Create);
                    try
                    {
                        //打开网络连接
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(downUrl);
                        //if (SPosition > 0)
                        //    myRequest.AddRange((int)SPosition);             //设置Range值
                        //向服务器请求,获得服务器的回应数据流
                        Stream myStream = myRequest.GetResponse().GetResponseStream();
                        //定义一个字节数据
                        byte[] btContent = new byte[512];
                        int intSize = 0;
                        intSize = myStream.Read(btContent, 0, 512);
                        //finishedsize = intSize;
                        while (intSize > 0)
                        {
                            FStream.Write(btContent, 0, intSize);
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize += intSize;
                        }
                        //关闭流
                        FStream.Close();
                        myStream.Close();
                        //更新下载状态
                        if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                            this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                        }
                        //更新进度条
                        if (this.progressBar2.InvokeRequired)//找到创建此控件的线程
                        {
                            frmMain.pro1Value++;
                            this.progressBar2.Invoke(new Action<int>(s => { this.progressBar2.Value = s; }), frmMain.pro1Value);
                            if (frmMain.pro1Value == frmMain.pro1Max)
                            {
                                MessageBox.Show("下载完成");
                            }
                        }
                    }
                    catch 
                    {
                        //第一次打开链接失败，就重新再试一次！
                        try
                        {
                            //打开网络连接
                            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(downUrl);
                            //if (SPosition > 0)
                            //    myRequest.AddRange((int)SPosition);             //设置Range值
                            //向服务器请求,获得服务器的回应数据流
                            Stream myStream = myRequest.GetResponse().GetResponseStream();
                            //定义一个字节数据
                            byte[] btContent = new byte[512];
                            int intSize = 0;
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize = intSize;
                            while (intSize > 0)
                            {
                                FStream.Write(btContent, 0, intSize);
                                intSize = myStream.Read(btContent, 0, 512);
                                //finishedsize += intSize;
                            }
                            //关闭流
                            FStream.Close();
                            myStream.Close();
                            //更新下载状态
                            if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                            {
                                this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                                this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                            }
                            //更新进度条
                            if (this.progressBar2.InvokeRequired)//找到创建此控件的线程
                            {
                                frmMain.pro1Value++;
                                this.progressBar2.Invoke(new Action<int>(s => { this.progressBar2.Value = s; }), frmMain.pro1Value);
                                if (frmMain.pro1Value == frmMain.pro1Max)
                                {
                                    MessageBox.Show("下载完成");
                                }
                            }

                        }
                        catch 
                        {
                            FStream.Close();
                            //更新下载状态
                            if (this.dataGridView2.InvokeRequired)//找到创建此控件的线程
                            {
                                this.dataGridView2.Invoke(new Action<string>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Value = s; }), "访问链接出错");
                                this.dataGridView2.Invoke(new Action<Color>(s => { this.dataGridView2.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Red);
                            }
                            //更新进度条
                            if (this.progressBar2.InvokeRequired)//找到创建此控件的线程
                            {
                                frmMain.pro1Value++;
                                this.progressBar2.Invoke(new Action<int>(s => { this.progressBar2.Value = s; }), frmMain.pro1Value);
                                if (frmMain.pro1Value == frmMain.pro1Max)
                                {
                                    MessageBox.Show("下载完成");
                                }
                            }

                        }
                    }
                }
            }
        }
        //获取千库网页源码
        private string getQKPageMess(string pkpagestr)
        {

            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = pkpagestr,
                Allowautoredirect = false,
                Cookie = frmMain.CK
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            return htmlText;
        }
        //解析千库网源码，取出title和ID
        private int qkstr2list(Dictionary<string, string> IDictionary, string strDate)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(strDate);
            HtmlNode node = doc.DocumentNode;
            try
            {
                string[] ss = this.textBox4.Text.Split(new char[] { '/' });
                string before = ss[3];
                if (before == "banner" || before == "xiangqingye")
                {
                    var aTags = node.SelectNodes("//a[@class='db']");
                    var divs = node.SelectNodes("//div[@class='hover-txt']");
                    for (int i = 0; i < aTags.Count;i++ ) 
                    {
                        if (aTags[i].Attributes["href"].Value.Contains("http://588ku.com/")) 
                        {
                            string datakey = aTags[i].Attributes["href"].Value;
                            datakey = datakey.Replace("http://588ku.com/" + before + "/", "");
                            datakey = datakey.Replace(".html", "");
                            string[] title = divs[i].InnerHtml.Split(new char[] { '(' });
                            try { IDictionary.Add(datakey, title[0]); }
                            catch { }
                        }
                    }
                }
                else
                {
                    var aTags = node.SelectNodes("//a");
                    for (int i = 0; i < aTags.Count; i++)
                    {
                        if (!aTags[i].InnerHtml.Contains("<") && !aTags[i].InnerHtml.Contains(">"))
                        {
                            if (aTags[i].InnerHtml != null)//千库网http://588ku.com/zhutu/0-27-new-59不包含target
                            {
                                if (aTags[i].Attributes["href"] != null)
                                {
                                    if (aTags[i].Attributes["href"].Value.Contains("http://588ku.com/" + before) && aTags[i].Attributes["href"].Value.Contains(".html"))
                                    {
                                        string title = "";
                                        if (aTags[i].InnerHtml == null)
                                        {
                                            title = "";
                                        }
                                        else 
                                        {
                                            title = aTags[i].InnerHtml;
                                        }
                                        string datakey = aTags[i].Attributes["href"].Value;
                                        datakey = datakey.Replace("http://588ku.com/" + before + "/", "");
                                        datakey = datakey.Replace(".html", "");
                                        IDictionary.Add(datakey, title);
                                    }
                                }

                            }
                        }
                    }
                }
                return IDictionary.Count;
            }
            catch
            {
                MessageBox.Show("解析失败");
                return 0;
            }
        }     
        //发送千库网请求获取某张图片的下载地址,不使用代理
        private string getQKUrl(string itemID, string typename, string pageUrl)
        {
            string downurl = "";
            if (pageUrl.Contains("sucai"))
            {
                if (typename == "PSD")
                {
                    downurl = getQKUrl1(pageUrl, "http://588ku.com/index.php?m=element&a=downpsd&id=" + itemID);
                }
                if (typename == "PNG")
                {
                    downurl = getQKUrl1(pageUrl, "http://588ku.com/index.php?m=element&a=down&id=" + itemID);
                }
            }
            else
            {
                if (typename == "PSD")
                {
                    downurl = getQKUrl1(pageUrl, "http://588ku.com/index.php?m=back&a=downpsd&id=" + itemID);
                }
                if (typename == "PNG")
                {
                    downurl = getQKUrl1(pageUrl, "http://588ku.com/index.php?m=back&a=down&id=" + itemID);
                }
            }

            return downurl;
        }
        private string getQKUrl1(string pageUrl, string downUrl)
        {
            //剔除cookie字符串中的answer和word
            string[] CKStrs = frmMain.CK.Split(new char[]{';'});
            //初始化frmMain.ck
            frmMain.CK = "";
            for (var i = 0; i < CKStrs.Length; i++) 
            {
                if ((!CKStrs[i].Contains("answer")) && (!CKStrs.Contains("word")))
                {
                    frmMain.CK = frmMain.CK + CKStrs[i] + ";";
                }
            }
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = downUrl,
                Host = "588ku.com",
                Cookie = frmMain.CK,
                Method = "GET",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                Referer = pageUrl,
                AutoRedirectCookie = true,
                KeepAlive = true
            };
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            //解决验证码问题  http://588ku.com/index.php?m=misc&a=verifyCaptcha&c=1
            string resCookieStr = result.Cookie;//获取服务器返回的cookie
            //将服务器返回的answer和word加入到cookie
            if (resCookieStr != null)
            {
                string[] resCookies = resCookieStr.Split(new char[] { ';', ',' });
                string answer = "";
                string word = "";
                if (resCookies.Length > 15)
                {
                    for (var j = 0; j < resCookies.Length; j++) 
                    {
                        if (resCookies[j].Contains("answer") && !resCookies[j].Contains("deleted")) 
                        {
                            resCookies[j] = resCookies[j].Replace("answer=", "");
                            answer = resCookies[j];
                            
                        }
                        if (resCookies[j].Contains("word") && !resCookies[j].Contains("deleted")) 
                        {
                            resCookies[j] = resCookies[j].Replace("word=", "");
                            word = resCookies[j];
                        }
                        
                    }
                    frmMain.CK = frmMain.CK + "answer=" + answer + ";word=" + word + ";";
                    //发送验证码认证,携带answer和word
                    sendCode(answer, pageUrl);
                    string codeHtmlText = getQKUrlCode(pageUrl, downUrl);
                    return subStringRespqk(codeHtmlText);
                }
                else
                {
                    return subStringRespqk(htmlText);
                }
            }
            return subStringRespqk(htmlText);

             
        }
        //若返回值显示需要验证码，则发送验证码到千库
        private void sendCode(string answer ,string refer) 
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "http://588ku.com/index.php?m=misc&a=verifyCaptcha&c="+answer,
                Host = "588ku.com",
                Cookie = frmMain.CK,
                Method = "GET",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                Referer = refer,
                AutoRedirectCookie = true,
                KeepAlive = true
            };
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
        }
        //发送验证码后，再获取下载链接函数
        private string getQKUrlCode(string pageUrl, string downUrl)
        {
            //剔除cookie字符串中的answer和word
            string[] CKStrs = frmMain.CK.Split(new char[] { ';' });
            //初始化frmMain.ck
            frmMain.CK = "";
            for (var i = 0; i < CKStrs.Length; i++)
            {
                if ((!CKStrs[i].Contains("answer")) && (!CKStrs.Contains("word")))
                {
                    frmMain.CK = frmMain.CK + CKStrs[i] + ";";
                }
            }
            string[] downUrls = downUrl.Split(new char[] { '&' });
            downUrl = "http://588ku.com/?m=element&" + downUrls[1] + "&yz=1" + "&" + downUrls[2];
            if(frmMain.CK.Contains("down_type=3"))
            {
                frmMain.CK = frmMain.CK.Replace("down_type=3", "down_type=1"); 
            }
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = downUrl,
                Host = "588ku.com",
                Cookie = frmMain.CK,
                Method = "GET",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                Referer = pageUrl,
                AutoRedirectCookie = true,
                KeepAlive = true
            };
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            return htmlText;
        }
        //发送千库网请求获取下载地址,使用代理
        private string getQKUrlip(string itemID, string typename, string pageUrl, IpAdr i)
        {
            System.GC.Collect();
            string ipstr = i.IP + ":" + i.Port;
            DateTime begintime = DateTime.Now;
            bool OK = Ping(i.IP);
            if (!OK)
            {
                return "ip更换未成功";
            }
            TimeSpan ts = DateTime.Now - begintime;
            string downurl = "";
            if (pageUrl.Contains("sucai"))
            {
                if (typename == "PSD")
                {
                    downurl = getQKUrlip1(pageUrl, "http://588ku.com/index.php?m=element&a=downpsd&id=" + itemID, ipstr);
                }
                if (typename == "PNG")
                {
                    downurl = getQKUrlip1(pageUrl, "http://588ku.com/index.php?m=element&a=down&id=" + itemID, ipstr);
                }
            }
            else
            {
                if (typename == "PSD")
                {
                    downurl = getQKUrlip1(pageUrl, "http://588ku.com/index.php?m=back&a=downpsd&id=" + itemID, ipstr);
                }
                if (typename == "PNG")
                {
                    downurl = getQKUrlip1(pageUrl, "http://588ku.com/index.php?m=back&a=down&id=" + itemID, ipstr);
                }
            }

            return downurl;
        }
        private string getQKUrlip1(string pageUrl, string downUrl, string ipstr)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = downUrl,
                Host = "588ku.com",
                Cookie = frmMain.CK,
                Method = "GET",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                Referer = pageUrl,
                AutoRedirectCookie = true,
                ProxyIp = ipstr,
                KeepAlive = true
            };
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;

            return subStringRespqk(htmlText);
            //return subStringRespqk("无法连接到远程服务器");
        }
        //截取千库返回的数据，得到连接
        private static string subStringRespqk(string str)
        {
            if (str.Contains("无法连接到远程服务器"))
            {
                return "无法连接到远程服务器";
            }
            if (str.Contains("[") || str.Contains("script") || !str.Contains("\"status\":0,\"url\":\"http:"))
            {
                return "失败";
            }
            if (str.Contains("{\"status\":0,\"url\":\"http:\\/\\/pic.97uimg.com?_upd=true&id="))
            {
                return "失败";
            }
            str = "[" + str + "]";
            JArray ja = (JArray)JsonConvert.DeserializeObject(str);
            string ja1a = "下载限制";
            if (ja[0]["url"] == null)
            {
                MessageBox.Show("下载限制");
                return ja1a;
            }
            ja1a = ja[0]["url"].ToString();
            return ja1a;
        }



        //90设计下载
        private void button3_Click(object sender, EventArgs e)
        {
            sleepState = false;//不暂停
            exitState = false;//不退出
            this.dataGridView1.DataSource = null;
            frmMain.pro1Value = 0;
            frmMain.thNum = 0;
            if (!CK.Contains("sns")) 
            {
                MessageBox.Show("请先登录90设计");
                return;
            }
            if(this.textBox9.Text=="")
            {
                MessageBox.Show("请输入名称号码");
                return;
            }
            int minPage = int.Parse(this.textBox3.Text);
            int maxPage = int.Parse(this.textBox2.Text);
            if(minPage>maxPage)
            {
                MessageBox.Show("起始页和终止页不符合要求");
                return;
            }
            //获取代理网站数据
            if(this.checkBox1.Checked)
            {
                if (this.textBox24.Text == "")
                {
                    MessageBox.Show("请输入代理网站后面的数字");
                    return;
                }
                GetProxyIPPage("http://www.youdaili.net/Daili/guonei/" + this.textBox24.Text + ".html");
                if (lstip == null)
                {
                    MessageBox.Show("获取代理网站IP失败，请重新输入");
                    return;
                }
            }
            string downUrl = this.textBox20.Text;

            string saveUrl = this.textBox1.Text;
            int backnum = int.Parse(this.textBox9.Text.Substring(1, this.textBox9.Text.Length - 1));
            string backchar = this.textBox9.Text.Substring(0, 1);            
            this.progressBar1.Value = 0;
            this.progressBar1.Step = 1;
            this.progressBar1.Minimum = 0;
            
            Dictionary<string, string> IDictionary = new Dictionary<string, string>();
            int[] pageNum = new int[5];
            int page = 0;
            while (minPage <= maxPage)
            {
                string pageMess = getPageMess(downUrl, minPage);
                if (pageMess == "") 
                {
                    MessageBox.Show("获取第" + minPage + "页面信息失败");
                    break;
                }
                pageNum[page] = str2list(pageMess, IDictionary);
                minPage++;
                page++;
            }
            //将数组指针移至首位
            page = 0;
            minPage = int.Parse(this.textBox3.Text);
            this.progressBar1.Maximum = IDictionary.Count;
            frmMain.pro1Max = IDictionary.Count;
            this.dataGridView1.Rows.Clear();
            if (IDictionary.Count == 0) 
            {
                MessageBox.Show("此页不能解析，请更换");
                return;
                //System.Threading.Thread.ResetAbort();
            }
            this.dataGridView1.Rows.Add(IDictionary.Count);
            int rowCountda = 0;
            int tem = 0;
            foreach (var item in IDictionary)
                {
                    this.dataGridView1.Rows[rowCountda].Cells[0].Value = minPage.ToString();
                    this.dataGridView1.Rows[rowCountda].Cells[1].Value = backchar + backnum.ToString() + "-" + item.Value;                    
                    this.dataGridView1.Rows[rowCountda].Cells[2].Value = "";
                    this.dataGridView1.Rows[rowCountda].Cells[3].Value = "";
                    this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = Color.Black;
                    
                    rowCountda++;
                    //名称数字加一
                    backnum++;
                    tem++;
                    if (tem == pageNum[page]) 
                    {
                        minPage++;
                        page++;
                        tem = 0;
                    }
                }

            backnum = int.Parse(this.textBox9.Text.Substring(1, this.textBox9.Text.Length - 1));
            //异步调用thread1Sharp控制下载的主线程
            Thread90Sharp thread90Sharp = thread90;
            IAsyncResult thread1SharpResult = thread90Sharp.BeginInvoke(backchar, backnum, saveUrl, IDictionary,int.Parse(this.textBox23.Text), null, null);
        }
        //控制下载的主线程
        public delegate void Thread90Sharp(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary, int sleepTime);
        public void thread90(string backchar, int backnum,string saveUrl, Dictionary<string, string> IDictionary,int sleepTime)
        {
            string dowUrl = "";
            int rowCountda = -1;
            foreach (var item in IDictionary)
            {
                rowCountda++;
                
                this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "正在获取下载链接~");//"获取中~~~"
                this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Blue);

                dowUrl = getUrl(item.Key);

                this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[2].Value = s; }), dowUrl);
                if (dowUrl == "下载限制")
                {
                    MessageBox.Show("下载限制");
                    break;
                }
                if (dowUrl == "失败")
                {
                    
                    //更新下载状态
                    if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "获取下载链接失败");//"下载失败"
                        this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Orange);
                        
                    }
                    //更新进度条
                    if (this.progressBar1.InvokeRequired)//找到创建此控件的线程
                    {
                        frmMain.pro1Value++;
                        this.progressBar1.Invoke(new Action<int>(s => { this.progressBar1.Value = s; }), frmMain.pro1Value);
                        if (frmMain.pro1Value == frmMain.pro1Max)
                        {
                            MessageBox.Show("下载完成");
                        }
                    }
                }
                else
                {
                    string backname = ".zip";
                    if (dowUrl.Contains("rar?"))
                        backname = ".rar";
                    if (dowUrl.Contains("zip?"))
                        backname = ".zip";
                    if (dowUrl.Contains("psd?"))
                        backname = ".psd";
                    if (dowUrl.Contains("jpg?"))
                        backname = ".jpg";
                    if (dowUrl.Contains("png?"))
                        backname = ".png";
                    //成功获取到下载地址，调用下载方法,传入下载链接，保存地址，保存文件名，更改进度条，更改下载状态，
                    //方法1：用异步调用下载方法下载
                    //newThread2(dowUrl, saveUrl, backchar+backnum.ToString()+"-"+item.Value+backname, rowCountda);
                    //方法2：用类下载
                    /*
                    myThread myth = new myThread();
                    myth.LocalFileName = saveUrl;
                    myth.name = backchar + backnum.ToString() + "-" + item.Value + backname;
                    myth.RemoteFileName = dowUrl;
                    (new Thread(new ThreadStart(myth.HttpDownloadFileTh))).Start();
                     * */
                    double milTime = downAndTime90(dowUrl, saveUrl, backchar + backnum.ToString() + "-" + item.Value + backname, rowCountda);
                    if(milTime<2000)
                    {

                        Thread.Sleep((int)(2000-milTime));
                    }

                }
                //每次遍历一个就将数字加一
                backnum++;
                while (true)
                {
                    if (exitState)//退出
                    {
                        IDictionary.Clear();
                        if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView1.Invoke(new MethodInvoker(() => this.dataGridView1.Rows.Clear()));
                        }
                        return;
                    }
                    if (sleepState)//暂停
                    {
                        continue;
                    }
                    else //继续
                    {
                        break;
                    }
                }
            }
        }
        //返回下载90图片所用时间的下载函数
        public double downAndTime90(string downUrl, string saveUrl, string saveName,int rowCountda) 
        {
            DateTime a = DateTime.Now;
            downFileall90(downUrl, saveUrl, saveName, rowCountda);
            DateTime b = DateTime.Now;
            TimeSpan c = b - a;
            return c.TotalMilliseconds;
        }
        //90下载方法，携带更新进度条，跟新下载状态
        public void downFileall90(string downUrl, string saveUrl, string saveName,int rowCountda)
        {
            {
                //打开上次下载的文件
                //long SPosition = 0;
                //实例化流对象
                saveName = saveName.Replace("/", "");
                saveName = saveName.Replace("*", "");
                saveName = saveName.Replace("\\", "");
                saveName = saveName.Replace("+", "");
                //判断要下载的文件是否存在
                if (File.Exists(saveUrl + "\\" + saveName))
                {
                    //更新下载状态
                    if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "文件已存在");
                        this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Yellow);
                    }
                    //更新进度条
                    if(this.progressBar1.InvokeRequired)//找到创建此控件的线程
                    {
                        frmMain.pro1Value++;
                        this.progressBar1.Invoke(new Action<int>(s => { this.progressBar1.Value = s; }), frmMain.pro1Value);
                        if (frmMain.pro1Value == frmMain.pro1Max)
                        {
                            MessageBox.Show("下载完成");
                        }
                    }                  
                }
                else
                {
                    //更新下载状态
                    if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "下载中~~~");
                        this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.YellowGreen);
                    }
                    FileStream FStream;
                    FStream = new FileStream(saveUrl + "\\" + saveName, FileMode.Create);
                    try
                    {
                        //打开网络连接
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(downUrl);
                        //if (SPosition > 0)
                        //    myRequest.AddRange((int)SPosition);             //设置Range值
                        //向服务器请求,获得服务器的回应数据流
                        Stream myStream = myRequest.GetResponse().GetResponseStream();
                        //定义一个字节数据
                        byte[] btContent = new byte[512];
                        int intSize = 0;
                        intSize = myStream.Read(btContent, 0, 512);
                        //finishedsize = intSize;
                        while (intSize > 0)
                        {
                            FStream.Write(btContent, 0, intSize);
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize += intSize;
                        }
                        //关闭流
                        FStream.Close();
                        myStream.Close();                        
                        //更新下载状态
                        if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                            this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                        }
                        //更新进度条
                        if (this.progressBar1.InvokeRequired)//找到创建此控件的线程
                        {
                            frmMain.pro1Value++;
                            this.progressBar1.Invoke(new Action<int>(s => { this.progressBar1.Value = s; }), frmMain.pro1Value);
                            if (frmMain.pro1Value == frmMain.pro1Max)
                            {
                                MessageBox.Show("下载完成");
                            }
                        }
                    }
                    catch 
                    {
                        //第一次打开链接失败，就重新再试一次！
                        try
                        {
                            //打开网络连接
                            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(downUrl);
                            //if (SPosition > 0)
                            //    myRequest.AddRange((int)SPosition);             //设置Range值
                            //向服务器请求,获得服务器的回应数据流
                            Stream myStream = myRequest.GetResponse().GetResponseStream();
                            //定义一个字节数据
                            byte[] btContent = new byte[512];
                            int intSize = 0;
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize = intSize;
                            while (intSize > 0)
                            {
                                FStream.Write(btContent, 0, intSize);
                                intSize = myStream.Read(btContent, 0, 512);
                                //finishedsize += intSize;
                            }
                            //关闭流
                            FStream.Close();
                            myStream.Close();
                            //更新下载状态
                            if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                            {
                                this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                                this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                            }
                            //更新进度条
                            if (this.progressBar1.InvokeRequired)//找到创建此控件的线程
                            {
                                frmMain.pro1Value++;
                                this.progressBar1.Invoke(new Action<int>(s => { this.progressBar1.Value = s; }), frmMain.pro1Value);
                                if (frmMain.pro1Value == frmMain.pro1Max)
                                {
                                    MessageBox.Show("下载完成");
                                }
                            }
                        }
                        catch 
                        {
                            FStream.Close();
                            //更新下载状态
                            if (this.dataGridView1.InvokeRequired)//找到创建此控件的线程
                            {
                                this.dataGridView1.Invoke(new Action<string>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Value = s; }), "访问链接出错");
                                this.dataGridView1.Invoke(new Action<Color>(s => { this.dataGridView1.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Red);
                            }
                            //更新进度条
                            if (this.progressBar1.InvokeRequired)//找到创建此控件的线程
                            {
                                frmMain.pro1Value++;
                                this.progressBar1.Invoke(new Action<int>(s => { this.progressBar1.Value = s; }), frmMain.pro1Value);
                                if (frmMain.pro1Value == frmMain.pro1Max)
                                {
                                    MessageBox.Show("下载完成");
                                }
                            }
                        }                        
                    }
                }
            }
        }
        //获取90设计网页源码
        private string getPageMess(string downUrl,int page) 
        {
            string pageStr = page.ToString();
            string url = downUrl + pageStr + ".html";

            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Allowautoredirect = false,
                Cookie = frmMain.CK
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            return htmlText;
        }
        //将90设计网页源码解析出title和ID
        private int str2list(string strDate,Dictionary<string, string> IDictionary)
        {
            try
            {
                //Dictionary<string, string> IDictionary = new Dictionary<string, string>();//存储title和datakey
                string strHsaRe = strDate.Replace("\\", "");
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(strHsaRe);

                HtmlNode node = doc.DocumentNode;
                HtmlNode div = node.SelectNodes("//div[@class='imgListBox clearfix']")[0];
                //HtmlNode div1 = div.SelectNodes("//div[@class='imgListBox clearfix']")[0];


                var lis = node.SelectNodes("//li[@class='imgItem imgItemN']");
                int count = lis.Count;
                int i = 0;
                foreach (HtmlNode li in lis)
                {
                    HtmlNode div2 = li.SelectNodes("//div[@class='imageBox imageBoxN']")[i];
                    HtmlNode a = div2.SelectNodes("//a[@class='mask ajaxShow']")[i];
                    string title = a.Attributes["title"].Value;
                    string datakey = a.Attributes["data-key"].Value;
                    try
                    {
                        if (IDictionary.ContainsKey(title) == false)
                        {
                            IDictionary.Add(datakey, title);
                        }

                    }
                    catch 
                    {
                        //Console.WriteLine("Error: {0}", e.Message);
                    }
                    i++;
                }
                return IDictionary.Count;
            }
            catch
            {
                MessageBox.Show("解析失败");
                return 0;
            }
            
        }
        //发送90设计请求，不用代理
        private string getUrl(string datakey) 
        {
            //TimeSpan ts = DateTime.Now - begintime;(Request-Line)	POST /index.php?m=inspireAjax&a=getDownloadLink HTTP/1.1
            //string url = "http://90sheji.com/index.php?m=inspireAjax&a=getDownloadLink";
            string url = "http://90sheji.com/index.php?m=inspireAjax&a=getDownloadLink";
            string refer = "http://90sheji.com/?m=Inspire&a=download&id=" + datakey;
            //string url = "http://90sheji.com/index.php?m=inspireAjax&a=getDownloadLink HTTP/1.1";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//改URL
                Host = "90sheji.com",
                Cookie = frmMain.CK,
                Method = "POST",
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                Accept = "application/json, text/javascript, */*; q=0.01",
                Referer = "http://90sheji.com/?m=Inspire&a=download&id=" + datakey,
                Postdata = "id=" + datakey,//"{\"id\":\"250\"}",//Post数据     可选项GET时不需要写  
                AutoRedirectCookie = true,
                ContentType = "application/x-www-form-urlencoded",
                // Allowautoredirect = true,
                KeepAlive = true
            };
            item.Header.Add("Accept-Encoding", "gzip, deflate");
            item.Header.Add("ContentLength", "6");
            item.Header.Add("x-requested-with", "XMLHttpRequest");
            item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;

            return subStringResp(htmlText);        
        }
        //发送90设计请求，用代理
        private string getUrlip(string datakey,IpAdr i) 
        {
                //System.GC.Collect();
                string ipstr = i.IP + ":" + i.Port;
                //DateTime begintime = DateTime.Now;
                bool OK = Ping(i.IP);
                if (!OK)
                {
                    return "ip更换未成功";
                }
                //TimeSpan ts = DateTime.Now - begintime;
                string url = "http://90sheji.com/index.php?m=inspireAjax&a=getDownloadLink";
                string refer = "http://90sheji.com/?m=Inspire&a=download&id=" + datakey;
                //string url = "http://90sheji.com/index.php?m=inspireAjax&a=getDownloadLink HTTP/1.1";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = url,
                    Host = "90sheji.com",
                    Cookie = frmMain.CK,
                    Method = "POST",
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                    Accept = "application/json, text/javascript, */*; q=0.01",
                    Referer = "http://90sheji.com/?m=Inspire&a=download&id=" + datakey,
                    Postdata = "id=" + datakey,//"{\"id\":\"250\"}",//Post数据     可选项GET时不需要写  
                    AutoRedirectCookie = true,
                    ContentType = "application/x-www-form-urlencoded",
                    ProxyIp = ipstr,
                    // Allowautoredirect = true,
                    KeepAlive = true
                };
                item.Header.Add("Accept-Encoding", "gzip, deflate");
                item.Header.Add("ContentLength", "6");
                item.Header.Add("x-requested-with", "XMLHttpRequest");
                item.Expect100Continue = false;
                HttpResult result = http.GetHtml(item);
                string htmlText = result.Html;

                return subStringResp(htmlText);
            
        }
        //截取90设计返回的数据，得到连接
        private static string subStringResp(string str)
        {
            if (str.Contains("overload"))
            {
                return "下载限制";
            }
            if (str.Contains("[") || str.Contains("script") || !str.Contains("rar")) 
            {
                return "失败";
            }
            str = "["+str+"]";

            JArray ja = (JArray)JsonConvert.DeserializeObject(str);
            string ja1a = "下载限制";
            if (ja[0]["link"]==null) 
            {
                return ja1a;
            }
            ja1a = ja[0]["link"].ToString();
            return ja1a;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.timer1.Start();
        }





        //JPG下载按钮
        private void button10_Click(object sender, EventArgs e)
        {
            sleepState = false;//不暂停
            exitState = false;//不退出
            this.dataGridView4.DataSource = null;
            frmMain.CK = "";
            if (this.textBox15.Text == "")
            {
                MessageBox.Show("请输入页面连接");
                return;
            }
            if (this.textBox19.Text == "")
            {
                MessageBox.Show("请选择保存地址");
                return;
            }
            if (this.textBox16.Text == "" || this.textBox17.Text == "")
            {
                MessageBox.Show("请输入起始页和结束页");
                return;
            }
            if (this.textBox18.Text == "")
            {
                MessageBox.Show("请输入名称号码");
                return;
            }      
            int minPage = int.Parse(this.textBox16.Text);
            int maxPage = int.Parse(this.textBox17.Text);
            int minPage_tem = int.Parse(this.textBox16.Text);
            int maxPage_tem = int.Parse(this.textBox17.Text);
            if (minPage > maxPage)
            {
                MessageBox.Show("起始页和终止页不符合要求");
                return;
            }
            label6.Text = "90设计未登录";
            label7.Text = "千库网未登录";
            label14.Text = "千图网未登录";
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label14.ForeColor = System.Drawing.Color.Black;
            string saveUrl = this.textBox19.Text;
            int backnum = int.Parse(this.textBox18.Text.Substring(1, this.textBox18.Text.Length - 1));
            int backnum_tem = int.Parse(this.textBox18.Text.Substring(1, this.textBox18.Text.Length - 1));
            string backchar = this.textBox18.Text.Substring(0, 1);
            Dictionary<string, string> pageUrlTit = new Dictionary<string, string>();//存储URL和title的集合
            int[] pageNum = new int[maxPage-minPage+1];
            int page = 0;
            while (minPage_tem <= maxPage_tem)
            {
                //千库
                if (this.textBox15.Text.Contains("588ku.com"))
                {
                    string strdate = getJPGPageMess(this.textBox15.Text + minPage_tem.ToString()+"/");
                    pageNum[page] = qkstr2listjpg(strdate, pageUrlTit);
                }
                //千图
                else if (this.textBox15.Text.Contains("58pic.com"))
                {
                    string strdate = getJPGPageMess(this.textBox15.Text + minPage_tem.ToString() + ".html");
                    pageNum[page] = getQTJpgUrl(strdate, pageUrlTit);
                }
                //其他网站
                else
                {
                    pageNum[page] = getJpgUrl(this.textBox15.Text + minPage_tem.ToString() + ".html", pageUrlTit);
                }
                minPage_tem++;
                page++;
            }
            page = 0;//将数组指针移至首位
            int rowCount = 0;
            if (pageUrlTit.Count == 0) 
            {
                MessageBox.Show("解析失败，网页是否更新？");
                return;
            }
            this.dataGridView4.Rows.Add(pageUrlTit.Count);
            //初始化dategrideview4
            int tem = 0;
            foreach (var item in pageUrlTit)
            {
                string value = item.Value.Replace("/", "");
                value = value.Replace("*", "");

                this.dataGridView4.Rows[rowCount].Cells[0].Value = minPage.ToString();
                this.dataGridView4.Rows[rowCount].Cells[1].Value = backchar + backnum_tem.ToString()+"-"+value;
                this.dataGridView4.Rows[rowCount].Cells[2].Value = item.Key;
                this.dataGridView4.Rows[rowCount].Cells[3].Value = "";
                this.dataGridView4.Rows[rowCount].Cells[3].Style.BackColor = Color.Black;
                rowCount++;
                backnum_tem++;
                tem++;
                if(tem == pageNum[page])
                {
                    minPage++;
                    page++;
                    tem = 0;
                }
            }
            //异步调用thread1Sharp控制下载的主线程
            ThreadJPGSharp threadJPGSharp = JPGThread;
            IAsyncResult thread1SharpResult = threadJPGSharp.BeginInvoke(backchar, backnum, saveUrl,pageUrlTit, null, null);
        }
        //JPG异步下载线程
        public delegate void ThreadJPGSharp(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary);
        public void JPGThread(string backchar, int backnum, string saveUrl, Dictionary<string, string> IDictionary) 
        {
            int rowCountda = 0;
            foreach (var item in IDictionary)
            {
                //更新下载状态
                if (this.dataGridView4.InvokeRequired)//找到创建此控件的线程
                {
                    this.dataGridView4.Invoke(new Action<string>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Value = s; }), "正在下载");
                    this.dataGridView4.Invoke(new Action<Color>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Blue);
                }
                string downFileCode = HttpDownloadFile(saveUrl, backchar + backnum.ToString() + "-" + item.Value + ".jpg", item.Key);//更新下载状态
                if (downFileCode == "文件已存在") 
                {
                    if (this.dataGridView4.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView4.Invoke(new Action<string>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Value = s; }), "文件已存在");
                        this.dataGridView4.Invoke(new Action<Color>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Blue);
                    }
                }
                if (downFileCode == "下载完成")
                {
                    if (this.dataGridView4.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView4.Invoke(new Action<string>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Value = s; }), "下载完成");
                        this.dataGridView4.Invoke(new Action<Color>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Green);
                    }
                }
                if (downFileCode == "下载失败")
                {
                    if (this.dataGridView4.InvokeRequired)//找到创建此控件的线程
                    {
                        this.dataGridView4.Invoke(new Action<string>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Value = s; }), "下载失败");
                        this.dataGridView4.Invoke(new Action<Color>(s => { this.dataGridView4.Rows[rowCountda].Cells[3].Style.BackColor = s; }), Color.Red);
                    }
                }
                backnum++;
                rowCountda++;
                while(true)
                {
                    if (exitState)//退出
                    {
                        IDictionary.Clear();
                        if (this.dataGridView4.InvokeRequired)//找到创建此控件的线程
                        {
                            this.dataGridView4.Invoke(new MethodInvoker(() => this.dataGridView4.Rows.Clear()));
                        }
                        return;
                    }
                    if (sleepState)//暂停
                    {
                        continue;
                    }
                    else //继续
                    {
                        break;
                    }
                }
            }
        }
        //jpg下载方法,LocalFileName为存储路径
        public string HttpDownloadFile(string LocalFileName, string name, string RemoteFileName)
        {
            //实例化流对象
            FileStream FStream;
            //判断要下载的文件是否存在
            if (File.Exists(LocalFileName + "\\" + name))
            {
                return "文件已存在";
            }
            name = name.Replace("/", "");
            name = name.Replace("*", "");
            name = name.Replace("\\", "");
            name = name.Replace("+", "");
            //name = name.Replace("-", "");
            FStream = new FileStream(LocalFileName + "\\" + name, FileMode.Create);
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(RemoteFileName);
                //if (SPosition > 0)
                //    myRequest.AddRange((int)SPosition);             //设置Range值
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                //finishedsize = intSize;
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                    //finishedsize += intSize;
                }
                //关闭流
                FStream.Close();
                myStream.Close();
                return "下载完成";
            }
            catch 
            {
                FStream.Close();
                return "下载失败";
            }
            
        }      
        //解析其他网jpg页面源码
        private int getJpgUrl(string pageUrl,Dictionary<string, string> IDictionary) 
        {
            int count = 0;
            string pageHtml = getJPGPageMess(pageUrl);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageHtml);
            HtmlNode node = doc.DocumentNode;
            var imgs = node.SelectNodes("//img");
            if(imgs!=null)
            {
                foreach (HtmlNode img in imgs)
                {
                    string src = "没此链接";
                    if (img.Attributes["src"] != null)
                    {
                        if (img.Attributes["src"].Value.Contains("jpg") || img.Attributes["src"].Value.Contains("png") || img.Attributes["src"].Value.Contains("JPG") || img.Attributes["src"].Value.Contains("PNG"))
                        {
                            if (img.Attributes["src"].Value.Contains("http://js.90sjimg.com"))
                            {
                               
                            }
                            else 
                            {
                                src = img.Attributes["src"].Value;
                            }
                        }
                    }
                    
                    if (img.Attributes["data-original"] != null)
                    {
                        if (img.Attributes["data-original"].Value.Contains("jpg") || img.Attributes["data-original"].Value.Contains("png") || img.Attributes["data-original"].Value.Contains("JPG") || img.Attributes["data-original"].Value.Contains("PNG"))
                        {
                            if (img.Attributes["data-original"].Value.Contains("http://js.90sjimg.com"))
                            {
                                
                            }
                            else 
                            {
                                src = img.Attributes["data-original"].Value;
                            }
                        }
                    }
                    if (img.Attributes["data-url"] != null && src!="")
                    {
                        if (img.Attributes["data-url"].Value.Contains("jpg") || img.Attributes["data-url"].Value.Contains("png") || img.Attributes["data-url"].Value.Contains("JPG") || img.Attributes["data-url"].Value.Contains("PNG"))
                        {
                            if (img.Attributes["data-url"].Value.Contains("http://js.90sjimg.com"))
                            {

                            }
                            else
                            {
                                src = img.Attributes["data-url"].Value;
                            }
                        }
                    }

                    string[] s = src.Split(new char[] { '!' });
                    src = s[0];
                    string title = "没找到标题";
                    if (img.Attributes["alt"] != null)
                    {
                        title = img.Attributes["alt"].Value;
                        if(title=="")
                        {
                            title = img.OuterHtml;
                            string[] ss = title.Split(new char[]{'"'});
                            title = ss[3];
                        }
                    }
                    if (img.Attributes["title"] != null)
                    {
                        title = img.Attributes["title"].Value;
                        if (title == "")
                        {
                            title = img.OuterHtml;
                            string[] ss = title.Split(new char[] { '"' });
                            title = ss[3];
                        }
                    }
                    if ((src.Contains("jpg") || src.Contains("png") || src.Contains("JPG") || src.Contains("PNG")) && (title != null) && !src.Contains("bpic.588ku.com"))
                    {
                        if (!IDictionary.ContainsKey(src))
                        {
                            IDictionary.Add(src, title);
                            count++;
                            src = "";
                            title = "";
                        }
                    }
                } 
            }
            return count;
        }
        //解析千图网JPG页面源码
        private int getQTJpgUrl(string pageMess, Dictionary<string, string> IDictionary) 
        {
            int count = 0;
            string jpgDownUrl = "";
            string title = "";
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageMess);
            HtmlNode node = doc.DocumentNode;
            var aTags = node.SelectNodes("//a[@class = 'thumb-box']");
            foreach(var aTag in aTags)
            {
                var imgTags = aTag.ChildNodes[0];
                try
                {
                    jpgDownUrl = imgTags.Attributes["src1"].Value;
                }
                catch 
                {
                    jpgDownUrl = imgTags.Attributes["src"].Value;
                }
                title = imgTags.Attributes["title"].Value;
                try
                {
                    jpgDownUrl = jpgDownUrl.Split(new char[]{'!'})[0];
                    IDictionary.Add(jpgDownUrl, title);
                    count++;
                }
                catch
                { }
            }
            /*
            var divs = node.SelectNodes("//div[@class='flow-box']");
            for (int i = 0; i < divs.Count; i++)
            {
                var img = divs[i].ChildNodes[0].ChildNodes[0].ChildNodes[0];
                string src = img.Attributes["src"].Value;
                string[] srcs = src.Split(new char[] { '!' });
                string title = img.Attributes["title"].Value;
                try
                {
                    IDictionary.Add(srcs[0], title);
                    count++;
                }
                catch
                {}
            }*/
            return count;
        }
        //获取千库网jpg页面源码
        private string getJPGPageMess(string qtPageUrl)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = qtPageUrl,
                Allowautoredirect = false,
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            return htmlText;
        }
        //解析千库网JPG页面源码,返回传入页面信息中JPG的数量
        private int qkstr2listjpg(string strDate, Dictionary<string, string> IDictionary)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            int count = 0;
            doc.LoadHtml(strDate);
            HtmlNode node = doc.DocumentNode;
            string[] ss = this.textBox15.Text.Split(new char[] { '/' });
            string before = ss[3];
            if (this.textBox15.Text.Contains("banner") || this.textBox15.Text.Contains("xiangqingye") || 
                this.textBox15.Text.Contains("beijing") )
            {
                var aTags = node.SelectNodes("//a[@class='db']");
                var divs = node.SelectNodes("//div[@class='hover-txt']");
                for (int i = 0; i < aTags.Count; i++)
                {
                    string datakey = aTags[i].Attributes["href"].Value;
                    datakey = qkToJpgPage(datakey);
                    string title = divs[i].InnerHtml;
                    if (title != null && datakey != null && datakey.Contains("http://bpic.588ku.com") && (datakey.Contains("jpg") || datakey.Contains("png") || datakey.Contains("JPG") || datakey.Contains("PNG")))
                    {
                        string[] titleTem = title.Split(new char[] { '(' });
                        try
                        {
                            IDictionary.Add(datakey, titleTem[0]);
                            count++;
                        }
                        catch { }
                    }
                }
            }
            else if (this.textBox15.Text.Contains("千库解析模块~~~~~~写在此处"))
            {

            }
            else
            {
                var aTags = node.SelectNodes("//a");
                //<a href="http://588ku.com/zhutu/4196679.html" class="title_title">直通车主图背景</a>
                for (int i = 0; i < aTags.Count; i++)
                {
                    if (!aTags[i].InnerHtml.Contains("<") && !aTags[i].InnerHtml.Contains(">"))
                    {
                        if (aTags[i].Attributes["href"] != null)
                        {
                            //"http://588ku.com/" + before
                            if (aTags[i].Attributes["href"].Value.Contains("http://588ku.com/" + before) && aTags[i].Attributes["href"].Value.Contains(".html"))
                            {
                                string title = aTags[i].InnerHtml;
                                string datakey = aTags[i].Attributes["href"].Value;
                                datakey = qkToJpgPage(datakey);
                                string[] datakeys = datakey.Split(new char[] { '!' });
                                datakey = datakeys[0];
                                if (title != null && datakey != null && (datakey.Contains("jpg") || datakey.Contains("JPG")))
                                {
                                    IDictionary.Add(datakey, title);
                                    count++;
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }
        //获取千库网的大JPG图
        private string qkToJpgPage(string url)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Allowautoredirect = false
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlText);
            HtmlNode node = doc.DocumentNode;
            var img = node.SelectNodes("//img");
            string jpgUrl = "";
            int i = 0;
            foreach (var imgTag in img)
            {
                if (imgTag.Attributes["src"] != null)
                {
                    string secstr = imgTag.Attributes["src"].Value.ToString();
                    i = i + 1;
                    if (imgTag.Attributes["src"].Value.Contains("http://bpic.588ku.com/"))
                    {
                        jpgUrl = imgTag.Attributes["src"].Value;
                    }
                }
            }
            return jpgUrl.Split(new char[]{'!'})[0];
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog4.ShowDialog() == DialogResult.OK)
            {
                textBox19.Text = folderBrowserDialog4.SelectedPath;
            }
        }



        //获取ip地址
        public void GetProxyIPPage(string url)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,
                AutoRedirectCookie = true,
                Allowautoredirect = true,
                ProtocolVersion = System.Net.HttpVersion.Version10,//获取或设置用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11
            };
            HttpResult result = http.GetHtml(item);
            string htmlText = result.Html;
            //tools.Writelog(htmlText);
            htmlText = htmlText.Replace("\0", " ");
            htmlText = htmlText.Replace("\r\n", " ");
            htmlText = htmlText.Replace("\n", " ");

            //1.193.162.91:8000@HTTP#河南省洛阳市 电信<br />  ((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))

            Regex reg = new Regex(@"(((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))):\d{2,4}@HTTP#)");
            var mats = reg.Matches(htmlText);

            foreach (Match m in mats)
            {
                string ipstr = m.Groups[1].Value;

                IpAdr ipa = new IpAdr();
                reg = new Regex(@"^(.*?):\d{2,4}@HTTP#");
                ipa.IP = reg.Match(ipstr).Groups[1].Value;
                reg = new Regex(":(.*?)@");
                ipa.Port = reg.Match(ipstr).Groups[1].Value;
                ipa.check = 0;
                lstip.Add(ipa);
            }
        }
        public static bool Ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";                                                                                                                                                                                                   
            byte[] buffer = Encoding.ASCII.GetBytes(data);                                                                                                                                                  
            int timeout = 3000; // Timeout 时间，单位：毫秒  
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }
        public static void WriteLog(string mssg)
        {
            string FilePath = "C:\\Users\\Administrator\\Desktop\\" + "自动下载日志" + ".txt";

            try
            {
                if (File.Exists(FilePath))
                {
                    using (StreamWriter tw = File.AppendText(FilePath))
                    {
                        tw.WriteLine(DateTime.Now.ToString() + "> " + mssg);
                    }  //END using

                }  //END if
                else
                {
                    TextWriter tw = new StreamWriter(FilePath);
                    tw.WriteLine(DateTime.Now.ToString() + "=> " + mssg);
                    tw.Flush();
                    tw.Close();
                    tw = null;
                }  //END else

            }  //END Try
            catch 
            { } //END Catch   

        }
        private void button12_Click(object sender, EventArgs e)//暂停/继续
        {
            if (sleepState)
            {
                sleepState = false;
            }
            else 
            {
                sleepState = true;
            }
        }
        private void button13_Click(object sender, EventArgs e)//停止
        {
            exitState = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            #region -- POST请求 --
            //HttpHelper http = new HttpHelper();
            //HttpItem item = new HttpItem()
            //{
            //    URL = "http://df522.com/tools/ssc_ajax.ashx?A=Login&S=dfc&U=admin",
            //    Host = "90sheji.com",
            //    Cookie = frmMain.CK,
            //    Method = "POST",
            //    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
            //    Accept = "application/json, text/javascript, */*; q=0.01",
            //    Referer = "http://df522.com/login",
            //    Postdata = "{\"Action\":\"Login\",\"UserName\":\"admin\",\"Password\":\"df522.com\",\"SourceName\":\"PC\"}",//Post数据     可选项GET时不需要写  
            //    AutoRedirectCookie = true,
            //    ContentType = "application/x-www-form-urlencoded",
            //    //ProxyIp = ipstr,
            //    // Allowautoredirect = true,
            //    KeepAlive = true
            //};
            //item.Header.Add("Accept-Encoding", "gzip, deflate");
            //item.Header.Add("ContentLength", "6");
            //item.Header.Add("x-requested-with", "XMLHttpRequest");
            //item.Expect100Continue = false;
            //HttpResult result = http.GetHtml(item);
            //string htmlText = result.Html;
            #endregion

            #region -- Get请求 --
            string url =
                "https://ssl.ptlogin2.qq.com/check?" +
                "regmaster=&" +
                "pt_tea=2&" +
                "pt_vcode=1&" +
                "uin=345981494&" +
                "appid=1600000716&" +
                "js_ver=10228&" +
                "js_type=1&" +
                "login_sig=hE*6zJvXuwBTeEHPlDTmMApjW80F7ofkyn4jlG-XxdckAbiZ296k1vGYrn3XNe9h&" +
                "r=0.1606137947974859&" +
                "pt_uistyle=40&" +
                "pt_jstoken=856023934";
            HttpHelper http = new HttpHelper();
            
            HttpItem item = new HttpItem()
            {
                URL = url,
                //Host = "www.58pic.com",
                Cookie = frmMain.CK,
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko",
                //Accept = "application/json, text/javascript, */*; q=0.01",
                //AutoRedirectCookie = true,
                //ContentType = "application/x-www-form-urlencoded",
                //KeepAlive = true
            };
            //item.Header.Add("Accept-Encoding", "gzip, deflate");
            //item.Header.Add("ContentLength", "6");
            //item.Header.Add("x-requested-with", "XMLHttpRequest");
            //item.Expect100Continue = false;
            HttpResult result = http.GetHtml(item);
            CookieCollection cc = new CookieCollection();
            result.CookieCollection = cc;
            string htmlText = result.Html;
            #endregion

            #region -- QQ登录网页 --
            /*
             * 1：GET "http://xui.ptlogin2.qq.com/cgi-bin/xlogin?" ，在本地存下 pt_login_sig 的cookie
             * 2：GET "https://ssl.ptlogin2.qq.com/check?" , 获取初始化信息,获取verifycode,salt（为二进制）,sessionid（2，3，4）
             *      ptui_checkVC('0','!VAX','\xf5\x8e\x00\x78\x04\x3c\x87\xac','3cdd20c52738395f25181c36bfb230b9c6','3'
             *      $.Encryption.getEncryption(n, pt.plogin.salt, i.verifycode, pt.plogin.armSafeEdit.isSafe)
             *      pt.plogin.salt为一个二进制
             *          salt转二进制方法（）
             *      i.verifycode为验证码
             *      pt.plogin.armSafeEdit.isSafe为空值
             *      n为加密后的密码
             * 3：拼接url进行登录：
             */
            #endregion
        }

        private void button16_Click(object sender, EventArgs e)
        {
            CK = "";
            string url =
                "https://www.panda.tv/";
            login.loginurl = "https://www.panda.tv/";
            login.incomeDetail = url;
            login.px = 0;
            login.py = 0;


            login lg = new login();
            lg.StartPosition = FormStartPosition.CenterParent;
            lg.Text = "登录90设计";
            lg.ShowDialog();
            if (CK != null && CK != "")
            {
                MessageBox.Show("获取cookie成功");
            }
            else
            {
                MessageBox.Show("获取cookie失败");
            }
            Uri uri = new Uri("https://www.panda.tv/cate");
            WebBrowser MywebBrowser = new WebBrowser();
            MywebBrowser.Url = uri;
            string cookieStr = MywebBrowser.Document.Cookie;
        }
    }
    //文件下载类
    public class myThread 
    {
        public string LocalFileName;
        public string name;
        public string RemoteFileName;
        public void HttpDownloadFileTh()
        {

            //打开上次下载的文件
            //long SPosition = 0;
            //实例化流对象
            name = name.Replace("/","");
            name = name.Replace("*","");
            name = name.Replace("\\", "");
            name = name.Replace("+", "");
            //判断要下载的文件是否存在
            if (File.Exists(LocalFileName + "\\" + name))
            {
                frmMain.thNum++;
            }
            else
            {
                FileStream FStream;
                FStream = new FileStream(LocalFileName + "\\" + name, FileMode.Create);
                try
                {
                    //打开网络连接
                    HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(RemoteFileName);
                    //if (SPosition > 0)
                    //    myRequest.AddRange((int)SPosition);             //设置Range值
                    //向服务器请求,获得服务器的回应数据流
                    Stream myStream = myRequest.GetResponse().GetResponseStream();
                    //定义一个字节数据
                    byte[] btContent = new byte[512];
                    int intSize = 0;
                    intSize = myStream.Read(btContent, 0, 512);
                    //finishedsize = intSize;
                    while (intSize > 0)
                    {
                        FStream.Write(btContent, 0, intSize);
                        intSize = myStream.Read(btContent, 0, 512);
                        //finishedsize += intSize;
                    }
                    //关闭流
                    FStream.Close();
                    myStream.Close();
                    frmMain.thNum++;//返回true下载成功
                }
                catch 
                {
                    try
                    {
                        //打开网络连接
                        HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(RemoteFileName);
                        //if (SPosition > 0)
                        //    myRequest.AddRange((int)SPosition);             //设置Range值
                        //向服务器请求,获得服务器的回应数据流
                        Stream myStream = myRequest.GetResponse().GetResponseStream();
                        //定义一个字节数据
                        byte[] btContent = new byte[512];
                        int intSize = 0;
                        intSize = myStream.Read(btContent, 0, 512);
                        //finishedsize = intSize;
                        while (intSize > 0)
                        {
                            FStream.Write(btContent, 0, intSize);
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize += intSize;
                        }
                        //关闭流
                        FStream.Close();
                        myStream.Close();
                        frmMain.thNum++;//返回true下载成功
                    }
                    catch
                    {
                        try
                        {
                            //打开网络连接
                            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(RemoteFileName);
                            //if (SPosition > 0)
                            //    myRequest.AddRange((int)SPosition);             //设置Range值
                            //向服务器请求,获得服务器的回应数据流
                            Stream myStream = myRequest.GetResponse().GetResponseStream();
                            //定义一个字节数据
                            byte[] btContent = new byte[512];
                            int intSize = 0;
                            intSize = myStream.Read(btContent, 0, 512);
                            //finishedsize = intSize;
                            while (intSize > 0)
                            {
                                FStream.Write(btContent, 0, intSize);
                                intSize = myStream.Read(btContent, 0, 512);
                                //finishedsize += intSize;
                            }
                            //关闭流
                            FStream.Close();
                            myStream.Close();
                            frmMain.thNum++;//返回true下载成功
                        }
                        catch
                        {
                            
                            FStream.Close();
                            frmMain.thNum++;//返回false下载失败
                            //frmUTSOFTMAIN.ErrMsg = ex.Message;
                        }
                    }
                }
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net;
using System.IO;
using System.Diagnostics;
using Common;
using log4net;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace VideoRecordings
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public bool LogSucceed=false;
        public Login()
        {
            InitializeComponent();
            textBox_name.Text = Program.GetAppConfig("LogName");
            if (!string.IsNullOrEmpty(Program.LogPassWord))
            {
                checkBox1.Checked = true;
                textBox_passward.Text = Program.LogPassWord;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Program.Version == "test")
            {
                textBox_name.Text = "xiekai";
                textBox_passward.Text = "xk";
                button1.PerformClick();
            }
        }

        /// <summary>
        /// 获取cookies并返回当前用户名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.1.225:19886/api/my/info";           
            string name = textBox_name.Text == string.Empty ? string.Empty : textBox_name.Text.Trim();
            string passWord = textBox_passward.Text == string.Empty ? string.Empty : textBox_passward.Text.Trim();
            if (name == string.Empty || passWord == string.Empty)
            {
                MessageBox.Show("账号或者密码不能为空");
                textBox_passward.Focus();
                return;
            }
            WebClinetHepler.Cookies = Logining(name,passWord);
            if (WebClinetHepler.Cookies==string.Empty)
            {
                MessageBox.Show("账号或者密码错误");
                Program.log.Error("账号登录",new Exception($"{textBox_name.Text}登录失败,账号密码错误"));
                textBox_passward.Focus();
                return;
            }
            LogSucceed = true;
            JObject obj = WebClinetHepler.GetJObject(url);
            Program.UserName = obj["result"]["real_name"].ToString() ;
            Program.LogName = obj["result"]["name"].ToString();
            Program.UpdataLongName();
            UpdataLongPassWord(passWord,checkBox1.Checked);
            this.Close();
           
        }

        public static void UpdataLongPassWord(string text,bool isupdate)
        {
            if (!isupdate)
            {
                text = string.Empty;
            }
            if (text != Program.GetAppConfig("PassWord"))
            {
                Program.UpdateAppConfig("PassWord", text);
            }
        }

        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        private string Logining(string name,string passWord)
        {
            string url = $"http://192.168.1.222:18281/login";
            HttpWebRequest request = null;
            try
            {
                request = WebRequest.CreateHttp(url);
                request.Method = "POST";
                request.Timeout = 5000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.CookieContainer = new CookieContainer();
                string content = $"name={name}&pwd={passWord}";
                using (var sw = new StreamWriter(request.GetRequestStream()))
                {
                    sw.Write(content);
                    sw.Close();
                }

                Stopwatch watch = new Stopwatch();
                watch.Start();
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    watch.Stop();
                    Console.WriteLine(
                        $"POST->{watch.ElapsedMilliseconds}ms,code:{(int)(response?.StatusCode ?? 0)},url:{url}");
                    return response?.Cookies["sessionid"]?.Value ?? string.Empty;
                }
            }
            finally
            {               
                request?.Abort();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    button1.PerformClick();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

}
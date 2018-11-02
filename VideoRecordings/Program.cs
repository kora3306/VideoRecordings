using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Common;
using System.Reflection;
using log4net;
using Manina.Windows.Forms;
using System.Threading;
using System.Net;
using System.Web.Script.Serialization;
using VideoRecordings.Models;
using Dal;
using VideoRecordings.GetDatas;
using System.Text;

namespace VideoRecordings
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //全局异常处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Directory.CreateDirectory("Log");
            if(!AppSettings.Initialize())
            {
                MessageBox.Show("配置文件出错");
                return;
            }
            CheckedFile();
            Checkconfiguration();
            if (!AppSettings.IsTest)
                CheckUpdate();
            Login log = new Login();
            log.ShowDialog();
            if (!log.LogSucceed) return;
            Application.Run(new FileManagement());
         
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
            
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }

        public static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static TypeLabels labels = new TypeLabels();

        private static void Checkconfiguration()
        {
            try
            {
                foreach (var item in AppSettings.Connects)
                {
                    Connect(item.Path, item.UserNmae, item.PassWord);
                }
                if (!Directory.Exists(AppSettings.ImageSavePath))
                {
                    Directory.CreateDirectory(AppSettings.ImageSavePath);
                }
                CopyConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// 检查文件数量，并删除5天前的文件
        /// </summary>
        /// <param name="log"></param>
        private static void CheckedFile()
        {
            try
            {
                string[] filePaths = Directory.GetFiles("Log");
                foreach (string filePath in filePaths)
                {
                    FileInfo file = new FileInfo(filePath);
                    if (file.CreationTime.AddDays(5) < DateTime.Now)
                        file.Delete();
                }
            }
            catch (Exception ex)
            {
                log.Error("删除文件出错" + ex);
            }
        }

        /// <summary>
        /// 检查更新版本，有的话选择更新
        /// </summary>
        /// <returns></returns>
        private static bool CheckUpdate()
        {
            CheckUpdaterUpdate();
            if (WebClinetHepler.Get($"{AppSettings.UpdateApi}/api/VideoRecordings/verson") == AppSettings.Version ||
                !File.Exists("Updater.exe"))
                return false;
            if (MessageBox.Show("有新版本需要更新？", @"提示", MessageBoxButtons.OKCancel) != DialogResult.OK)
                return true;
            //ImageListViewCacheThumbnail.DiskCache.Clear();
            var p = new Process
            {
                StartInfo =
                {
                    FileName = Path.Combine(Application.StartupPath, "Updater.exe"),
                    Arguments =
                        "\"VideoRecordings\" " +
                        "\"VideoRecordings.exe\" " +
                        "\"http://192.168.1.222:18820\" " +
                         "*.bat|LabelConfig.json|update server/|Log/|cache/|config.json|*.txt",
                    UseShellExecute = true
                }
            };
            p.Start();
            Environment.Exit(0);
            return true;
        }

        /// <summary>
        /// 每过一个小时检查一次是否有新版本，有的话提示
        /// </summary>
        private static void CheckUpdateThread()
        {
            DateTime now = DateTime.Now;
            int hours = now.Hour;
            while (true)
            {
                Thread.Sleep(1000);
                now = DateTime.Now;
                if (hours == now.Hour)
                    continue;
                hours = now.Hour;
                CheckUpdate();
            }
        }

        /// <summary>
        /// 检查updater更新
        /// </summary>
        private static void CheckUpdaterUpdate()
        {
            string newversion = WebClinetHepler.Get($"{AppSettings.UpdateApi}/api/Updater/verson");
            if (File.Exists("Updater.exe") &&
                FileVersionInfo.GetVersionInfo("Updater.exe").FileVersion == newversion)
                return;
            using (var webclient = new WebClient())
            {
                File.Delete("Updater.exe");
                foreach (var temp in (Dictionary<string, object>)new JavaScriptSerializer().DeserializeObject(
                    WebClinetHepler.Get($"{AppSettings.UpdateApi}/api/Updater/{newversion}/filelist")))
                {
                    webclient.DownloadFile($"{AppSettings.UpdateApi}/api/{temp.Value}", temp.Key);
                }
            }
        }


        /// <summary>
        /// 复制解码库
        /// </summary>
        public static void CopyConfig()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string newpath = @"C:\Users\Public\Thunder Network\APlayer\codecs";
            Directory.CreateDirectory(newpath);
            string oldpath = @".\codecs";
            Methods.CopyDirectory(oldpath, newpath);
            stopwatch.Stop();
            long time = stopwatch.ElapsedMilliseconds;
        }


        /// <summary>
        /// 连接共享文件夹
        /// </summary>
        /// <param name="remoteHost"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static bool Connect(string remoteHost, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + remoteHost + " " + passWord + " /User:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }

    }

}

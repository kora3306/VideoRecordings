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
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Directory.CreateDirectory("Log");
            CheckedFile();
            Checkconfiguration();
            Application.Run(new Login());
        }

       public static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string VideoPlay;
        public const string PlayerName = "PlayerPath";    //储存的播放器路径
        /// <summary>
        /// 接口
        /// </summary>
        public static string Urlpath = "http://192.168.1.198:16080";
        //public const string Urlpath = "http://192.168.1.225:18080";
        public static List<string> labels = new List<string>();


        public const string PathUrl = @"\\192.168.1.158";
        /// <summary>
        /// 截图存储位置
        /// </summary>
        public static string ImageSavePath;
        public const string ImageName = "ImageName";

        public static string Cookies = string.Empty;

        public static string UserName = string.Empty;
        public static string LogName = string.Empty;
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string strKey)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value.ToString();
                }
            }
            return null;
        }

        ///<summary>  
        ///在*.exe.config文件中appSettings配置节增加一对键值对  
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            string file = System.Windows.Forms.Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            bool exist = false;
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == newKey)
                {
                    exist = true;
                }
            }
            if (exist)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
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
                string dosLine = "net use " + remoteHost + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (String.IsNullOrEmpty(errormsg))
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

        /// <summary>
        /// 根据文件名判断共享文件路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReturnStringUrl(string url)
        {
            string Url1 = @"\\192.168.1.158";
            string Url2 = @"\\192.168.1.198";
            string Url3 = @"\\192.168.1.234";

            List<string> list = url.Split('\\').ToList();
            switch (list[1])
            {
                case "msdata":
                    return Url1 + url;
                case "md0":
                case "md1":
                case "md2":
                case "md3":
                case "md4":
                    return Url2 + @"\iscdata" + url;
                case "md5":
                case "md6":
                    return Url3 + url;
                default:
                    break;
            }
            return string.Empty;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="destPath"></param>
        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        if (!File.Exists(destPath + "\\" + i.Name))
                        {
                            File.Copy(i.FullName, destPath + "\\" + i.Name, true);
                        }
                        //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 复制解码库
        /// </summary>
        public static void CopyConfig()
        {
            string newpath = @"C:\Users\Public\Thunder Network\APlayer\codecs";
            Directory.CreateDirectory(newpath);
            string oldpath = @".\codecs";
            CopyDirectory(oldpath, newpath);
        }

        public static void UpdataLongName()
        {
            if (LogName != GetAppConfig("LogName"))
            {
                UpdateAppConfig("LogName", LogName);
            }
        }

        private static void Checkconfiguration()
        {
            bool open = Connect(PathUrl, "leets", "songnana1234");
            bool open1 = Connect("\\\\192.168.1.234", "work", "test234");
            VideoPlay = GetAppConfig(PlayerName);
            ImageSavePath = GetAppConfig(ImageName);
            if (GetAppConfig("TestApi") == "0")
            {
                Urlpath = GetAppConfig("UrlPath");
            }
            else
            {
                Urlpath = GetAppConfig("TestPath");
            }
            string savepath = Directory.GetCurrentDirectory() + "\\" + "ScreenCapture";
            if (ImageSavePath != savepath)
            {
                ImageSavePath = savepath;
                UpdateAppConfig(ImageName, ImageSavePath);
            }
            if (!Directory.Exists(ImageSavePath))
            {
                Directory.CreateDirectory(ImageSavePath);
            }
            CopyConfig();
            if (!open)
            {
                log.Error("连接服务器",new Exception("158连接失败"));
            }
            if (!open1)
            {
                log.Error("连接服务器", new Exception("158连接失败"));
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
    }
}

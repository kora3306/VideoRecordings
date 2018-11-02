using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.Models;

namespace VideoRecordings
{
    public static class AppSettings
    {
        /// <summary>
        /// 播放器存储路径
        /// </summary>
        public static List<VideoPath> VideoPlayPath;

        public readonly static List<string> Paths = new List<string>() { "StormPlayer", "KMPIAYER", "XMP", "VLC" };

        public readonly static string PlayerPath =Path.Combine(Application.StartupPath,"VideoPlayPath.txt");    //储存的播放器路径文件

        /// <summary>
        /// 使用接口,端口
        /// </summary>
        public static string Urlpath;

        // 视频存放路径 登录账号密码
        public static string PathUrl158 = @"\\192.168.1.158";
        public static string PathUrl198 = @"\\192.168.1.198";
        public static string PathUrl234 = @"\\192.168.1.234";

        public static string UserName158 = "leets";
        public static string UserNmae198 = "work";
        public static string UserName234 = "work";

        public static string PassWord158 = "songnana1234";
        public static string PassWord198 = "test198";
        public static string PassWord234 = "test234";

        public static List<Connect> Connects = new List<Connect>()
        {
            new Connect(PathUrl158,UserName158,PassWord158),
            new Connect(PathUrl198,UserNmae198,PassWord198),
            new Connect(PathUrl234,UserName234,PassWord234)
        };
        public static PersistentCache Persistent = new PersistentCache(@".\cache", 100000000000);

        /// <summary>
        /// 截图存储位置
        /// </summary>
        public static string ImageSavePath;
        public const string ImageName = "ImageName";

        public static string Version;
        public static int TestApi;
        public static string UpdateApi;

        public static User User;
        public static string LogPassWord;

        public static bool IsTest
        {
            get => Version == "test";
        }

        public static bool IsTestApi
        {
            get => TestApi == 1;
        }

        public static bool Initialize()
        {
            try
            {
                VideoPlayPath = Methods.ReadPath(PlayerPath);
                ImageSavePath = GetAppConfig(ImageName);
                TestApi = int.Parse(GetAppConfig("TestApi"));
                if (!IsTestApi)
                    Urlpath = GetAppConfig("UrlPath");
                else
                    Urlpath = GetAppConfig("TestPath");
                Version = GetAppConfig("Version");
                UpdateApi = GetAppConfig("UpdateApi");
                LogPassWord = GetAppConfig("PassWord");
                string savepath = Directory.GetCurrentDirectory() + "\\" + "ScreenCapture";
                if (ImageSavePath != savepath)
                {
                    ImageSavePath = savepath;
                    UpdateAppConfig(ImageName, ImageSavePath);
                }

            }
            catch
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 更新登录名
        /// </summary>
        public static void UpdataLongName()
        {
            if (User.Name != GetAppConfig("LogName"))
            {
                UpdateAppConfig("LogName", User.Name);
            }

        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string strKey)
        {
            string file = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                if (key == strKey)
                {
                    return config.AppSettings.Settings[strKey].Value;
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
            string file = Application.ExecutablePath;
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
        /// 根据文件名判断共享文件路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ReturnStringUrl(string url)
        {
            List<string> list = url.Split('\\').ToList();
            switch (list[1])
            {
                case "msdata":
                    return PathUrl158 + url;
                case "md0":
                case "md1":
                case "md2":
                case "md3":
                case "md4":
                    return  PathUrl198+ @"\iscdata" + url;
                case "md5":
                case "md6":
                case "md7":
                case "md8":
                case "md9":
                case "md10":
                case "md11":
                    return PathUrl234 + url;
                default:
                    break;
            }
            return string.Empty;
        }
    }



    /// <summary>
    /// 连接共享文件夹
    /// </summary>
    public class Connect
    {
        public string Path { get; set; }
        public string UserNmae { get; set; }
        public string PassWord { get; set; }

        public Connect(string path,string name,string password)
        {
            Path = path;
            UserNmae = name;
            PassWord = password;
        }     
    }

}

using Common;
using Manina.Windows.Forms;
using MyControl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoRecordings.Models;

namespace VideoRecordings
{
    public class Methods
    {

        /// <summary>
        /// 路径定位文件夹
        /// </summary>
        /// <param name="fileFullName"></param>
        public static void OpenFolderAndSelectFile(String fileFullName)
        {
            ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe")
            {
                Arguments = "/e,/select," + fileFullName
            };
            Process.Start(psi);
        }

        /// <summary>
        /// 将选中图片加入控件显示
        /// </summary>
        public static void ShowImage(ImageListView image)
        {
            if (image.Items.Count == 0)
            {
                return;
            }
            int i = 0;
            int index = 0;
            List<ImageListViewItem> showimages = new List<ImageListViewItem>();
            foreach (var item in image.Items)
            {
                if (item.Selected)
                {
                    index = i;
                }
                showimages.Add(item);
                i++;
            }
            new ShowImage(showimages, index).Show();
        }

        public static void AddIsTest(Form form)
        {
            if (AppSettings.IsTestApi)
                form.Text += "(测试库)";
            form.Text += $"(Version:{AppSettings.Version})";
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        public static void DelImage(ImageListView image, List<string> imageurl)
        {
            string url = AppSettings.Urlpath + "/video/snapshot/";
            if (image.SelectedItems.Count == 0)
            {
                return;
            }
            foreach (var item in image.SelectedItems)
            {
                JObject obj = WebClinetHepler.Delete_New(url + item.Text.Split('/').Last());
                if (obj == null)
                {
                    MessageBox.Show(url + item.Text.Split('/').Last() + "删除失败");
                    return;
                }
                image.Items.Remove(item);
                imageurl.Remove(url + item.Text.Split('/').Last());
            }
            Program.log.Error($"删除图片{url}");
        }

        public static VideoPlay GetNewImages(int index)
        {
            string url = AppSettings.Urlpath + $"/videos?id={index}";
            JObject obj = WebClinetHepler.GetJObject(url);
            VideoPlay videoplay = new VideoPlay();
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                Projects project = JsonHelper.DeserializeDataContractJson<Projects>(obj["result"][0]["project"].ToString());
                EquipmentInfo info = JsonHelper.DeserializeDataContractJson<EquipmentInfo>(obj["result"][0]["equipments"][0]["equipment_info"].ToString());
                videoplay = JsonHelper.DeserializeDataContractJson<VideoPlay>(obj["result"][0]["equipments"][0]["videos"][0].ToString());
                videoplay.Project = project;
                videoplay.Rquipment = info;
            }
            return videoplay;
        }


        public static List<VideoPath> ReadPath(string path)
        {
            if (!File.Exists(path))
            {
                return new List<VideoPath>();
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string read = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return JsonConvert.DeserializeObject<List<VideoPath>>(read);
        }

        public static void WritePath(string path, List<VideoPath> text)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(JsonConvert.SerializeObject(text));
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 转换路径中的/为\
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ConversionString(string path)
        {
            string[] n = path.Split('/');
            if (n.Length > 1)
            {
                string Ret = string.Empty;
                for (int i = 0; i < n.Length; i++)
                {
                    Ret += n[i] + "\\";
                }
                return Ret.Substring(0, Ret.Length - 1);
            }
            return path;
        }


        //public TypeLabel GetNodeToTypeLabel(TreeNode tree)
        //{
        //    TypeLabel type = new TypeLabel(){Name=tree.Text,Id=int.Parse(tree.Tag.ToString())};
        //    List<VideoLabel> videos = new List<VideoLabel>();
        //    foreach (TreeNode node in tree.Nodes)
        //    {
        //        videos.Add(GetNodeToVideoLabel(node));
        //    }

        //    type.Labels = videos;
        //    return type;
        //}

        public static VideoLabel GetNodeToVideoLabel(TreeNode node)
        {
            return new VideoLabel() { Id = int.Parse(node.Tag.ToString()), Name = node.Text };
        }

        public static TypeLabel GetNodeToTypeLabel(TreeNode node)
        {
            return new TypeLabel() { Id = int.Parse(node.Tag.ToString()), Name = node.Text };
        }



        /// <summary>
        /// 反射获取窗体
        /// </summary>
        /// <param name="form"></param>
        /// <param name="sender"></param>
        public void GenerateForm(string form, object sender)
        {
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.BringToFront();
            fm.TopLevel = false;
            fm.Parent = (Panel)sender;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();
        }


        /// <summary>
        /// 判断获取加载的窗体
        /// </summary>
        /// <param name="index"></param>
        public void SetForm(string formClass, string name, TabControlEx tabControlExHfrz)
        {
            TabPage tabPageMapping = new TabPage(name) { Name = name };
            tabControlExHfrz.TabPages.Add(tabPageMapping);
            GenerateForm(formClass, tabPageMapping);
        }

        public static List<string> IdConvetToPath(List<int> ids)
        {
            List<string> imageurl = new List<string>();
            string url = AppSettings.Urlpath + "/video/snapshot/";
            foreach (var item in ids)
            {
                imageurl.Add(url + item);
            }
            return imageurl;
        }


        // 利用反射实现深拷贝
        public static T DeepCopyWithReflection<T>(T obj)
        {
            Type type = obj.GetType();

            // 如果是字符串或值类型则直接返回
            if (obj is string || type.IsValueType) return obj;

            if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopyWithReflection(array.GetValue(i)), i);
                }

                return (T)Convert.ChangeType(copied, obj.GetType());
            }

            object retval = Activator.CreateInstance(obj.GetType());

            PropertyInfo[] properties = obj.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static);
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj, null);
                if (propertyValue == null)
                    continue;
                property.SetValue(retval, DeepCopyWithReflection(propertyValue), null);
            }

            return (T)retval;
        }

        // 利用DataContractSerializer序列化和反序列化实现
        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = ser.ReadObject(ms);
                ms.Close();
            }
            return (T)retval;
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
                throw e;
            }
        }
 

    }

}

using Common;
using Manina.Windows.Forms;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        /// 将所有图片加入控件显示
        /// </summary>
        /// <param name="image"></param>
        public static void ShowListImages(ImageListView image)
        {
            if (image.Items.Count == 0)
            {
                return;
            }
            List<ImageListViewItem> showimages = new List<ImageListViewItem>();
            foreach (var item in image.Items)
            {
                showimages.Add(item);
            }
            new ShowImage(showimages).ShowDialog();
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
            List<ImageListViewItem> showimages = new List<ImageListViewItem>();
            foreach (var item in image.Items)
            {
                showimages.Add(item);
            }
            new ShowImage(showimages).ShowDialog();

        }

        /// <summary>
        /// 删除图片
        /// </summary>
        public static void DelImage(ImageListView image,List<string> imageurl)
        {
            string url = Program.Urlpath + "/video/snapshot/";
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
    }
}

using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRecordings
{
    class GetData
    {
        /// <summary>
        /// 获取所有文件夹信息,转化成类集合
        /// </summary>
        /// <param name="url"></param>
        /// <param name="na"></param>
        public static List<VideoProject> GetListVideo(string url, string na)
        {
            List<VideoProject> project = new List<VideoProject>();
            try
            {
                List<string> datajson = new List<string>();
                JObject obj = WebClinetHepler.GetJObject(url);
                if (obj == null) return null;
                project = JsonHelper.DeserializeDataContractJson<List<VideoProject>>(obj[$"{na}"].ToString());
                project = project.OrderBy(t => t.Name).ToList();
                return project;
            }
            catch (Exception ex)
            {
                Program.log.Error("获取文件夹信息", ex);
                throw;
            }
        }

        public static List<VideoProject> GetAllFolder(string name = null)
        {
            string getpath = Program.Urlpath + "/video/projects";
            if (name != null)
            {
                getpath += $"?name={name}";
            }
            return GetListVideo(getpath, "video_projects");
        }

        public static List<EquipmentInfo> GetEquipment(string project_name)
        {
            string url = Program.Urlpath + $"/video/equipments?project_name={project_name}";
            JObject obj = WebClinetHepler.GetJObject(url);
            List<EquipmentInfo> equipments = JsonHelper.DeserializeDataContractJson<List<EquipmentInfo>>(obj["result"].ToString());
            return equipments;
        }

        public static bool AddEquipment(string project_name, string name)
        {
            string url = Program.Urlpath + "/video/equipment";
            var equipmengt = new
            {
                project_name,
                name
            };
            string json = JsonObject.Serialize(equipmengt);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        public static bool UpdateEquipmengt(int id, string name)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}";
            var equipmengt = new
            {
                name
            };
            string json = JsonObject.Serialize(equipmengt);
            JObject obj = WebClinetHepler.Patch_New(url, json);
            return obj != null;
        }

        public static bool DeleteEquipmengt(int id)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}";
            JObject obj = WebClinetHepler.Delete_New(url);
            return obj != null;
        }

        public static bool AddVideoInEquipment(int id, List<int> ids)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}/add/videos";
            var postids = new
            {
                video_ids = ids
            };
            string json = JsonObject.Serialize(postids);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        public static bool DelteVideosFromEquipment(int id,List<int> ids)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}/remove/videos";
            var postids = new
            {
                video_ids = ids
            };
            string json = JsonObject.Serialize(postids);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        /// <summary>
        /// 扫描文件夹
        /// </summary>
        /// <param name="focusedfolder"></param>
        /// <returns></returns>
        public static bool ScanFolder(VideoProject focusedfolder)
        {
            string posturl = Program.Urlpath + "/scan/video/project/" + focusedfolder.Id.ToString();
            string conditions = "project_name=" + focusedfolder.Name;
            JObject obj = WebClinetHepler.Post_New(posturl);
            return obj == null;          
        }

        public static bool BatchAddLabels(List<int> ids, List<int> label)
        {
            string url = Program.Urlpath + "/bulk/add/video/labels";
            var str = new
            {
                videos = ids,
                labels = label
            };
            string json = JsonObject.Serialize(str);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        /// <summary>
        /// 所有标签信息
        /// </summary>
        /// <param name="LabelsNumber"></param>
        /// <param name="LabelAll"></param>
        /// <returns></returns>
        public static List<TreeNode> GetLabels(out Dictionary<string, string> LabelsNumber, out Dictionary<string, string> LabelAll)
        {
            LabelsNumber = new Dictionary<string, string>();
            LabelAll = new Dictionary<string, string>();
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null || obj["result"] == null || obj["result"].Count() == 0)
            {
                return null;
            }
            List<TreeNode> items = new List<TreeNode>();
            for (int i = 0; i < obj["result"].Count(); i++)
            {
                TreeNode tree = new TreeNode
                {
                    Text = obj["result"][i]["name"].ToString(),
                    ForeColor = Color.Green
                };
                LabelsNumber.Add(obj["result"][i]["id"].ToString(), obj["result"][i]["name"].ToString());
                for (int j = 0; j < obj["result"][i]["children"].Count(); j++)
                {
                    TreeNode node = new TreeNode
                    {
                        Text = obj["result"][i]["children"][j]["name"].ToString(),
                        Tag = obj["result"][i]["children"][j]["is_fre"].ToString(),
                        ForeColor = Color.Blue
                    };
                    LabelAll.Add(obj["result"][i]["children"][j]["id"].ToString(), obj["result"][i]["children"][j]["name"].ToString());
                    tree.Nodes.Add(node);
                }
                items.Add(tree);
            }
            return items;
        }
    }
}

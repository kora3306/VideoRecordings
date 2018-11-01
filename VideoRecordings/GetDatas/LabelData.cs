using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoRecordings.Models;


namespace VideoRecordings.GetDatas
{
    public class LabelData
    {
        /// <summary>
        /// 批量增加标签
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="label"></param>
        /// <returns></returns>
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

        public static AllTypeLabel GetAllLabel()
        {
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            AllTypeLabel AllLabel = JsonConvert.DeserializeObject<AllTypeLabel>(obj["result"].ToString());
            return AllLabel;
        }

        public static AllTypeLabel GetAllLabel(int id)
        {
            string url = Program.Urlpath + $"/labels?video={id}";
            JObject obj = WebClinetHepler.GetJObject(url);
            AllTypeLabel AllLabel = JsonConvert.DeserializeObject<AllTypeLabel>(obj["result"].ToString());
            return AllLabel;
        }

        public static bool UpdateLabelName(int id, string name)
        {
            string url = Program.Urlpath + $"/label/{id}";
            var up = new
            {
                name
            };
            JObject obj = WebClinetHepler.Patch_New(url, JsonConvert.SerializeObject(up));
            return obj != null;
        }

        public static bool DeleteLabel(int id)
        {
            string url = Program.Urlpath + $"/label/{id}";
            JObject obj = WebClinetHepler.Delete_New(url);
            return obj != null;
        }

        public static bool AddLabels(int parent_id, List<string> names, int type, int label_ref)
        {
            string url = Program.Urlpath + "/labels";
            List<object> dics = new List<object>();
            foreach (var item in names)
            {
                Dictionary<string, object> diclabel = new Dictionary<string, object>();
                diclabel.Add("parent_id", parent_id);
                diclabel.Add("label_ref", label_ref);
                diclabel.Add("name", item);
                diclabel.Add("type", type);
                dics.Add(diclabel);
            }
            JObject obj = WebClinetHepler.Post_New(url, JsonConvert.SerializeObject(dics));
            return obj != null;
        }

        /// <summary>
        /// 视频打标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public static async Task<bool> AddLabelToVideoAsync(int id, List<int> labels)
        {
            string url = Program.Urlpath + $"/video/{id}/labels";
            JObject obj = await WebClinetHepler.PostAsync(url, JsonConvert.SerializeObject(labels)).ConfigureAwait(false);
            return obj != null;
        }

        /// <summary>
        /// 标签添加到通道
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        public static bool AddLabelToEquipment(int id, List<int> labels)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}/add/labels";
            JObject obj = WebClinetHepler.Post_New(url, JsonConvert.SerializeObject(labels));
            return obj != null;
        }

        public static TypeLabels GetLabelToEquipment(int id)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            return JsonConvert.DeserializeObject<TypeLabels>(obj["result"].ToString());
        }

        public static string GetListVideoLabels(int id)
        {
            List<TypeLabel> typeLabels = GetLabelToEquipment(id);
            List<VideoLabel> videos = new List<VideoLabel>();
            foreach (TypeLabel item in typeLabels)
            {
                videos.AddRange(item.Labels);
            }
            return string.Join(",", videos);
        }

        /// <summary>
        /// 设置关联
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="label_ref"></param>
        /// <returns></returns>
        public static bool RelevanceLabel(List<int> ids, int label_ref)
        {
            string url = Program.Urlpath + $"/labels/set/ref";
            var json = new
            {
                ids,
                label_ref
            };
            JObject obj = WebClinetHepler.Post_New(url, JsonConvert.SerializeObject(json));
            return obj != null;
        }

        public static bool SetLabelType(List<int> ids, bool isstatic)
        {
            string url = Program.Urlpath + $"/labels/set/type";
            int label_type = isstatic ? 1 : 0;
            var json = new
            {
                ids,
                label_type
            };
            JObject obj = WebClinetHepler.Post_New(url, JsonConvert.SerializeObject(json));
            return obj != null;
        }
    }
}

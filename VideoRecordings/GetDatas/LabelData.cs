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

        public static List<TypeLabel> GetAllLabel()
        {
            string url = Program.Urlpath + "/labels";
            JObject obj = WebClinetHepler.GetJObject(url);
            List<TypeLabel> AllLabel = JsonConvert.DeserializeObject<List<TypeLabel>>(obj["result"].ToString());
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

        public static bool AddLabels(int parent_id, List<string> names)
        {
            string url = Program.Urlpath + "/labels";
            List<object> dics = new List<object>();
            foreach (var item in names)
            {
                Dictionary<string, object> diclabel = new Dictionary<string, object>();
                diclabel.Add("parent_id", parent_id);
                diclabel.Add("name", item);
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
        public static bool AddLabelToVideo(int id, List<int> labels)
        {
            string posturl = Program.Urlpath + $"/video/{id}/labels";
            JObject obj = WebClinetHepler.Post_New(posturl, JsonConvert.SerializeObject(labels));
            return obj != null;
        }
    }
}

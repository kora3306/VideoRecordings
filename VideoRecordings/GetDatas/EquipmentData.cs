using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoRecordings.Models;

namespace VideoRecordings.GetDatas
{
    public class EquipmentData
    {

        public static MyGroup GetAlEquipment()
        {
            string url = Program.Urlpath + $"/video/equipments";
            JObject obj = WebClinetHepler.GetJObject(url);
            MyGroup equipments = JsonConvert.DeserializeObject<MyGroup>(obj["result"].ToString());
            return equipments;
        }

        public static bool AddEquipment(string city, string street,string site,string uid)
        {
            string url = Program.Urlpath + "/video/equipment";
            var equipmengt = new
            {
                city,
                street,
                site,
                uid
            };
            string json = JsonObject.Serialize(equipmengt);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        public static bool UpdateEquipmengt(int id, string city, string street, string site, string uid)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}";
            var equipmengt = new
            {
                city,
                street,
                site,
                uid
            };
            string json = JsonObject.Serialize(equipmengt);
            JObject obj = WebClinetHepler.Patch_New(url, json);
            return obj != null;
        }

        public static bool DeleteEquipmengt(int id)
        {
            string url = Program.Urlpath + $"/video/equipments";
            List<int> ids = new List<int>(){id};
            JObject obj = WebClinetHepler.Delete_New(url,JsonConvert.SerializeObject(ids));
            return obj != null;
        }

        public static bool DeleteEquipmengt(List<int> ids)
        {
            string url = Program.Urlpath + $"/video/equipments";
            JObject obj = WebClinetHepler.Delete_New(url, JsonConvert.SerializeObject(ids));
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

        public static bool DelteVideosFromEquipment(int id, List<int> ids)
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

        public static int GetImageId(int id)
        {
            string url = Program.Urlpath + $"/video/equipment/{id}/snapshot";
            JObject obj = WebClinetHepler.GetJObject(url);

            return obj["result"] != null?int.Parse(obj["result"].ToString()) :0;
        }

    }
}

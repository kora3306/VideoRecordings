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
        public static string Url = AppSettings.Urlpath;

        public static MyGroup GetAlEquipment()
        {
            string url = Url + $"/video/equipments";
            JObject obj = WebClinetHepler.GetJObject(url);
            MyGroup equipments = JsonConvert.DeserializeObject<MyGroup>(obj["result"].ToString());
            return equipments;
        }

        public static bool AddEquipment(string city, string street,string site,string uid)
        {
            string url = Url + "/video/equipment";
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
            string url = Url + $"/video/equipment/{id}";
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
            string url = Url + $"/video/equipments";
            List<int> ids = new List<int>(){id};
            JObject obj = WebClinetHepler.Delete_New(url,JsonConvert.SerializeObject(ids));
            return obj != null;
        }

        public static bool DeleteEquipmengt(List<int> ids)
        {
            string url = Url + $"/video/equipments";
            JObject obj = WebClinetHepler.Delete_New(url, JsonConvert.SerializeObject(ids));
            return obj != null;
        }

        public static bool AddVideoInEquipment(int id, List<int> ids)
        {
            string url = Url + $"/video/equipment/{id}/add/videos";
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
            string url = Url + $"/video/equipment/{id}/remove/videos";
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
            string url = Url + $"/video/equipment/{id}/snapshot";
            JObject obj = WebClinetHepler.GetJObject(url);

            return obj["result"] != null?int.Parse(obj["result"].ToString()) :0;
        }

        public static EquipmentInfo GetEquipmentById(int id)
        {
            string url = Url + $"/video/equipments?id={id}";
            JObject obj = WebClinetHepler.GetJObject(url);
            return JsonConvert.DeserializeObject<EquipmentInfo>(obj["result"]["equipments"][0].ToString());
        }

    }
}

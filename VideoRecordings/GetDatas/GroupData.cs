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
    public class GroupData
    {

        public static string Url = AppSettings.Urlpath;
        /// <summary>
        /// 获取通道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<GalleryGroup> GetAllGalleryGroup(string name = null)
        {
            string url = Url + "/video/equipments/by/groups";
            if (name != null)
            {
                url += $"?name={name}";
            }
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null) return new List<GalleryGroup>();
            List<GalleryGroup> galleryGroups = JsonConvert.DeserializeObject<List<GalleryGroup>>(obj["result"].ToString());
            return galleryGroups;
        }

        /// <summary>
        /// 获取条件通道
        /// </summary>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="site"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static MyGroup GetGroupShows(string city = null, string street = null, string site = null, string uid = null)
        {
            string url = Url + "/video/equipments";
            if (!string.IsNullOrEmpty(city)) url += $"?city={city}";
            if (!string.IsNullOrEmpty(street)) url += $"&street={street}";
            if (!string.IsNullOrEmpty(site)) url += $"&site={site}";
            if (!string.IsNullOrEmpty(uid)) url += $"&uid={uid}";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null) return null;
            MyGroup show = JsonConvert.DeserializeObject<MyGroup>(obj["result"].ToString());
            return show;
        }

        /// <summary>
        /// 通道加入分组
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool AddEquipmentToGroup(int id, List<int> ids)
        {
            string url = Url + $"/video/equipment/group/{id}/add/equipments";
            Dictionary<string, List<int>> jsondic = new Dictionary<string, List<int>>();
            jsondic.Add("equip_ids", ids);
            string json = JsonConvert.SerializeObject(jsondic);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        public static bool DeleteEquipmentToGroup(int id, List<int> ids)
        {
            string url = Url + $"/video/equipment/group/{id}/remove/equipments";
            Dictionary<string, List<int>> jsondic = new Dictionary<string, List<int>>();
            jsondic.Add("equip_ids", ids);
            string json = JsonConvert.SerializeObject(jsondic);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool AddGroup(string name)
        {
            string url = Url + $"/video/equipment/group";
            List<Dictionary<string, object>> jsondics = new List<Dictionary<string, object>>();
            Dictionary<string, object> jsondic = new Dictionary<string, object>();
            jsondic.Add("name", name);
            jsondic.Add("parent_id", 0);
            jsondics.Add(jsondic);
            string json = JsonConvert.SerializeObject(jsondics);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteGroup(int id)
        {
            string url = Url + $"/video/equipment/group/{id}";
            JObject obj = WebClinetHepler.Delete_New(url);
            return obj != null;
        }

        public static bool UpdateGroup(int id ,string name)
        {
            string url = Url + $"/video/equipment/group/{id}";
            Dictionary<string, object> jsondic = new Dictionary<string, object>();
            jsondic.Add("name", name);
            jsondic.Add("parent_id", 0);
            string json = JsonConvert.SerializeObject(jsondic);
            JObject obj = WebClinetHepler.Patch_New(url, json);
            return obj != null;
        }
    }
}

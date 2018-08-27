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
    public class VideoData
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
            return obj != null;
        }

        /// <summary>
        /// 重新扫描
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool RefreshFolder(int id)
        {
            string url = Program.Urlpath + $"/rescan/video/project/{id}";
            JObject obj = WebClinetHepler.Post_New(url);
            if (obj?["message"].ToString() == "not scaned") return false;
            return obj != null;
        }

        public static bool DeleteVideo(int id)
        {
            string url = Program.Urlpath + $"/video/{id}";
            JObject obj = WebClinetHepler.Delete_New(url);
            return obj != null;
        }

        public static JObject GetAllVideoInfo(string projectName)
        {
            string geturl = Program.Urlpath + $"/videos?project_name={projectName}";
            JObject obj = WebClinetHepler.GetJObject(geturl);
            return obj;
        }

        public static VideoPlay GetIndexVideoInfo(int id)
        {
            string geturl = Program.Urlpath + $"/videos?id={id}";
            JObject obj = WebClinetHepler.GetJObject(geturl);
            if (obj == null || obj["result"].ToString() == "[]") return null;
            List<VideoPlay> videoplays = new List<VideoPlay>();
            for (int i = 0; i < obj["result"][0]["equipments"].Count(); i++)
            {
                EquipmentInfo equipment = JsonHelper.DeserializeDataContractJson<EquipmentInfo>(obj["result"][0]["equipments"][i]["equipment_info"].ToString());
                List<VideoPlay> videos = JsonHelper.DeserializeDataContractJson<List<VideoPlay>>(obj["result"][0]["equipments"][i]["videos"].ToString());
                videos.ForEach(t => t.Rquipment = equipment);
                videoplays.AddRange(videos);
            }
            return videoplays.First();
        }

        public static List<VideoPlay> GetAllVideoPlay(string projectNmae)
        {
            string geturl = Program.Urlpath + $"/videos?project_name={projectNmae}";
            JObject obj = WebClinetHepler.GetJObject(geturl);
            List<VideoPlay> videoplays = new List<VideoPlay>();
            for (int i = 0; i < obj["result"][0]["equipments"].Count(); i++)
            {
                List<VideoPlay> videos = JsonConvert.DeserializeObject<List<VideoPlay>>(obj["result"][0]["equipments"][i]["videos"].ToString());
                videoplays.AddRange(videos);
            }
            return videoplays;
        }

        /// <summary>
        /// 添加查重
        /// </summary>
        /// <param name="name"></param>
        /// <param name="info"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool VideoRepetition(string name, string info, int id, List<int> ids)
        {
            string url = Program.Urlpath + $"/video/check";
            Repetition repetition = new Repetition(name, info, id, ids);
            JObject obj = WebClinetHepler.Post_New(url, JsonConvert.SerializeObject(repetition));
            return obj != null;
        }

        public static List<Repetitions> GetRepetitions(string name, string number = null)
        {
            string url = Program.Urlpath + $"/video/check/user/status?user_name={name}";
            if (number != null) url += $"&pull_number={number}";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null) return new List<Repetitions>();
            List<Repetitions> repetitions = JsonConvert.DeserializeObject<List<Repetitions>>(obj["data"]["result"].ToString());
            return repetitions;
        }

        public static List<ReturnRepetition> GetOutResult(int id)
        {
            string url = Program.Urlpath + $"/video/check/result?id={id}";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null) return null;
            return JsonConvert.DeserializeObject<List<ReturnRepetition>>(obj["data"]["result"].ToString());
        }

        public static bool DeleteRepetition(int id)
        {
            string url = Program.Urlpath + $"/video/check/result";
            string json = JsonConvert.SerializeObject(new Dictionary<string, int>() { { "id", id } });
            JObject obj = WebClinetHepler.Delete_New(url, json);
            return obj != null;
        }

        public static bool DeleteRepetitionVideo(List<int> ids)
        {
            string url = Program.Urlpath + $"/video/check";
            string json = JsonConvert.SerializeObject(new Dictionary<string, List<int>>() { { "video_ids", ids } });
            JObject obj = WebClinetHepler.Delete_New(url, json);
            return obj != null;
        }

        public static bool DeleteRepetitionVideo(int id)
        {
            List<int> ids = new List<int>();
            ids.Add(id);
            string url = Program.Urlpath + $"/video/check";
            string json = JsonConvert.SerializeObject(new Dictionary<string, List<int>>() { { "video_ids", ids } });
            JObject obj = WebClinetHepler.Delete_New(url, json);
            return obj != null;
        }


        public static bool SaveTime(int id, string json)
        {
            string url = Program.Urlpath + $"/video/{id}";
            JObject obj = WebClinetHepler.Patch_New(url, json);
            return obj != null;
        }

        public static bool SaveImage(int id, string json)
        {
            string url = Program.Urlpath + $"/video/{id}/snapshots";
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VideoRecordings.Models;
using VideoRecordings.Video;

namespace VideoRecordings.GetDatas
{
    public class SolutionData
    {
        private static string TestUrl = "http://192.168.1.224:16087";
        public static string Url =AppSettings.IsTestApi? TestUrl : AppSettings.Urlpath;

        /// <summary>
        /// 删除解帧信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteSolution(List<int> ids)
        {
            string url = Url + $"/deframe";
            //string url = "http://192.168.1.224:16087/deframe";
            string json = JsonConvert.SerializeObject(ids);
            JObject obj = WebClinetHepler.Delete_New(url, json);
            return obj!=null;
        }

        public static bool SolutionOfTheFrame(Solution solu)
        {
            //string url = "http://192.168.1.224:16087/deframe/task";
            string url = Url + $"/deframe/task";
            string json = JsonConvert.SerializeObject(solu);
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }

        public static List<BackSolution> GetFrame()
        {
            //string url = "http://192.168.1.224:16087/deframe/queue";
            string url = Url + $"/deframe/queue";
            JObject obj = WebClinetHepler.GetJObject(url);
            if (obj == null) return new List<BackSolution>();
            List<BackSolution> repetitions = JsonConvert.DeserializeObject<List<BackSolution>>(obj["result"].ToString());
            return repetitions;
        }

        public static bool QueueSolution(List<BackSolution> ids)
        {
            string url = Url + $"/topping/deframe";
            //string url = "http://192.168.1.224:16087/topping/deframe";
            string json = JsonConvert.SerializeObject(ids.Select(t => t.VideoId));
            JObject obj = WebClinetHepler.Post_New(url, json);
            return obj != null;
        }
    }
}

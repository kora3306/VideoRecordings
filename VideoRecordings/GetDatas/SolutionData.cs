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
        /// <summary>
        /// 删除解帧信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static bool DeleteSolution(List<int> ids)
        {
            string url = Program.Urlpath + $"/deframe";
            string json = JsonConvert.SerializeObject(ids);
            JObject obj = WebClinetHepler.Delete_New(url, json);
            if (obj != null)
            {
                BackDeleteSolution back = JsonConvert.DeserializeObject<BackDeleteSolution>(obj.ToString());
                int wincount = ids.Count - back.Deframing.Count - back.NotDeframe.Count - back.NotFound.Count;
                MessageShow messageShow = new MessageShow("成功清除视频解帧信息:", wincount, "队列中未完成解帧：", back.Deframing.Count
                    , "没有解帧信息：", back.NotDeframe.Count, "没有找到的视频：", back.NotFound.Count);
                messageShow.ShowDialog();
                //MessageBox.Show($"成功清除视频解帧信息:{wincount}个\r\n    " +
                //                $"队列中未完成解帧：{back.Deframing.Count}个\r\n    " +
                //                $"没有解帧信息：{back.NotDeframe.Count}个\r\n     " +
                //                $"没有找到文件：{back.NotFound.Count}个");
                return true;
            }
            return false;
        }

    }
}

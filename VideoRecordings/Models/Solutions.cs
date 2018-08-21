using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
   public class Solutions
    {

    }

    /// <summary>
    /// 解帧信息
    /// </summary>
    [DataContract]
    public class Solution
    {
        public Solution()
        {

        }

        public Solution(string uri, int id, int step)
        {
            VideoUri = uri;
            VideoId = id;
            Step = step;
        }

        [DataMember(Name = "video_uri")]
        public string VideoUri { get; set; }

        [DataMember(Name = "video_id")]
        public int VideoId { get; set; }

        [DataMember(Name = "step")]
        public int Step { get; set; }
    }

    /// <summary>
    /// 删除解帧信息返回
    /// </summary>
    [DataContract]
    public class BackDeleteSolution
    {
        [DataMember(Name = "deframing")]
        public List<int> Deframing { get; set; }

        [DataMember(Name = "not_deframe")]
        public List<int> NotDeframe { get; set; }

        [DataMember(Name = "not_found")]
        public List<int> NotFound { get; set; }

    }

    /// <summary>
    /// 解帧信息返回
    /// </summary>
    [DataContract]
    public class BackSolution
    {
        [DataMember(Name = "deframing")]
        public List<int> Deframing { get; set; }

        [DataMember(Name = "frame_exists")]
        public List<int> FrameExists { get; set; }

        [DataMember(Name = "not_found")]
        public List<int> NotFound { get; set; }

        [DataMember(Name = "wait_tasks")]
        public int WaitTasks { get; set; }
    }
}

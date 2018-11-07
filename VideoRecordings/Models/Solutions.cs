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
        public Solution(string taskname,string note, List<int> ids, int step,int top)
        {
            TaskName = taskname;
            Note = note;
            VideoIds = ids;
            Step = step;
            Top = top;
        }

        [DataMember(Name = "task_name")]
        public string TaskName { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "video_ids")]
        public List<int> VideoIds { get; set; }

        [DataMember(Name = "step")]
        public int Step { get; set; }

        [DataMember(Name = "top")]
        public int Top { get; set; }

        [DataMember(Name = "Id")]
        public int Id { get; set; }
    }


    /// <summary>
    /// 解帧信息返回
    /// </summary>
    [DataContract]
    public class BackSolution
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "priority")]
        public int Priority { get; set; }

        [DataMember(Name = "step")]
        public int Step { get; set; }

        [DataMember(Name = "task_name")]
        public string TaskName { get; set; }

        [DataMember(Name = "video_id")]
        public int VideoId { get; set; }

        [DataMember(Name = "video_uri")]
        public string VideoUri { get; set; }
    }
}

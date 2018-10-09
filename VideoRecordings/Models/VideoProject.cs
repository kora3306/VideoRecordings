using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    /// <summary>
    /// 视频文件夹信息
    /// </summary>
    [DataContract]
    public class VideoProject
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "place")]
        public string Place { get; set; }

        [DataMember(Name = "scenes")]
        public int Scenes { get; set; }

        [DataMember(Name = "scenes_name")]
        public string ScenesName { get; set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        [DataMember(Name = "video_count")]
        public int VideoCount { get; set; }

        public int Count { get => Statistic.Total; }

        [DataMember(Name = "replicator")]
        public string Replicator { get; set; }

        [DataMember(Name = "recorder")]
        public string Recorder { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name= "dup_checked")]
        public int DupChecked { get; set; }

        [DataMember(Name = "record_time")]
        public string RecordTime { get; set; }

        [DataMember(Name = "status_name")]
        public string Status { get; set; }


        [DataMember(Name = "statistic")]
        public Completeness Statistic { get; set; }

        public string Percent
        {
            get => (Convert.ToDouble(Statistic.Recorded) / Convert.ToDouble(Statistic.Total)).ToString("0.0%")
                   + $"({Statistic.Recorded}/{Statistic.Total})";
        }
    }

    /// <summary>
    /// 完成数量
    /// </summary>
    [DataContract]
    public class Completeness
    {
        [DataMember(Name = "deframed")]
        public int Deframed { get; set; }

        [DataMember(Name = "recorded")]
        public int Recorded { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class VideoPath
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }
    }

}

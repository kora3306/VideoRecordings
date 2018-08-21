using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    /// <summary>
    /// 图片文件夹信息
    /// </summary>
    [DataContract]
    public class ImageProject
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "place")]
        public string Place { get; set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        [DataMember(Name = "video_count")]
        public int ImageCount { get; set; }

        [DataMember(Name = "replicator")]
        public string Replicator { get; set; }

        [DataMember(Name = "recorder")]
        public string Recorder { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }

        [DataMember(Name = "record_tim")]
        public string RecordTime { get; set; }

        [DataMember(Name = "status_name")]
        public string Status { get; set; }

        [DataMember(Name = "bussiness")]
        public string Bssiness { get; set; }

        [DataMember(Name = "lanes")]
        public int Lanes { get; set; }

        [DataMember(Name = "direction")]
        public string Direction { get; set; }

        [DataMember(Name = "old")]
        public string OldUri { get; set; }
    }
}

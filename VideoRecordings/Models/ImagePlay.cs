using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    /// <summary>
    /// 图片文件类
    /// </summary>
    [DataContract]
    public class ImagePlay
    {
        /// <summary>
        /// 文件夹ID
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 文件夹名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 数据编号
        /// </summary>
        [DataMember(Name = "project_name")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// 图片状态
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "recorded")]
        public string Recorded { get; set; }

        [DataMember(Name = "create_time")]
        public string CreateTime { get; set; }

        [DataMember(Name = "labels")]
        public List<string> Labels;

        public string Label
        {
            get
            {
                return string.Join(",", Labels);
            }
        }

        [DataMember(Name = "snapshot_ids")]
        public List<int> ImageId;

        public int Images
        {
            get
            {
                return ImageId.Count;
            }
        }

        [DataMember(Name = "start_time")]
        public string StartTime { get; set; }

        [DataMember(Name = "end_time")]
        public string EndTime { get; set; }

        [DataMember(Name = "record_time")]
        public string RecordTime { get; set; }

        [DataMember(Name = "project")]
        public Projects Project { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        public string Place { get => Project.Place; }

        public string Replicator { get => Project.Replicator; }
    }
}

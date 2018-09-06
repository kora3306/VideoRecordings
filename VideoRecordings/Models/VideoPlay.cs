using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    /// <summary>
    /// 视频文件类
    /// </summary>
    [DataContract]
    public class VideoPlay
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "project_name")]
        public string ProjectName { get; set; }

        public string NameExhibition
        {
            get
            {
                return ProjectName.ToUpper();
            }
        }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "recorded")]
        public string Recorded { get; set; }

        [DataMember(Name = "deframed")]
        public string Deframed { get; set; }

        [DataMember(Name = "frame_path")]
        public string FramePath { get; set; }

        [DataMember(Name = "create_time")]
        public string CreateTime { get; set; }

        [DataMember(Name = "labels")]
        public AllTypeLabel Labels { get; set; }

        public string Label
        {
            get
            {
                if (Labels.StaticLabel==null&&Labels.DynamicLabel==null)
                    return string.Empty;
                List<VideoLabel> labels = new List<VideoLabel>();
                foreach (TypeLabel item in Labels.AllLabel)
                {
                    labels.AddRange(item.Labels);
                }
                return string.Join(",",labels.Select(t=>t.Name));
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

        [DataMember(Name = "snapshoted")]
        public string Snapshoted { get; set; }

        [DataMember(Name = "equipment_info")]
        public EquipmentInfo Rquipment { get; set; }

        public string Place { get => Project.Place; }

        public string Replicator { get => Project.Replicator; }

        public int EquipmentID { get => Rquipment.Id; }

        public string EquipmentName { get => Rquipment.Id + ":" + Rquipment.Name; }
    }

    /// <summary>
    /// 字典类
    /// </summary>
    [DataContract]
    public class Projects
    {
        [DataMember(Name = "place")]
        public string Place { get; set; }

        [DataMember(Name = "project_name")]
        public string ProjectName { get; set; }

        [DataMember(Name = "replicator")]
        public string Replicator { get; set; }

        [DataMember(Name = "project_id")]
        public string ProjectId { get; set; }
    }
}

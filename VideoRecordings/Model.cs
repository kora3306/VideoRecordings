using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings
{
    class Model
    {
    }
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
    }


    /// <summary>
    /// 视频文件类
    /// </summary>
    [DataContract]
    public class VideoPlay
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set;}

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

        public string Place { get => Project.Place; }

        public string Replicator { get => Project.Replicator; }
    }


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

        [DataMember(Name="bussiness")]
        public string Bssiness { get; set; }

        [DataMember(Name = "lanes")]
        public int Lanes { get; set; }

        [DataMember(Name = "direction")]
        public string Direction { get; set; }

        [DataMember(Name = "old")]
        public string OldUri { get; set; }
    }



    /// <summary>
    /// 图片文件类
    /// </summary>
    [DataContract]
    public class ImagePlay
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "project_name")]
        public string ProjectName { get; set; }

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

        public string Place { get => Project.Place; }

        public string Replicator { get => Project.Replicator; }
    }
}

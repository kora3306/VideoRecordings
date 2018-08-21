using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    [DataContract]
    public class Repetitions
    {
        [DataMember(Name ="id")]
        public int ID { get; set; }

        [DataMember(Name = "create_time")]
        public string ModifyTimes { get; set; }

        [DataMember(Name ="status")]
        public int Status { get; set; }

        [DataMember(Name = "batch_info")]
        public string Info { get; set; }

        public string ModifyTime
        {
            get
            {
                return GMT2Local(ModifyTimes).ToString();
            }
        }


        /// <summary>  
        /// GMT时间转成本地时间  
        /// </summary>  
        /// <param name="gmt">字符串形式的GMT时间</param>  
        /// <returns></returns>  
        public static DateTime GMT2Local(string gmt)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;
        }
    }

    /// <summary>
    /// 视频查重
    /// </summary>
    [DataContract]
    public class Repetition
    {
        public Repetition(string name, string info,int id, List<int> ids)
        {
            Name = name;
            Info = info;
            ID = id;
            Ids = ids;
        }
        [DataMember(Name = "user_name")]
        public string Name { get; set; }
        [DataMember(Name = "batch_info")]
        public string Info { get; set; }
        [DataMember(Name = "video_ids")]
        public List<int> Ids { get; set; }
        [DataMember(Name = "project_id")]
        public int ID { get; set; }        
    }



    /// <summary>
    /// 视频查重返回
    /// </summary>
    [DataContract]
    public class ReturnRepetition
    {
        [DataMember(Name = "error_code")]
        public int Code { get; set; }

        [DataMember(Name = "path")]
        public string Path { get; set; }

        [DataMember(Name = "video_id")]
        public int ID { get; set; }

        [DataMember(Name = "duplicate_video_path")]
        public List<Duplicate> Duplicates { get; set; }

        public int DuplicateId { get => Duplicates!=null?Duplicates.First().Id:0; }

        public string DuplicatePath { get => Duplicates != null ? Duplicates.First().Path : string.Empty; }
    }

    [DataContract]
    public class Duplicate
    {
        [DataMember(Name = "path")]
        public string Path { get; set; } = string.Empty;
        [DataMember(Name = "video_id")]
        public int Id { get; set; } = 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    /// <summary>
    /// 设备信息
    /// </summary>
    [DataContract]
    public class EquipmentInfo
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "site")]
        public string Site { get; set; }

        [DataMember(Name = "uid")]
        public string Uid { get; set; }

        [DataMember(Name = "labels")]
        public List<string> Labels { get; set; }

        public string LabelStr { get => string.Join(",", OrderLabel()); }

        private List<string> OrderLabel()
        {
            string subject = Labels.FirstOrDefault(t => t == "车" || t == "人");
            if (string.IsNullOrEmpty(subject)) return Labels;
            Labels.Remove(subject);
            Labels.Insert(0, subject);
            return Labels;
        }

    }

}

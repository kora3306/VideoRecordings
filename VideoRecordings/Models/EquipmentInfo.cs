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
    }

}

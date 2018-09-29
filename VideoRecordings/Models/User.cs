using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VideoRecordings.Models
{
    [DataContract]
   public class User
    {
        [DataMember(Name="id")] public int Id { get; set; }
        [DataMember(Name = "name")] public string Name { get; set; }
        [DataMember(Name = "permission")] public int Permisson { get; set; }
        [DataMember(Name = "real_name")] public string RealName { get; set; }
    }
}

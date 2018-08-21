using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using VideoRecordings.GetDatas;

namespace VideoRecordings.Models
{
    /// <summary>
    /// 通道分组展示类
    /// </summary>
    [DataContract]
    public class MyGroup
    {
        public static MyGroup GetGetGroups(string city = null, string street = null, string site = null, string uid = null)
        {
            MyGroup group = GroupData.GetGroupShows(city, street, site, uid);
            return group;
        }

        [DataMember(Name = "cities")] public List<string> Cities { get; set; }

        [DataMember(Name = "equipments")]
        public List<EquipmentInfo> Equipments { get; set; }

        [DataMember(Name = "streets")] public List<string> Streets { get; set; }

        [DataMember(Name = "sites")] public List<string> Sites { get; set; }

        [DataMember(Name = "uids")] public List<string> Uids { get; set; }

        public static bool AddGroup(string name)
        {
            return GroupData.AddGroup(name);
        }

        public static bool DeleteGroup(int id)
        {
            return GroupData.DeleteGroup(id);
        }

        public static bool UpdateGroup(int id,string name)
        {
            return GroupData.UpdateGroup(id, name);
        }

        public static bool DeleteEquipmentToGroup(int id,List<int> ids)
        {
            return GroupData.DeleteEquipmentToGroup(id,ids);
        }

        public static bool AddEquipmentToGroup(int id, List<int> ids)
        {
            return GroupData.AddEquipmentToGroup(id,ids);
        }
    }
}

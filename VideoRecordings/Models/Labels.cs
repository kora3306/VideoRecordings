using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using VideoRecordings.GetDatas;
using Common;

namespace VideoRecordings.Models
{

    [DataContract]
    public class TypeLabel : ICloneable
    {
        [DataMember(Name = "id")] public int Id { get; set; }
        [DataMember(Name = "name")] public string Name { get; set; }
        [DataMember(Name = "children")] public VideoLabels Labels { get; set; } = new VideoLabels();
        [DataMember(Name = "type")] public int Type { get; set; }
        [DataMember(Name = "ref")] public int Ref { get; set; }

        public object Clone()
        {
            var temp = (TypeLabel)this.MemberwiseClone();
            temp.Labels = (VideoLabels)Labels?.Clone();
            return temp;
        }
    }

    public class TypeLabels:List<TypeLabel>,ICloneable
    {
        public TypeLabels()
        {

        }

        public TypeLabels(List<TypeLabel> types)
        {
            AddRange(types);
        }

        public object Clone()
        {
            return new TypeLabels(this.Select(t => (TypeLabel)t.Clone()).ToList());
        }

        public TypeLabels(List<TypeLabel> types1, List<TypeLabel> types2)
        {
            AddRange(types1);
            AddRange(types2);
        }
    }

    [DataContract]
    public class VideoLabel : ICloneable
    {
        [DataMember(Name = "id")] public int Id { get; set; }
        [DataMember(Name = "name")] public string Name { get; set; }

        public object Clone()
        {
            VideoLabel label = (VideoLabel)this.MemberwiseClone();
            return label;
        }

    }

    public class VideoLabels:List<VideoLabel>,ICloneable
    {
        public VideoLabels() { }

        public string LabelNameStr => string.Join(",", this.Select(s => s.Name));

        public VideoLabels(List<VideoLabels> list)
        {
            foreach (VideoLabels item in list)
            {
                AddRange(item);
            }
        }

        public VideoLabels(List<VideoLabel> list)
        {
            AddRange(list);
        }

        public object Clone()
        {
             return new VideoLabels(this.Select(s =>(VideoLabel)s.Clone()).ToList());
        }
    }

    [DataContract]
    public class AllTypeLabel
    {
        [DataMember(Name = "dynamic")]
        public TypeLabels DynamicLabel { get; set; }

        [DataMember(Name = "static")]
        public TypeLabels StaticLabel { get; set; }

        public TypeLabels AllLabel
        {
            get
            {
                if (DynamicLabel != null && StaticLabel != null)
                {
                    return new TypeLabels(DynamicLabel, StaticLabel);
                }
                else if (DynamicLabel == null)
                {
                    return StaticLabel;
                }
                else if (StaticLabel == null)
                {
                    return DynamicLabel;
                }
                else
                {
                    return new TypeLabels();
                }
            }
        }
    }
        

        

    public class MyLabels
    {
        public MyLabels()
        {
            AllLabels = LabelData.GetAllLabel();
        }

        public MyLabels(int id)
        {
            AllLabels = LabelData.GetAllLabel(id);
        }

        public AllTypeLabel AllLabels { get; set; }

        public TypeLabels DynamicLabel { get => AllLabels.DynamicLabel; }

        public TypeLabels StaticLabel { get => AllLabels.StaticLabel; }

        public Dictionary<int, string> AllLabelsToDic
        {
            get
            {
                List<VideoLabel> videoLabels = new List<VideoLabel>();
                foreach (TypeLabel it in DynamicLabel.Union(StaticLabel))
                {
                    videoLabels.AddRange(it.Labels);
                }
                return videoLabels.ToDictionary(t => t.Id, s => s.Name);
            }
        }

        public  List<int> GetSelectIds(List<string> label)
        {
            List<int> ids = new List<int>();
            foreach (var item in label)
            {
                int id = AllLabelsToDic.FirstOrDefault(t => t.Value == item).Key;
                ids.Add(id);
            }
            return ids;
        }

        public int GetSelectIds(string label)
        {
           return AllLabelsToDic.FirstOrDefault(t => t.Value == label).Key;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using VideoRecordings.GetDatas;

namespace VideoRecordings.Models
{

    [DataContract, Serializable]
    public class TypeLabel : ICloneable
    {
        [DataMember(Name = "id")] public int Id { get; set; }
        [DataMember(Name = "name")] public string Name { get; set; }
        [DataMember(Name = "children")] public List<VideoLabel> Labels { get; set; } = new List<VideoLabel>();
        [DataMember(Name = "type")] public int Type { get; set; }
        [DataMember(Name = "ref")] public int Ref { get; set; }

        public object Clone()
        {
            TypeLabel typelabel = new TypeLabel() { Id = Id, Name = Name };
            foreach (VideoLabel item in Labels)
            {
                VideoLabel label = (VideoLabel)item.Clone();
                typelabel.Labels.Add(label);
            }
            return typelabel;
        }

        public TypeLabel DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as TypeLabel;
            }
        }
    }

    [DataContract, Serializable]
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

    [DataContract]
    public class AllTypeLabel
    {
        [DataMember(Name = "dynamic")]
        public List<TypeLabel> DynamicLabel { get; set; }

        [DataMember(Name = "static")]
        public List<TypeLabel> StaticLabel { get; set; }
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

        public List<TypeLabel> DynamicLabel { get => AllLabels.DynamicLabel; }

        public List<TypeLabel> StaticLabel { get => AllLabels.StaticLabel; }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRecordings
{
    class DataClass
    {

    }

    public class MyLabel
    {
        public List<TreeNode> treeNodes { get; set; }
        public Dictionary<string, string> LabelsNumber = new Dictionary<string, string>();   // 标签对照
        public Dictionary<string, string> LabelAll = new Dictionary<string, string>();
        public MyLabel()
        {
            treeNodes = GetData.GetLabels(out LabelsNumber, out LabelAll);
        }

        public  List<int> GetIds(List<string> labels)
        {
            List<int> ids = new List<int>();
            foreach (var item in labels)
            {
                int id = int.Parse(LabelAll.FirstOrDefault(t => t.Value == item).Key);
                ids.Add(id);
            }
            return ids;
        }
    }
}

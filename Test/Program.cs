using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRecordings.Models;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ids = new List<int>() { 12214 , 12215 , 12216, 12217 };
            foreach (int item in ids)
            {
                Console.WriteLine(item+"---"+ Get(item));
            }       
            Console.ReadKey();
        }

        /// <summary>
        /// 将视频改为未解帧状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static bool Get(int id)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("deframed","未解帧");
            bool obj = VideoRecordings.GetDatas.VideoData.SaveTime(id,JsonConvert.SerializeObject(keys));
            return obj;
        }
    }
}

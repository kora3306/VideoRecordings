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
            int index = 12963;
            List<int> ids = new List<int>() ;
            for (int i = 0; i < 56; i++)
            {
                ids.Add(index + i);
            }
            index = 13139;
            for (int i = 0; i < 52; i++)
            {
                ids.Add(index + i);
            }
            foreach (int item in ids)
            {
                bool b = Get(item).Result;
                Console.WriteLine(item+"---"+ b);
            }
            Console.WriteLine($"结束-----{ids.Count}");
            Console.ReadKey();
        }

        /// <summary>
        /// 将视频改为未解帧状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private static async Task<bool> Get(int id)
        {
            Dictionary<string, string> keys = new Dictionary<string, string>();
            keys.Add("deframed","未解帧");
            bool obj = await VideoRecordings.GetDatas.VideoData.SaveTimeAsync(id,JsonConvert.SerializeObject(keys));
            return obj;
        }
    }
}

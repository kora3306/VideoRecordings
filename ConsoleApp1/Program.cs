using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public string path = @"C:\Users\sk\Desktop\CS";

        static void Main(string[] args)
        {
            //List<int> arr = new List<int>() { 1, 1, 2, 2, 3, 3 ,4};
            //List<int> brr = new List<int>() { 1, 1, 2, 2, 3, 3 ,4};

            //Console.WriteLine(arr.SequenceEqual(brr));
            //Console.ReadKey();
            B<string> b = new B<string>("11");
            b.set("1");

            Console.ReadKey();
        }

        static async Task CS()
        {
            var b = Write1();
            Console.WriteLine("+++++++++");
            var c = Write2();
        }


        static async Task Write1()
        {
            await Task1("Write1");
        }
        

        static async Task Write2()
        {
            await Task1("Write2");
        }


        static async Task Task1(string text)
        {
            Thread.Sleep(2000);
            Console.WriteLine("测试输出"+text);
        }


        static void JobForAThread(object state)
        {
            for (int i = 0; i < 300; i++)
            {
                Console.WriteLine("loop {0}, running inside pooled thread {1}", i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(50);
            }
        }

       
        public static void Set(A<int> a)
        {
            Console.WriteLine(a.AA);
        }
    }

    public class A<T>
    {
        public string AA { get; set; }

        public virtual void set( T a )
        {

        }
    }

    public class B<T>:A<T>
    {
        public string BB { get; set; }

        public B(string str)
        {
            BB = str;
        }
            
        public void Get()
        {
            Console.WriteLine(BB);
        }
           
        public override void set(T b)
        {
            Console.WriteLine(b+"----"+typeof(T).ToString());
        }
    }
}

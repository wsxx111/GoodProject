using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread t = new Thread(GoWithParameters);
            //t.Name = "小金星";
            //t.Start("第一个参数");
            //int ss = 0;
            //Thread t2 = new Thread(GoWithParameters);
            //t2.Name = "大太阳";
            //t2.Start("给你的第二个参数");
            ////Thread t3 = new Thread(GoWithParameters);
            ////t3.Start("参数啊");
            //t2.Join();
            //Console.WriteLine("结束了");
            //Console.ReadKey();


            var signal = new ManualResetEvent(false);

            new Thread(() =>
            {
                Console.WriteLine("Waiting for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("Got signal!");
            }).Start();
            Thread.Sleep(2000);

            signal.Set();// 打开“信号”

            Console.ReadKey();
        }


        static void Go()
        {
            Console.WriteLine("We Will Go");
        }

        static void GoWithParameters(object msg)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i + Thread.CurrentThread.Name + "参数为:" + msg);
                Thread.Sleep(2000);
            }
            Console.WriteLine(Thread.CurrentThread.Name + "参数为:" + "dddd");
        }

    }
}

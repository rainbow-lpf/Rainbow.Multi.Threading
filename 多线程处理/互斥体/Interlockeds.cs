using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.互斥体
{
    public class Interlockeds
    {
        static int count = 0;
        static Mutex mutex = new Mutex();

        public static void MainRun() 
        {
            for (int i = 0; i < 100; i++)
            {
                Thread t = new Thread(Run);
                t.Start();
            }
           //原子操作Add方法  以原子操作形式  添加两个32位的整数并用两者和替换第一个整数
            int x = 10;
            Interlocked.Add(ref x,20);
            Console.WriteLine(x);
            //设为指定的值 并返回原始值
            Interlocked.Exchange(ref x, 30);
            Console.WriteLine(x);
            //如果相等 返回第二个参数的值
            Interlocked.CompareExchange(ref x, 30, 10);
            Console.WriteLine(x);
        }
        public static void Run() 
        {
            Thread.Sleep(1000);
            //Interlocked.Increment 自增
            Console.WriteLine("当前数字：{0}", Interlocked.Increment(ref count));
        }
    }
}

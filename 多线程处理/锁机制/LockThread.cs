using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.锁机制
{

    public class LockThread
    {
        //资源
        static object obj = new object();
        static int count = 0;

        public static void LockMain()
        {
            for (int i = 0; i < 10; i++)
            {
                //没有枷锁 顺序乱
                Thread t = new Thread(Run);
                //枷锁按照顺序来
                Thread lockt = new Thread(Run);
                t.Start();
                //lockt.Start();
            }
        }
        public static void Run()
        {
            Thread.Sleep(10);
            Console.WriteLine("当前数字：{0}", ++count);
        }

        /// <summary>
        /// 枷锁的情况   Monitor.Enter  Monitor.Exit 成对出现
        /// </summary>
        public static void LockRun()
        {
            Thread.Sleep(1000);
            //进入临界区
            Monitor.Enter(obj);
            Console.WriteLine("当前数字：{0}", ++count);
            //退出临界区
            Monitor.Exit(obj);
        }
    }
}

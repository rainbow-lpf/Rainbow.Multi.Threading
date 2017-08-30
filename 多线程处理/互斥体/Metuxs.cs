using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.互斥体
{
    /// <summary>
    ///  Metux中提供了WatiOne和ReleaseMutex来确保只有一个线程来访问共享资源，是不是跟Monitor很类似，下面我还是举个简单的例子，注意我并没有给Metux取名字。
    /// </summary>
    public class Metuxs
    {
        //一个同步基元 可用于进程间的同步
        // private static Mutex mutex = new Mutex(); //线程之间的同步
        private static Mutex mutex = new Mutex(false, "cnblogs");
        private static int count = 0;

        #region  线程之间的同步
        public static void MianRun()
        {
            for (int i = 0; i < 30; i++)
            {
                Thread t = new Thread(Run);
                t.Start();
            }
        }
        /// <summary>
        /// 线程之间的同步
        /// </summary>
        public static void Run()
        {
            Thread.Sleep(1000);
            //阻止当前线程 直到收到信号
            mutex.WaitOne();
            Console.WriteLine("当前数字：{0}", ++count);
            //释放
            mutex.ReleaseMutex();
        }
        #endregion

        #region  进程之间的同步
        /// <summary>
        /// 跑起来
        /// </summary>
        public static void MainProcess()
        {
            Thread t = new Thread(ProcessRun);
            t.Start();
        }
        /// <summary>
        /// 进程之间的同步
        /// </summary>
        public static void ProcessRun()
        {
            mutex.WaitOne();
            Console.WriteLine("当前时间：{0}我是线程:{1}，我已经进去临界区", DateTime.Now, Thread.CurrentThread.GetHashCode());
            //10s
            Thread.Sleep(10000);
            Console.WriteLine("\n当前时间:{0}我是线程:{1}，我准备退出临界区", DateTime.Now, Thread.CurrentThread.GetHashCode());
            mutex.ReleaseMutex();
        }
        #endregion
    }
}

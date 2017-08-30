using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.锁机制
{
    public class ReaderWriterLocks
    {
        static List<int> list = new List<int>();
        //定义单个写线程和多个读线程的锁
        static ReaderWriterLock rw = new System.Threading.ReaderWriterLock();
        //<1>AcquireWriterLock: 获取写入锁。
        //ReleaseWriterLock：释放写入锁。
        //<2>AcquireReaderLock: 获取读锁。
        //ReleaseReaderLock：释放读锁。
        //<3>UpgradeToWriterLock:将读锁转为写锁。
        //DowngradeFromWriterLock：将写锁还原为读锁。
        public static void MainRun()
        {
            Thread t1 = new Thread(AutoAddFunc);
            Thread t2 = new Thread(AutoReadFunc);
            t1.Start();
            t2.Start();
        }

        public static void AutoAddFunc()
        {
            Timer timer = new Timer(new TimerCallback(Add), null, 0, 3000);
        }

        /// <summary>
        /// 1000mm 自动读取一次
        /// </summary>
        public static void AutoReadFunc()
        {
            Timer timer1 = new Timer(new TimerCallback(Read), null, 0, 1000);
            Timer timer2 = new Timer(new TimerCallback(Read), null, 0, 1000);
            Timer timer3 = new Timer(new TimerCallback(Read), null, 0, 1000);
        }
        public static void Add(object obj)
        {
            var num = new Random().Next(0, 1000);
            //写锁
            rw.AcquireWriterLock(TimeSpan.FromSeconds(30));
            list.Add(num);
            Console.WriteLine("我是线程{0}，我插入的数据是{1}。", Thread.CurrentThread.ManagedThreadId, num);
            //释放锁 让其等待
            rw.ReleaseWriterLock();
        }

        public static void Read(object obj)
        {
            rw.AcquireReaderLock(TimeSpan.FromSeconds(30));
            Console.WriteLine("我是线程{0},我读取的集合为:{1}", Thread.CurrentThread.ManagedThreadId, string.Join(",", list));
            //释放锁
            rw.ReleaseReaderLock();
        }
    }
}

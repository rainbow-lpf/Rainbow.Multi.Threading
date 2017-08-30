using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.信号量
{
    public class Semaphores
    {
        //指定最大的并发入口数
        private static Semaphore sem = new Semaphore(1, 10);

        public static void SemMain() 
        {
            Thread t1 = new Thread(Run1);
            t1.Start();
            Thread t2 = new Thread(Run2);
            t2.Start();
            Thread.Sleep(1000);
            //手动干预
            sem.Release(5);
        }
        public static void Run1() 
        {
            sem.WaitOne();
            Console.WriteLine("大家好，我是Run1");
            //如果不加这句代码  我们悲剧的发现t2线程不能执行，我们知道WaitOne相当于自减信号量，然而默认的信号量个数为1，所以t2想执行必须等待t1通过Release来释放。
           // sem.Release();
        }

        public static void Run2() 
        {
            sem.WaitOne();
            Console.WriteLine("大家好，我是Run2");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.信号量
{
    public class ManualReset
    {
        //设置信号量初始化为false  
        //信号量初始为True，WaitOne采用无限期阻塞，实验发现WaitOne其实并没有被阻塞。
        private static ManualResetEvent mr = new ManualResetEvent(false); 

        public static void MainRun() 
        {
            Thread t = new Thread(Run);
            t.Name = "Jack";
            Console.WriteLine("当前时间:{0}  {1} {1},我是主线程,收到请回答。", DateTime.Now, t.Name);
            t.Start();
            Thread.Sleep(5000);
            //讲事件设置为终止状态  允许一个或多个等待线程继续
            mr.Set();
        }

        public static void Run() 
        {
            //阻止线程 直到当前线程收到信号  这里设置超时时间的话 上面的sleep 就不起作用了 
            mr.WaitOne(2000);
            Console.WriteLine("\n当前时间:{0}  主线程，主线程,{1}已收到！", DateTime.Now, Thread.CurrentThread.Name);
        }
    }
}

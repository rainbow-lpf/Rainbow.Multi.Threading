using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.线程池
{
    public class TheadPools
    {
        /// <summary>
        /// TheadPool 
        /// </summary>
        public static void Run() 
        {
            int workerThreads;
            int completePortsThreads;
            ThreadPool.GetMaxThreads(out workerThreads,out completePortsThreads);
            Console.WriteLine("线程池中最大的线程数{0},线程池中异步IO线程的最大数目{1}", workerThreads, completePortsThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completePortsThreads);
            Console.WriteLine("线程池中最小的线程数{0},线程池中异步IO线程的最小数目{1}", workerThreads, completePortsThreads);
        }

        /// <summary>
        /// SetMaxTheads,SetMinThreads 方法
        /// </summary>
        public static void RunSetMax() 
        {
            int workerThreads;
            int completePortsThreads;
            ThreadPool.SetMaxThreads(100,50);
            ThreadPool.SetMinThreads(20, 10);
            ThreadPool.GetMaxThreads(out workerThreads, out completePortsThreads);
            Console.WriteLine("线程池中最大的线程数{0},线程池中异步IO线程的最大数目{1}", workerThreads, completePortsThreads);
            ThreadPool.GetMinThreads(out workerThreads, out completePortsThreads);
            Console.WriteLine("线程池中最小的线程数{0},线程池中异步IO线程的最小数目{1}", workerThreads, completePortsThreads);
        }

        public static void QueueUserWorkItemRun() 
        {
            ThreadPool.QueueUserWorkItem(Run);
        }

        public static void Run(object obj) 
        {
            Console.WriteLine("我是线程{0}，我是线程池中的线程吗？ \n回答：{1}", Thread.CurrentThread.ManagedThreadId,
                                                                          Thread.CurrentThread.IsThreadPoolThread);
        }

        public static void RegisterWaitForSingleObjectRun() 
        {
            RegisteredWaitHandle handle = null; //动态控制线程
            AutoResetEvent ar = new AutoResetEvent(false); 
           // 当定期触发任务的时候都是由线程池提供并给予执行，那么这里我们溶于信号量的概念以后同样可以实现计时器的功能。
           handle=ThreadPool.RegisterWaitForSingleObject(ar, Run2, null, 5000, false);
            //注册一个委托 并指定超时时间  
            //ThreadPool.RegisterWaitForSingleObject(ar, Run2, null, Timeout.Infinite, false);
            Console.WriteLine("时间:{0} 工作线程请注意，您需要等待5s才能执行。\n", DateTime.Now);
            Thread.Sleep(5000);
            ar.Set();
            Console.WriteLine("时间:{0} 工作线程已执行。\n", DateTime.Now);
            Thread.Sleep(10000);
            //让其停止  
            handle.Unregister(ar);
            Console.WriteLine("小子，主线程要干掉你了。");
        }

        public static void Run2(object obj, bool sign) 
        {
            Console.WriteLine("当前时间:{0}  我是线程{1}\n", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }
}

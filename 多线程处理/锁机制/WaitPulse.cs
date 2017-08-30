using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.锁机制
{
    public class WaitPulse
    {
        /// <summary>
        /// ①可能A线程进入到临界区后，需要B线程做一些初始化操作，然后A线程继续干剩下的事情。
        ///②上面的两个方法，我们可以实现线程间的彼此通信。
        /// </summary>
        //资源锁 
        public class LockObj { }

        public static void WaitPulseRun()
        {
            /// <summary>
            /// ①   可能A线程进入到临界区后，需要B线程做一些初始化操作，然后A线程继续干剩下的事情。
            ///②   用上面的两个方法，我们可以实现线程间的彼此通信。
            /// </summary>
            /// 2：Monitor.Wait和Monitor.Pulse
            //首先这两个方法是成对出现，通常使用在Enter，Exit之间。
            //Wait： 暂时的释放资源锁，然后该线程进入”等待队列“中，那么自然别的线程就能获取到资源锁。
            //Pulse:  唤醒“等待队列”中的线程，那么当时被Wait的线程就重新获取到了锁。
            LockObj obj = new LockObj();
            Jack jack = new Jack(obj);
            John john = new John(obj);
            Thread t1 = new Thread(jack.Run);
            Thread t2 = new Thread(john.Run);
            t1.Start();
            t1.Name = "jack";
            t2.Start();
            t2.Name = "john";
        }
        public class Jack
        {
            private LockObj obj;
            public Jack(LockObj obj)
            {
                this.obj = obj;
            }
            public void Run()
            {
                Monitor.Enter(this.obj);
                Console.WriteLine("{0}:我已进入茅厕。", Thread.CurrentThread.Name);
                Console.WriteLine("{0}：擦，太臭了，我还是撤！", Thread.CurrentThread.Name);
                //暂时释放资源锁
                Monitor.Wait(this.obj);
                Console.WriteLine("{0}：兄弟说的对，我还是进去吧。", Thread.CurrentThread.Name);
                //唤醒等待队列中的资源
                Monitor.Pulse(this.obj);
                Console.WriteLine("{0}：拉完了，真舒服。", Thread.CurrentThread.Name);
                Monitor.Exit(this.obj);
            }
        }

        public class John
        {
            private LockObj obj;
            public John(LockObj obj)
            {
                this.obj = obj;
            }

            public void Run()
            {
                Monitor.Enter(this.obj);
                Console.WriteLine("{0}:直奔茅厕，兄弟，你还是进来吧，小心憋坏了！", Thread.CurrentThread.Name);
                //唤醒等待队列中的线程
                Monitor.Pulse(this.obj);
                Console.WriteLine("{0}:哗啦啦....", Thread.CurrentThread.Name);
                //暂时释放锁资源
                Monitor.Wait(this.obj);
                Console.WriteLine("{0}：拉完了,真舒服。", Thread.CurrentThread.Name);
                Monitor.Exit(this.obj);
            }
        }
    }
}

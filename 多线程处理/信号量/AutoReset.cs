using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace 多线程处理.信号量
{
    public class AutoReset
    {
     //   在VS对象浏览器中，我们发现AutoResetEvent和ManualResetEvent都是继承于EventWaitHandle，所以基本功能是一样的，不过值得注意
     //的一个区别是WaitOne会改变信号量的值，比如说初始信号量为True，如果WaitOne超时信号量将自动变为False，而ManualResetEvent则不会。
        private static AutoResetEvent ar = new AutoResetEvent(true);

        public static void AutoRun() 
        {
            Thread t = new Thread(Run);
            t.Name = "liupengfei ";
            t.Start();
        }

        public static void Run() 
        {
            var state = ar.WaitOne(1000, true);
            Console.WriteLine("我当前的信号量状态:{0}", state);
            state = ar.WaitOne(1000, true);
            Console.WriteLine("我恨你，不理我，您现在的状态是:{0}", state);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using 多线程处理.互斥体;
using 多线程处理.基础线程;
using 多线程处理.锁机制;
using 多线程处理.线程池;
using 多线程处理.信号量;

namespace 多线程处理
{
    class Program
    {
        static void Main(string[] args)
        {
            #region  基础线程
           // BaseThread.SleepJoin();
            //BaseThread. InterruptAbort();
            //图片下载
            //BaseThread.DownMain();
            #endregion

            #region  锁机制
            //LockThread.LockMain();
            //WaitPulse.WaitPulseRun();
            //ReaderWriterLocks.MainRun();
            #endregion

            #region 互斥体
            //Metuxs.MianRun();
            //Metuxs.MainProcess();
            // Interlockeds.MainRun();
            #endregion

            #region 信号量
            //ManualReset.MainRun();
            // AutoReset.AutoRun();
            //Semaphores.SemMain();
            // SemaphoresProcess.SemMain();
            #endregion

            TheadPools.Run();
            TheadPools.RunSetMax();
            TheadPools.QueueUserWorkItemRun();
            TheadPools.RegisterWaitForSingleObjectRun();
            Console.ReadLine();
        }
    }
}

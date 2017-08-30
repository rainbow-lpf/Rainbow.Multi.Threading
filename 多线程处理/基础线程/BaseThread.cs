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

namespace 多线程处理.基础线程
{
    public class BaseThread
    {
        #region Sleep和Join
        public static void SleepJoin()
        {
            Thread thread = new Thread(Run);
            thread.Start();
            //Join相当于把Run方法内嵌如此
            thread.Join();
            //该死的t.Join(),害的我主线程必须在你执行完后才能执行。
            Console.WriteLine("我是主线程:" + Thread.CurrentThread.GetHashCode());
        }
        public static void Run()
        {
            Thread.Sleep(1000);
            Console.WriteLine("我是线程：" + Thread.CurrentThread.GetHashCode());
        }
        #endregion

        #region Interrupt和Abort
        //  ① Interrupt:  抛出的是 ThreadInterruptedException 异常。
        //         Abort:  抛出的是  ThreadAbortException 异常。
        //  ② Interrupt：如果终止工作线程，只能管到一次，工作线程的下一次sleep就管不到了，相当于一个contine操作。
        //         Abort：这个就是相当于一个break操作，工作线程彻底死掉。 
        public static void InterruptAbort()
        {
            Thread thread = new Thread(new ThreadStart(RunIAbort));
            thread.Start();
            Thread.Sleep(100);
            //  thread.Interrupt(); //相当于continue;
            thread.Abort();
            Console.Read();
        }

        public static void RunIAbort()
        {
            for (int i = 0; i < 3; i++)
            {
                Stopwatch watch = new Stopwatch();
                try
                {
                    watch.Start();
                    Thread.Sleep(2000);
                    watch.Stop();
                    Console.WriteLine("第{0}延迟执行：{1}ms", i, watch.ElapsedMilliseconds);
                }
                catch (ThreadInterruptedException e)
                {

                    Console.WriteLine("第{0}延迟执行：{1}ms,不过抛出异常", i, watch.ElapsedMilliseconds);
                }
            }
        }
        #endregion

        #region 下载图片案例

        public static void DownMain()
        {
            string[] str = { "model", "sexy", "belle", "stars" };
            for (int url = 0; url < str.Length; url++)
            {
                Thread thread = new Thread(DownLoad);

                thread.Start(str[url]);
            }
        }

        public static void DownLoad(object category)
        {
            string url = string.Empty;
            for (int purl = 9014; purl > 10; purl--)
            {
                for (int pageSize = 0; pageSize < 20; pageSize++)
                {
                    try
                    {
                        if (pageSize == 0)
                            url = "http://www.mm8mm8.com/" + category + "/" + purl + ".html";
                        else
                            url = "http://www.mm8mm8.com/" + category + "/" + purl + "_" + pageSize + ".html";
                        //创建http链接
                        var request = (HttpWebRequest)WebRequest.Create(url);
                        request.Timeout = 1000 * 5;
                        var response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader str = new StreamReader(stream);
                        string content = str.ReadToEnd();
                        var list = GetHtmlImageUrlList(content);
                        WebClient client = new WebClient();
                        string directoryName = @"D:\MM\";
                        if (!Directory.Exists(directoryName))
                            Directory.CreateDirectory(directoryName);
                        var fileName = string.Empty;
                        if (list.Count == 0)
                        {
                            Console.WriteLine("时间：" + DateTime.Now + " 当前网址：" + url + "  未发现图片");
                            break;
                        }
                        try
                        {
                            fileName = category + "_" + purl + "_" + (pageSize + 1) + ".jpg";
                            var localFile = directoryName + fileName;
                            var imgageRequest = (HttpWebRequest)WebRequest.Create(list[0]);
                            imgageRequest.Timeout = 1000 * 5;
                            var imageResponse = (HttpWebResponse)imgageRequest.GetResponse();
                            var s = imageResponse.GetResponseStream();
                            Image image = image = Image.FromStream(s);
                            image.Save(localFile);
                            image.Dispose();

                            Console.WriteLine("时间：" + DateTime.Now + "  图片：" + fileName + " 已经下载   存入磁盘位置：" + localFile);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("时间：" + DateTime.Now + " 当前图片：" + fileName + " 错误信息：" + e.Message);

                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("时间：" + DateTime.Now + " 当前网址：" + url + " 错误信息：" + e.Message);

                    }
                }
            }
        }

        public static List<string> GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);

            List<string> sUrlList = new List<string>();

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList.Add(match.Groups["imgUrl"].Value);
            return sUrlList;
        }
        #endregion
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerTest
{
    class Program
    {

        //private static volatile int _isUsing;

        //public static void ExitUsing()
        //{
        //    Interlocked.Exchange(ref _isUsing,1);
        //}
        //public static bool TryUsing()
        //{
        //    return Interlocked.CompareExchange(ref _isUsing, 1, 0) == 0;
        //}
        //public static void NoUsing()
        //{
        //    Interlocked.Exchange(ref _isUsing, 0);
        //}
        static void Main(string[] args)
        {
            #region MyRegion

            //List<int> list = new List<int> { 1,2,3,4,5,6,7,8,9,10};



            //for (int i = 1; i < 11; i++)
            //{
            //    Thread t1 = new Thread(() =>
            //    {
            //        if (TryUsing())
            //        {
            //            Thread.Sleep(1000);
            //            list.RemoveAt(1);
            //            list.ForEach(p =>
            //            {
            //                Console.WriteLine(p+",");
            //            });
            //            NoUsing();
            //        }

            //    });

            //    t1.Start();
            //}



            //Thread t2 = new Thread(() =>
            //{
            //    if (TryUsing())
            //    {
            //        Thread.Sleep(1000);
            //        list.Add(0);
            //        NoUsing();
            //    }

            //});



            #endregion


            //检测注册表中本地安装的net版本
            string subkeyv4 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
            string subkeyv35 = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5";

            bool Isv4 = false;
            bool Isv3 = false;


            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                .OpenSubKey(subkeyv4))
            {
                if (ndpKey != null)
                {
                    Isv4 = ndpKey.GetValue("Version").ToString().StartsWith("4");

                }
            }


            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                .OpenSubKey(subkeyv35))
            {
                if (ndpKey != null)
                {
                    Isv3 = ndpKey.GetValue("Version").ToString().StartsWith("3");

                }
            }

            if (Isv4 && Isv3)
            {
                Console.WriteLine("都装了");
            }
            else
            {
                if (Isv4)
                {
                    Console.WriteLine("4装了");
                }

                if (Isv3)
                {
                    Console.WriteLine("3装了");
                }

                //都没装 默认 net4
                Console.WriteLine("都没装");
            }


            Console.ReadKey();
        }
        /// <summary>
        /// TaskYield使用await Task.Yield()，是真正的异步执行，await关键字之前和之后的代码使用不同的线程执行
        /// </summary>
        //    static async Task TaskYield()
        //    {
        //        Console.WriteLine("TaskYield before await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

        //        await Task.Yield();//执行到await Task.Yield()时，调用TaskYield()方法的线程（主线程）立即就返回了，await关键字后面的代码实际上是由另一个线程池线程执行的
        //        //注意Task.Yield()方法返回的不是Task类的对象，而是System.Runtime.CompilerServices.YieldAwaitable结构体的实例

        //        Console.WriteLine("TaskYield after await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

        //        Thread.Sleep(3000);//阻塞线程3秒钟，模拟耗时的操作

        //        Console.WriteLine("TaskYield finished!");
        //    }

        //    /// <summary>
        //    /// 模拟TaskYield的异步执行
        //    /// </summary>
        //    static  Task TaskYieldSimulation()
        //    {
        //        //模拟TaskYield()方法中，await关键字之前的代码，由调用TaskYieldSimulation()方法的线程（主线程）执行
        //        Console.WriteLine("TaskYieldSimulation before await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

        //        return Task.Run(() =>
        //        {
        //            //使用Task.Run启动一个新的线程什么都不做，立即完成，相当于就是Task.Yield()
        //        }).ContinueWith(t =>
        //        {
        //            //下面模拟的是TaskYield()方法中，await关键字之后的代码，由另一个线程池线程执行

        //            Console.WriteLine("TaskYieldSimulation after await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

        //            Thread.Sleep(3000);//阻塞线程3秒钟，模拟耗时的操作

        //            Console.WriteLine("TaskYieldSimulation finished!");
        //        });
        //    }

        //    /// <summary>
        //    /// TaskCompleted使用await Task.CompletedTask，是假的异步执行，实际上是同步执行，await关键字之前和之后的代码使用相同的线程执行
        //    /// </summary>
        //    static async Task TaskCompleted()
        //    {
        //        Console.WriteLine("TaskCompleted before await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

        //        await Task.CompletedTask;//执行到await Task.CompletedTask时，由于await的Task.CompletedTask已经处于完成状态，所以.NET Core判定await关键字后面的代码还是由调用TaskCompleted()方法的线程（主线程）来执行，所以实际上整个TaskCompleted()方法是单线程同步执行的

        //        Console.WriteLine("TaskCompleted after await, current thread id: {0}", Thread.CurrentThread.ManagedThreadId.ToString());

        //        Thread.Sleep(3000);//阻塞线程3秒钟，模拟耗时的操作

        //        Console.WriteLine("TaskCompleted finished!");
        //    }

        //}

        //public class MyTaskScheduler:TaskScheduler
        //{

        //    protected override IEnumerable<Task>? GetScheduledTasks()
        //    {

        //        throw new NotImplementedException();
        //    }

        //    protected override void QueueTask(Task task)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}

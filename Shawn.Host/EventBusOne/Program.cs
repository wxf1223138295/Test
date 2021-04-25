using ShawnEventBus.EventBus;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventBusOne.LazyImp;

namespace EventBusOne
{
    public class Myclass
    {
        public string Name { get; set; }
    }
    class Program
    {
        // static IdWorker idWorker = new IdWorker(1, 1);
        //static IdWorker idWorker = new IdWorker(1, 1);
        // private static SnowflakeBug snowflake =new SnowflakeBug(1,1);
      
        public static Task Main(string[] args)
        {
            LazyImp.Lazy<Myclass> las = new LazyImp.Lazy<Myclass>();
            var ssw = "333";
            var ss=las.Value;
            

            var r = las.Value;




            return Task.CompletedTask;
            //ConcurrentQueue<long> queue = new ConcurrentQueue<long>();

            //var result = Parallel.For(1, 100000, (a, b) =>
            //{
            //    var id = idWorker.NextId();
            //    queue.Enqueue(id);
            //});

            //var ttt = result.IsCompleted;
            //var tst = queue.ToList().GroupBy(p => p).Count();

            //var tt = Snowflake.Instance(1, 1);


            //ConcurrentQueue<long> queue = new ConcurrentQueue<long>();
            //var result = Parallel.For(1L, 100000, (w, d) =>
            //{
            //    var reuslt = tt.NextId();
            //    queue.Enqueue(reuslt);
            //});

            //var ttt = result.IsCompleted;

            //var tst = queue.ToList().GroupBy(p => p).Count();


            //ConcurrentQueue<long> queue = new ConcurrentQueue<long>();


            //var result = Parallel.For(1L, 100000, (w, d) =>
            //{
            //    var reuslt = snowflake.GetId();
            //    queue.Enqueue(reuslt);
            //});

            //var ttt = result.IsCompleted;

            //var tst = queue.ToList().GroupBy(p => p).Count();



            //Console.WriteLine($"总共：{queue.Count}个");
            //Console.WriteLine($"重复：{queue.Count- tst}个");
            //Console.ReadKey();



        }


    }
}

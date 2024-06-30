using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace ConsoleApp1
{
    public class TaskDemo
    {

        public static void RunWaitAll() 
        {
            Console.WriteLine("################## TaskDemo Begin ##########################3333");
            List<Task> tasks = new List<Task>();
            var taskOne = Task.Run(() => 
            {
                Console.WriteLine($"TaskOne Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                for (int i = 0; i< 3; i ++) 
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"taskOne 切片{i}  : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                }
                
                Console.WriteLine($"TaskOne End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });
            tasks.Add( taskOne );

            var complete = taskOne.Wait(7000);
            Console.WriteLine($"taskOne 5000毫秒内是否完成:{complete} : {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            if (!complete)
            {
                var taskTwo = Task.Run(() =>
                {
                    Console.WriteLine($"taskTwo Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine($"taskTwo 切片{i}");
                    }
                    Console.WriteLine($"taskTwo End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                });
                Console.WriteLine($"taskTwo创建");
                tasks.Add(taskTwo);
            }
            
            Task.WaitAll(tasks.ToArray());


            Console.WriteLine("####################### TaskDemo End ##############################################");
        }

        public static void RunWaitAny()
        {
            Console.WriteLine("################## TaskDemo Begin ##########################3333");
            var taskOne = Task.Run(() =>
            {
                Console.WriteLine($"TaskOne Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(6000);
                Console.WriteLine($"TaskOne End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            var taskTwo = Task.Run(() =>
            {
                Console.WriteLine($"taskTwo Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(3000);
                Console.WriteLine($"taskTwo End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            Task.WaitAny(taskOne, taskTwo);


            Console.WriteLine("####################### TaskDemo End ##############################################");
        }

        public static void RunWaitAnyTwo()
        {
            Console.WriteLine("################## TaskDemo Begin ##########################3333");
            var taskOne = Task.Run(() =>
            {
                Console.WriteLine($"TaskOne Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(6000);
                Console.WriteLine($"TaskOne End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            var taskTwo = Task.Run(() =>
            {
                Console.WriteLine($"taskTwo Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(3000);
                Console.WriteLine($"taskTwo End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            Task.WaitAny(taskTwo);


            Console.WriteLine("####################### TaskDemo End ##############################################");
        }

        public static void RunWhenAll()
        {
            Console.WriteLine("################## TaskDemo Begin ##########################3333");
            var taskOne = Task.Run(() =>
            {
                Console.WriteLine($"TaskOne Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(6000);
                Console.WriteLine($"TaskOne End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            var taskTwo = Task.Run(() =>
            {
                Console.WriteLine($"taskTwo Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(3000);
                Console.WriteLine($"taskTwo End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            Task.WhenAll(taskOne, taskTwo);


            Console.WriteLine("####################### TaskDemo End ##############################################");
        }

        public static void RunWhenAny()
        {
            Console.WriteLine("################## TaskDemo Begin ##########################3333");
            var taskOne = Task.Run(() =>
            {
                Console.WriteLine($"TaskOne Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(6000);
                Console.WriteLine($"TaskOne End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });


            var taskTwo = Task.Run(() =>
            {
                Console.WriteLine($"taskTwo Start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                Thread.Sleep(3000);
                Console.WriteLine($"taskTwo End {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            });

            Task.WhenAny(taskOne, taskTwo);


            Console.WriteLine("####################### TaskDemo End ##############################################");
        }

        public static void RunCurrency()
        {
            ConcurrentBag<string> notExistShopSkuIds = new ConcurrentBag<string>();
            List<string> listId = new List<string>();
            List<string> listId2 = new List<string>();
            for (int i = 0; i < 10000; i++) 
            {
                listId.Add(i.ToString());
            }

            try {
                Parallel.ForEach(listId, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, inventory =>
                {
                    listId2.Add(inventory.ToString());
                    notExistShopSkuIds.Add(inventory);

                });
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }

            

            Console.WriteLine($"listId2:{listId2.Count()},ConcurrentCount:{notExistShopSkuIds.Count()}");
        }

    }
}

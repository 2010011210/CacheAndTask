using ConsoleApp1.Cache.Utility;
using ConsoleApp1.Extension;
using ConsoleApp1.Utility;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            CacheTest.RunTest();
            return;
            ShopSortTest.Run();
            Console.ReadLine();

            //TaskDemo.RunWaitAnyTwo();
            TaskDemo.RunWaitAll();

            //TaskDemo.RunWaitAll();

            List<int> items = new List<int>();
            List<int> result = new List<int>();
            for (int i = 1; i < 10; i++) 
            {
                items.Add(i);
            }

            items.PageEach(2, page => 
            {
                if (page.Contains(5)) 
                {
                    return;
                }
                result.AddRange(page);
            });

            Console.Write(string.Join(",", result));
            Console.ReadLine();

        }

        public static void TestDic()
        {
            var notShippedDic = new Dictionary<string, int>();
            List<Sku> skus = new List<Sku>() { new Sku() { SkuId = "SKU-10CDB198*****857", Amount = 2 } };
            foreach (Sku sku in skus)
            {
                notShippedDic[sku.SkuId] = sku.Amount;
            }

            foreach (Sku item in skus)
            {
                if (!notShippedDic.TryGetValue(item.SkuId, out var v))
                {
                    item.Amount = 0;
                    continue;
                }

                item.Amount = Math.Min(item.Amount, v);
            }

            var items = skus.Where(x => x.Amount > 0).ToList();
            Console.Write($"总个数是：{items.Count}");

        }

        public class Sku
        {
            public string SkuId { get; set; }
            public int Amount { get; set; }
        }


    }
}
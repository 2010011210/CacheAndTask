using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class ShopSortTest
    {
        public static void Run() 
        {
            List<ShopSorted> shopSorted = new List<ShopSorted>();
            shopSorted.Add(new ShopSorted() { Shop = new ShopModel() { Name = "firstShop" }, Level =  StockCalcLevel.Level50 });
            shopSorted.Add(new ShopSorted() { Shop = new ShopModel() { Name = "secondShop" }, Level = StockCalcLevel.Level20 });
            shopSorted.Add(new ShopSorted() { Shop = new ShopModel() { Name = "thirdShop" }, Level = StockCalcLevel.Level10000 });
            shopSorted.Add(new ShopSorted() { Shop = new ShopModel() { Name = "forthShop" }, Level =  StockCalcLevel.Level500 });

            var shopList = shopSorted.OrderByDescending(x => x.Level).Select(s => s.Shop).ToList();
            var top2Shop = shopList.Take(2).ToList();
            Console.WriteLine("top2Shop");
            foreach (var item in top2Shop)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("top2ShopEnd");
            foreach (var item in shopList)
            {
                Console.WriteLine(item.Name);
            }

        }
    }
}

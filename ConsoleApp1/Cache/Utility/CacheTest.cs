using ConsoleApp1.Cache.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache.Utility
{
    public class CacheTest
    {
        public static void RunTest() 
        {
            for (int i = 0; i< 5; i++) 
            {
                List<Order> result;
                var key = $"{nameof(CacheTest)}_Query_{nameof(DBHelper)}_{123}";
                Console.WriteLine($"Key:{key}");
                if (CustomeCache.Exist(key))
                {
                    result = CustomeCache.Get<List<Order>>(key);
                }
                else 
                {
                    result = DBHelper.Query<Order>(key);
                    CustomeCache.Add(key, result);
                }

                Console.WriteLine($"{result.GetHashCode()}_{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            for (int i = 0; i < 5; i++)
            {
                List<Order> result;
                var key = $"{nameof(CacheTest)}_Query_{nameof(RedisHelper)}_{173}";
                Console.WriteLine($"Key:{key}");
                if (CustomeCache.Exist(key))
                {
                    result = CustomeCache.Get<List<Order>>(key);
                }
                else
                {
                    result = RedisHelper.Query<Order>(key);
                    CustomeCache.Add(key, result);
                }

                Console.WriteLine($"{result.GetHashCode()}_{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

            for (int i = 0; i < 5; i++)
            {
                var key = $"{nameof(CacheTest)}_Query_{nameof(DBHelper)}_{124}";
                Console.WriteLine($"Key:{key}");
                var result = CustomeCache.Find<List<Order>>(key, () => DBHelper.Query<Order>(key));      //默认值当做一个委托传进去。
                var result2 = CustomeCache.Find<List<Order>>(key, () => RedisHelper.Query<Order>(key));

                Console.WriteLine($"{result.GetHashCode()}_{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

        }
    }
}

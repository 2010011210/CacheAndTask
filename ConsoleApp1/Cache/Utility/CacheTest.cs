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
                var key2 = $"{nameof(CacheTest)}_Query_{nameof(DBHelper)}_{124}";
                Console.WriteLine($"Key:{key2}");
                var result = CustomeCache.Find<List<Order>>(key2, () => DBHelper.Query<Order>(key2));      //默认值当做一个委托传进去。
                var result2 = CustomeCache.Find<List<Order>>(key2, () => RedisHelper.Query<Order>(key2));

                Console.WriteLine($"{result.GetHashCode()}_{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            }

        }

        /// <summary>
        /// 缓存有过期时间
        /// </summary>
        public static void RunCacheWithExpiredTest() 
        {
            #region 带过期时间的key

            string key = "123456";
            if (CustomeCacheWithExpired.Exist(key))
            {
                string result = CustomeCacheWithExpired.Get<string>(key);
            }
            else
            {
                string resutl = $"每天进步一点点{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                CustomeCacheWithExpired.Add(key, resutl, 10);
            }

            Thread.Sleep(5 * 1000);

            if (CustomeCacheWithExpired.Exist(key))
            {
                string result = CustomeCacheWithExpired.Get<string>(key);
            }
            else
            {
                string resutl = $"第二次设置缓存{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                CustomeCacheWithExpired.Add(key, resutl, 10);
            }

            Thread.Sleep(5 * 1000);

            if (CustomeCacheWithExpired.Exist(key))
            {
                string result = CustomeCacheWithExpired.Get<string>(key);
            }
            else
            {
                string resutl = $"第3次设置缓存{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                CustomeCacheWithExpired.Add(key, resutl, 10);
            }

            #endregion

        }

        /// <summary>
        /// 多线程并发
        /// </summary>
        public static void RunMultiThread() 
        {
            for (int i = 0; i < 1000; i++) 
            {
                int k = i;
                Task.Run(() => { CustomeCache.Add($"key{k}", k); });  
            }

            if (CustomeCache.Exist($"key{1}")) 
            {
                var s = CustomeCache.Get<int>($"key{1}");
            }
        }

    }
}

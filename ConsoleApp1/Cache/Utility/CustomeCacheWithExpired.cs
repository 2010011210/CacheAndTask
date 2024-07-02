using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache.Utility
{
    public class CustomeCacheWithExpired
    {
        private static Dictionary<string, KeyValuePair<object, DateTime>> customeCache;

        static CustomeCacheWithExpired()
        {
            customeCache = new Dictionary<string, KeyValuePair<object, DateTime>>();
            Task.Run(() => 
            {
                while (true) 
                {
                    List<string> expiredKeys = new List<string>();
                    foreach (var key in customeCache.Keys) 
                    {
                        var cacheValue = customeCache[key];
                        if (cacheValue.Value < DateTime.Now)
                        {
                            expiredKeys.Add(key);
                        }
                    }
                    expiredKeys.ForEach(key => { customeCache.Remove(key); });
                }
            });
        }

        /// <summary>
        /// 添加缓存，需要添加过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        public static void Add(string key, object value, int seconds)
        {
            customeCache[key] = new KeyValuePair<object, DateTime>(value, DateTime.Now.AddSeconds(seconds));
        }


        public static T Get<T>(string key) 
        {
            return (T)customeCache[key].Key;
        }

        /// <summary>
        /// 检查的时候，也需要检查过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key) 
        {
            if (customeCache.ContainsKey(key))
            {
                var cacheValue = customeCache[key];
                if (cacheValue.Value > DateTime.Now)
                {
                    return true;
                }
                else 
                {
                    customeCache.Remove(key);  //过期了，直接删除
                    return false;
                }

            }
            else 
            {
                return false;
            }
        }


        public static T Find<T>(string key, Func<T> fun,int second)
        {
            var t = default(T);
            if (Exist(key))
            {
                t = Get<T>(key);
            }
            else
            {
                t = fun.Invoke();
                CustomeCacheWithExpired.Add(key, t, second);
            }
            return t;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache.Utility
{
    public class CustomeCache
    {
        private static Dictionary<string, object> customeCache;

        static CustomeCache() 
        {
            customeCache = new Dictionary<string, object>();
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}初始化缓存");
        }

        // 添加
        public static void Add(string key, object value) 
        {
            if (customeCache.ContainsKey(key))
            {
                customeCache[key]= value;
            }
            else 
            {
                customeCache.Add(key, value);
            }
        }

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveOrUpdate(string key, object value)
        {
            customeCache[key] = value;
        }

        public static T Get<T>(string key) 
        {
            return (T)customeCache[key];
        }

        /// <summary>
        /// 判断key值是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key) 
        {
            if (string.IsNullOrEmpty(key)) 
            {
                return false;
            }
            return customeCache.ContainsKey(key);
        }


        public static T Find<T>(string key, Func<T> fun) 
        {
            var t = default(T);
            if (Exist(key))
            {
                t = Get<T>(key);
            }
            else 
            {
                t = fun.Invoke();
                customeCache.Add(key, t);
            }
            return t;
        }



    }
}

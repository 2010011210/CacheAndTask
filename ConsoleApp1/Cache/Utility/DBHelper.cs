using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache.Utility
{
    public class DBHelper
    {
        public static List<T> Query<T>(string key) 
        {
            Console.WriteLine($"this is query from {nameof(DBHelper)}");
            long count = 0;
            for (int i = 1; i < 1000000000; i++)   //模拟数据库操作延迟
            {
                count += i;
            }
            return new List<T>() { default(T) };
        }
    }
}

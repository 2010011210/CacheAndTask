using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class ForeachBreakTest
    {
        public static void Run() 
        {
            List<string> list = new List<string>()
            {
                "one","two","three"
            };

            foreach (var item in list) 
            {
                Console.WriteLine(item);
                if (item == "two") 
                {
                    break;
                }
            }

            Console.WriteLine("End");

        }
    }
}

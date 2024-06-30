using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class TryCatchTest
    {
        public static void Test() 
        {
            try {
                throw new Exception("错误测试");
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.Message);
                return;
            }
            finally 
            {
                Console.WriteLine("Finally");
            }
        }
    }
}

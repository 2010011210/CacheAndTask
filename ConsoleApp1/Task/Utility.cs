using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TaskUtility
    {
        public static void DoSomething()
        {
            Console.WriteLine($"Doing something no sleep:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}");
        }

        public static int DoSomething(int sleep)
        {
            Thread.Sleep(sleep * 1000);
            Console.WriteLine($"Doing something:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}");
            return sleep;
        }
    }
}

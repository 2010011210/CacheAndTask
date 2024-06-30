using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class CompanyTest
    {
        public static void Run()
        {
            List<Company> companies= new List<Company>();
            companies.Add(new Company() { Age = 1, Name="JST",Address="sahnghai"});
            companies.Add(new Company() { Age = 2, Name="HuaWei",Address="ShenZhen"});
            companies.Add(new Company() { Age = 5, Name="BYD",Address="ShenZhen"});
            TestDic();
            //var companyOlds = companies.Where(i => i.Age > 1).ToList();
            AddCountry(companies.Where(i => i.Age > 1).ToList());
            foreach (var i in companies) 
            {
                Console.WriteLine($"Age:{i.Age}.Name:{i.Name},Address:{i.Address}");
            }
            
        }

        public static void AddCountry(List<Company> companies) 
        {
            companies.ForEach(company => { company.Address += " China "; });
            Console.WriteLine("################### 增加国家 ######################");
            foreach (var i in companies)
            {
                Console.WriteLine($"Age:{i.Age}.Name:{i.Name},Address:{i.Address}");
            }
            Console.WriteLine("######################################################");
        }

        public static void TestDic() 
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("blue", "蓝3");
            dic.Add("red","红1");
            dic.Add("yellow","黄2");
            dic.Add("black","黑4");

            foreach (var item in dic) 
            {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }

            Console.ReadLine();

        }
    }
}

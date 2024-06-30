using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1.Cache.Model
{
    public class Order
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

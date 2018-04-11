using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data.SqlClient;

namespace ConsoleApplication1
{
    class Program
    {
        public class Payment2
        {
            public string id { get; set; }
            public string amount { get; set; }
            public string invoiceid { get; set; }
            public string outlet_id { get; set; }
            public string user_id { get; set; }
            public string fordate { get; set; }
            public string mode { get; set; }
            public string grn { get; set; }
            public string po { get; set; }
            public string custom_invoice { get; set; }
            public string approval_status { get; set; }
            public string created { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string modified { get; set; }
            public string order_id { get; set; }
            public string discount { get; set; }
            public string orderuser_id { get; set; }
            public string schemediscount { get; set; }
            public string beat_id { get; set; }
            public string beat_name { get; set; }
            public string fromoutlet_id { get; set; }
        }

        public class RootObject
        {
            public List<Payment2> Payments { get; set; }
        }
        static void Main(string[] args)
        {
            var client = new MongoClient();
        }
    }
}

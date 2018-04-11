using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Product
    {
        public int id { get; set; }
        public string item { get; set; }
        public string suplier { get; set; }
        public string contactPerson { get; set; }
        public string country { get; set; }
    }
}
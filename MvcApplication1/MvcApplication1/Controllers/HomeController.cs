using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAll()
        {
            var list = new List<Product>()
            {
                new Product {id=1,item="Robber",suplier="German",contactPerson="Google",country="India" },
                new Product {id=2,item="sealnt",suplier="Bangalore",contactPerson="Yahoo",country="Uk" },
                 new Product {id=3,item="Plastic",suplier="Chennai",contactPerson="Facebook",country="German" },
                  new Product {id=4,item="Iron",suplier="Delhi",contactPerson="gmail",country="Italy" }
            };
            return View(list);
        }
        public PartialViewResult Edit(int id)
        {
            List<Product> list = new List<Product>();
            //Product model = new Product();
            var product = list.FirstOrDefault(x => x.id == id);
            return PartialView("View",product);
        }
        [HttpPost]
        public PartialViewResult Edit(Product obj)
        {
            List<Product> productList = new List<Product>();
            Product product = new Product();
            product.item = obj.item;
            product.suplier = obj.suplier;
            product.contactPerson = obj.contactPerson;
            product.country = obj.country;
            productList.Add(product);
            return PartialView("View",productList);
        }
        public PartialViewResult Update()
        {
            return PartialView();

        }

    }
}
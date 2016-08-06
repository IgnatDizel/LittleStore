using LittleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        LittleStoreContext db = new LittleStoreContext();
        public ActionResult Index()
        {

            var products = (from u in db.Products select u).ToList();

            return View(products);
        }

        public ActionResult ProductDetails(int id)
        {

            var product = (from u in db.Products 
                            where u.ProductId == id
                            select u).First();

            return View(product);
        }


    }
}

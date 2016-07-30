using LittleStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleStore.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        LittleStoreContext db = new LittleStoreContext();
        public ActionResult Index()
        {

            var products = (from u in db.Products select u).ToList();

            return View(products);
        }
    }
}

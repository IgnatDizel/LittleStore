using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LittleStore.Models;
using LittleStore.Models.viewModel;
using System.Data;
using LittleStore.Models.EditImage;


namespace LittleStore.Controllers
{
    public class ProductController : Controller
    {

        LittleStoreContext db = new LittleStoreContext();

        public ActionResult ProductDetails(int id)
        {

            var product = (from u in db.Products
                           where u.ProductId == id
                           select u).First();

            return View(product);
        }

        public ActionResult Create()
        {
            return View(new ProductCraeteViewModel());
        }

        [HttpPost]
        public ActionResult Create(ProductCraeteViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ImageUpload != null)
                {
                    var fileName = Path.GetFileName(viewModel.ImageUpload.FileName);
                    var path = Server.MapPath("~/Content/" + viewModel.product.ProductId + fileName);
                    viewModel.ImageUpload.SaveAs(path);
                    db.Images.Add(new Image() { ProductId = viewModel.product.ProductId, ImagePath = "/Content/" + fileName });
                }
                else
                {
                    db.Images.Add(new Image() { ProductId = viewModel.product.ProductId, ImagePath = "/Content/No_Image.png" });
                }

                db.Products.Add(viewModel.product);
                db.SaveChanges();
            }

            return RedirectToAction("ProductDetails", "Product", new {id = viewModel.product.ProductId});
        }

        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductDetails", new {id = product.ProductId});
            }

            return View(product);
        }

        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);

            Image image = (from u in db.Images
                           where u.ProductId == id
                           select u).First();
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index","Home", new {id = product.ProductId});
        }

        public ActionResult DeleteImage(int imageId, int productId)
        {
            Image image = (from u in db.Images
                           where u.ImageId == imageId
                           select u).First();
            db.Images.Remove(image);
            db.SaveChanges();


            CheckValidImage.productHasImage(db.Products.Find(productId));
            


            return RedirectToAction("Edit", new {id = productId});
        }

        public ActionResult AddImage(int productId)
        {
            AddImageViewModel model = new AddImageViewModel();
            model.productId = productId;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddImage(AddImageViewModel addImage)
        {
            if (addImage != null)
            {
                CheckValidImage.productHasDefaultImage(db.Products.Find(addImage.productId));

                var fileName = Path.GetFileName(addImage.ImageUpload.FileName);
                var path = Server.MapPath("~/Content/" + fileName);
                addImage.ImageUpload.SaveAs(path);
                db.Images.Add(new Image() { ProductId = addImage.productId, ImagePath = "/Content/" + fileName });

            }
            else
            {
                db.Images.Add(new Image() { ProductId = addImage.productId, ImagePath = "/Content/No_Image.png" });
            }

            db.SaveChanges();

            return RedirectToAction("Edit", "Product", new {id = addImage.productId});
        }
    }
}

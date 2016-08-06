using LittleStore.Models;
using LittleStore.Models.viewModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using LittleStore.Models.EditImage;

namespace LittleStore.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        LittleStoreContext db = new LittleStoreContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products()
        {
            var products = (from u in db.Products select u).ToList();

            return View(products);
        }

        public ActionResult Details(int id)
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
                    db.Images.Add(new Image() { ProductId = viewModel.product.ProductId, ImagePath = "/Content/" + viewModel.product.ProductId + fileName });
                }
                else
                {
                    db.Images.Add(new Image() { ProductId = viewModel.product.ProductId, ImagePath = "/Content/No_Image.png" });
                }
                
                db.Products.Add(viewModel.product);
                db.SaveChanges();
            }

            return RedirectToAction("Products", "Admin");
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
                return RedirectToAction("Products");
            }

            return View(product);
        }

        public ActionResult EditImages(int id)
        {
            var images = (from i in db.Images
                where i.ProductId == id
                select i).ToList();

            return View(images);
        }

        public ActionResult DeleteImage(int imageId, int productId)
        {
            Image image = (from u in db.Images
                           where u.ImageId == imageId
                           select u).First();
            db.Images.Remove(image);
            if (db.Images.Any())
            {
                AddDefaultImage.AddDefImg(productId);
            }
            db.SaveChanges();
            
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddImage(int Imageid)
        {
            AddImageViewModel model = new AddImageViewModel();
            model.productId = Imageid;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddImage(AddImageViewModel addImage)
        {
            if (addImage != null)
                {
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

            return RedirectToAction("Products", "Admin");
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
            return RedirectToAction("Products");
        }

    }
}

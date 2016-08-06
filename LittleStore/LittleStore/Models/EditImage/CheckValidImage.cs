using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models.EditImage
{
    public static class CheckValidImage
    {
        public static void productHasDefaultImage(Product product)
        {
            if (product.Images.Count == 1 && product.Images.First().ImagePath == "/Content/No_Image.png")
            {
                DelDegImg(product.Images.First());
            }

        }

        public static void productHasImage(Product product)
        {
            if (product.Images.Count == 0)
            {
                AddDefImg(product.ProductId);
            }

        }

        private static void DelDegImg(Image image)
        {
            LittleStoreContext db = new LittleStoreContext();
            var i = db.Images.Single(o => o.ImageId == image.ImageId);
            db.Images.Remove(i);
            db.SaveChanges();
        }

        private static void AddDefImg(int productId)
        {
            Image image = new Image();
            image.ImagePath = "/Content/No_Image.png";
            image.ProductId = productId;
            LittleStoreContext db = new LittleStoreContext();
            db.Images.Add(image);
            db.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models.EditImage
{
    public static class AddDefaultImage
    {
        public static Image AddDefImg(int productId)
        {
            Image image = new Image();
            image.ImagePath = "/Content/No_Image.png";
            image.ProductId = productId;
            LittleStoreContext db = new LittleStoreContext();
            db.Images.Add(image);
            db.SaveChanges();
            return image;
        }
    }
}
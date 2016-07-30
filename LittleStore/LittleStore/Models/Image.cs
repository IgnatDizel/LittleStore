using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
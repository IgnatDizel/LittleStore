using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string DescriptionShort { get; set; }
        public string Description { get; set; }
        public int? CategoryID { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
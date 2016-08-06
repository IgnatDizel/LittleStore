using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models.viewModel
{
    public class ProductCraeteViewModel
    {
        public Product product { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
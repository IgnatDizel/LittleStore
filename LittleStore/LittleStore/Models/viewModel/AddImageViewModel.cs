using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleStore.Models.viewModel
{
    public class AddImageViewModel
    {
        public int productId { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
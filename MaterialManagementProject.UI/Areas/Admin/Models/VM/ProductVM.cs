using MaterialManagementProject.Model.Option;
using MaterialManagementProject.UI.Areas.Admin.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaterialManagementProject.UI.Areas.Admin.Models.VM
{
    public class ProductVM
    {
        public ProductVM()
        {
            Categories = new List<Category>();
            Product = new ProductDTO();
        }
        public List<Category> Categories { get; set; }
        public ProductDTO Product { get; set; }
    }
}
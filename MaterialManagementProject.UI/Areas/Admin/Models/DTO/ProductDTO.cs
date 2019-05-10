using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaterialManagementProject.UI.Areas.Admin.Models.DTO
{
    public class ProductDTO
    {
        public Guid ID { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public decimal? Price { get; set; }
        public int ProductCode { get; set; }
        public short? UnitInStock { get; set; }
        public string ImagePath { get; set; }


        public Guid CategoryID { get; set; }
    }
}
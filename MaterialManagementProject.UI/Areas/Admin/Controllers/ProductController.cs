using MaterialManagementProject.Model.Option;
using MaterialManagementProject.UI.Areas.Admin.Models.DTO;
using MaterialManagementProject.UI.Areas.Admin.Models.VM;
using MaterialManagementProject.Utility;
using MaterialMangementProject.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaterialManagementProject.UI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        CategoryService _categoryService;
        ProductService _productService;
        public ProductController()
        {
            _categoryService = new CategoryService();
            _productService = new ProductService();
        }

        public ActionResult Add()
        {

            ProductVM model = new ProductVM()
            {
                Categories = _categoryService.GetActive(),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product data, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.ImagePath = UploadedImagePaths[0];

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                data.ImagePath = ImageUploader.DefaultProfileImagePath;
                data.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                data.ImagePath = ImageUploader.DefaulCruptedProfileImage;

            }
            else
            {

                data.ImagePath = UploadedImagePaths[1];
                data.ImagePath = UploadedImagePaths[2];

            }

            data.Status = MaterialManagemenetProject.Core.Enum.Status.Active;

            _productService.Add(data);

            return Redirect("/Admin/Product/List");
        }


        public ActionResult List()
        {
            List<Product> model = _productService.GetActive();
            return View(model);
        }

        public ActionResult Update(Guid id)
        {

            Product product = _productService.GetByID(id);
            ProductVM model = new ProductVM();
            model.Product.ID = product.ID;
            model.Product.ProductName = product.ProductName;
            model.Product.Quantity = product.Quantity;
            model.Product.Price = product.Price;
            model.Product.ProductCode = product.ProductCode;
            model.Product.UnitInStock = product.UnitInStock;
            model.Product.ImagePath = product.ImagePath;
            List<Category> categories = _categoryService.GetActive();
            model.Categories = categories;



            return View(model);
        }

        [HttpPost]
        public ActionResult Update(ProductDTO data, HttpPostedFileBase Image)
        {

            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.ImagePath = UploadedImagePaths[0];


            Product update = _productService.GetByID(data.ID);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {

                if (update.ImagePath == null || update.ImagePath == ImageUploader.DefaultProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                    update.ImagePath = ImageUploader.DefaulCruptedProfileImage;

                }
                else
                {
                    update.ImagePath = data.ImagePath;

                }

            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];

            }
            Product product = _productService.GetByID(data.ID);
            product.Price = data.Price;
            product.ProductName = data.ProductName;
            product.ProductCode = data.ProductCode;
            product.UnitInStock = data.UnitInStock;
            product.ImagePath = data.ImagePath;
            product.Quantity = data.Quantity;
            product.CategoryID = data.CategoryID;

            _productService.Update(product);

            return Redirect("/Admin/Product/List");


        }

        public ActionResult Delete(Guid id)
        {
            _productService.Remove(id);
            return Redirect("/Admin/Product/List");
        }
    }
}
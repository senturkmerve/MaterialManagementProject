using MaterialManagementProject.Model.Option;
using MaterialManagementProject.UI.Areas.Admin.Models.DTO;
using MaterialManagementProject.Utility;
using MaterialMangementProject.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaterialManagementProject.UI.Areas.Admin.Controllers
{
    public class AppUserController : Controller
    {
        AppUserService _appUserService;
        public AppUserController()
        {
            _appUserService = new AppUserService();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AppUser data, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.UserImage = UploadedImagePaths[0];

            if (data.UserImage == "0" || data.UserImage == "1" || data.UserImage == "2")
            {
                data.UserImage = ImageUploader.DefaultProfileImagePath;
                data.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                data.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                data.XSmallUserImage = UploadedImagePaths[1];
                data.CruptedUserImage = UploadedImagePaths[2];
            }

            data.Status = MaterialManagemenetProject.Core.Enum.Status.Active;

            _appUserService.Add(data);

            return Redirect("/Admin/AppUser/List");
        }

        public ActionResult List()
        {
            List<AppUser> model = _appUserService.GetActive();
            return View(model);
        }

        public ActionResult Update(Guid id)
        {

            AppUser user = _appUserService.GetByID(id);

            AppUserDTO model = new AppUserDTO();

            model.ID = user.ID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Address = user.Address;
            model.PhoneNumber = user.PhoneNumber;
            model.Role = user.Role;
            model.UserImage = user.UserImage;
            model.XSmallUserImage = user.XSmallUserImage;
            model.CruptedUserImage = user.CruptedUserImage;


            return View(model);

        }

        [HttpPost]
        public ActionResult Update(AppUserDTO data, HttpPostedFileBase Image)
        {

            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.UserImage = UploadedImagePaths[0];


            AppUser update = _appUserService.GetByID(data.ID);

            if (data.UserImage == "0" || data.UserImage == "1" || data.UserImage == "2")
            {

                if (update.UserImage == null || update.UserImage == ImageUploader.DefaultProfileImagePath)
                {
                    update.UserImage = ImageUploader.DefaultProfileImagePath;
                    update.XSmallUserImage = ImageUploader.DefaultXSmallProfileImage;
                    update.CruptedUserImage = ImageUploader.DefaulCruptedProfileImage;
                }
                else
                {
                    update.UserImage = update.UserImage;
                    update.XSmallUserImage = update.XSmallUserImage;
                    update.CruptedUserImage = update.CruptedUserImage;
                }

            }
            else
            {
                update.UserImage = UploadedImagePaths[0];
                update.XSmallUserImage = UploadedImagePaths[1];
                update.CruptedUserImage = UploadedImagePaths[2];
            }

            update.FirstName = data.FirstName;
            update.LastName = data.LastName;
            update.UserName = data.UserName;
            update.Password = data.Password;
            update.Address = data.Address;
            update.PhoneNumber = data.PhoneNumber;
            update.Role = data.Role;
            //update.ImagePath = data.ImagePath;
            _appUserService.Update(update);

            return Redirect("/Admin/AppUser/List");


        }
        public RedirectResult Delete(Guid id)
        {
            _appUserService.Remove(id);
            return Redirect("/Admin/AppUser/List");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OMS.Web.Utilities;
using System.Linq;
using OMS.Model.ViewModel;
using OMS.Web.Globalization;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;

namespace OMS.Web.Controllers
{
    public class ProfileController : BaseController
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CheckAuthentication()
        {
            if (SessionRegistery.CurrentUser != null)
            {
                return Json(new { Result = "OK" });
            }
            else
                return Json(new { Result = "Error" });
        }
        public JsonResult LoadProfile(long UserId)
        {
            var user = uow.Users.Get(e => e.Id == UserId, null, e => e.Upload).FirstOrDefault();
            return Json(new { Data = new ProfileModel(user,CultureHelper.IsArabic?"ar":"en") });
        }

        public JsonResult Save(ProfileModel model)
        {
            var entity = uow.Users.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new User();
                }
                model.FillEntity(entity);

                uow.Users.AddOrUpdate(entity, e => e.Id == model.Id);
                uow.Save();
                SessionRegistery.CurrentUser.NameAr = entity.NameAr;
                SessionRegistery.CurrentUser.NameEn = entity.NameEn;
                SessionRegistery.CurrentUser.ImagePath = model.ImagePath;
                return Json(new { Model = model, Exceptions = new List<string>() });
            }
            catch (BusinessException bexp)
            {
                return Json(new { Model = model, Exceptions = bexp.Exceptions });
            }
            catch (Exception ex)
            {
                List<string> lstExceptions = new List<string>();
                lstExceptions.Add(ex.Message);
                return Json(new { Model = model, Exceptions = lstExceptions });
            }
        }
    }
}
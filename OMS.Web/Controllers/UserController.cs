using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using OMS.Web.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            List<LookupModel> lstUserPageActions = new List<LookupModel>();
            lstUserPageActions.Add(new LookupModel() { Id = (int)PageActionMode.FromGroup, Name = CultureHelper.IsArabic ? "من المجموعة" : "From Group" });
            lstUserPageActions.Add(new LookupModel() { Id = (int)PageActionMode.Yes, Name = CultureHelper.IsArabic ? "نعم" : "Yes" });
            lstUserPageActions.Add(new LookupModel() { Id = (int)PageActionMode.No, Name = CultureHelper.IsArabic ? "لا" : "No" });
            ViewBag.UserPageActions = lstUserPageActions;

            List<LookupModel> lstUserStatuses = new List<LookupModel>();
            lstUserStatuses.Add(new LookupModel() { Id = (int)UserStatus.Active,  Name = CultureHelper.IsArabic ? "مفعل" : "Active" });
            lstUserStatuses.Add(new LookupModel() { Id = (int)UserStatus.InActive, Name = CultureHelper.IsArabic ? "غير مفعل" : "Inactive" });
            ViewBag.UserStatuses = lstUserStatuses;

            ViewBag.SystemPages = uow.SystemPages.Get(C=> C.ParentId == null && C.NameEn != "Home",
                null, c=>c.Children,c=>c.PageActions).Select(s => new UserSystemPageSummaryModel(s)).ToList();

            ViewBag.Nationalities = uow.Nationalities.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            //ViewBag.Jobs = uow.Jobs.Get().Select(s => new LookupModel()
             //   { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Branches = uow.Warehouses.Get().Select(s => new
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn, Selected = false }).ToList();

            ViewBag.Groups = uow.Groups.Get().Select(e=>new LookupModel { 
                Id=e.Id,
                Name=CultureHelper.IsArabic?e.NameAr:e.NameEn
            });

            return View();
        }

        public JsonResult List(UserSearchViewModel sc)
        {
            var result = uow.Users.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new UserSummaryModel(e)).ToList() });
        }

        public JsonResult Details(UserSearchViewModel sc)
        {
            var result = uow.Users.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new UserModel(e)).ToList() });
        }

        public JsonResult GroupActions(UserSearchViewModel sc)
        {
            if (!sc.GroupId.HasValue)
                return Json(new { Data = new List<Group>() });
            var result = uow.Groups.Get(e => e.Id == sc.GroupId, null, e => e.GroupPageActions).FirstOrDefault();
            return Json(new { Data = result.GroupPageActions.Select(S => new { S.PageActionId }).ToList() });
        }

        public JsonResult Save(UserModel model)
        {
            var entity = uow.Users.Get(e=>e.Id == model.Id, null, e=> e.UserGroups,e=>e.UserPageActions).FirstOrDefault();
            
            try
            {
                if (entity == null)
                {
                    entity = new User();
                    entity.ActivationCode = Guid.NewGuid().ToString();
                }
                else
                {
                    uow.UserPageActions.Delete(entity.UserPageActions);
                }
                model.FillEntity(entity);

                ValidateModel(model);
                uow.Users.AddOrUpdate(entity, e => e.Id == model.Id);
                uow.Save();
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

        public JsonResult Delete(long Id)
        {
            try
            {
                uow.Users.DeleteById(Id);

                return Json(new { Exceptions = new List<string>() });
            }
            catch (BusinessException bexp)
            {
                return Json(new { Exceptions = bexp.Exceptions });
            }
            catch (Exception ex)
            {
                List<string> lstExceptions = new List<string>();
                lstExceptions.Add(ex.Message);
                return Json(new { Exceptions = lstExceptions });
            }
        }

        public JsonResult GetDocumentNumber()
        {
            var documentNo = uow.Customers.GetDocumentNumber();
            return Json(documentNo);
        }

        private void ValidateModel(UserModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("NameAr");
            else if (uow.Cities.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameAr");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("NameEn");
            else if (uow.Cities.GetFirst(a => a.NameEn.Equals(model.NameEn) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameEn");

            if (!model.GroupId.HasValue)
                bex.AddRequiredMessage("Group");

            if (model.Password != model.ConfirmPassword)
            {
                bex.StringMessage("Password doesn't equal confirm password");
            }

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
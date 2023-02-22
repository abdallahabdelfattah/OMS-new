using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;

namespace OMS.Web.Controllers
{
    public class GroupController : BaseController
    {
        // GET: Group
        public ActionResult Index()
        {
            ViewBag.SystemPages = uow.SystemPages.Get(C => C.PageActions.Count > 0 && C.ParentId == null && C.NameEn != "Home",
                null, c => c.Children, c => c.PageActions).Select(s => new UserSystemPageSummaryModel(s)).ToList();

            return View();
        }

        public JsonResult List(GroupSearchViewModel sc)
        {
            var result = uow.Groups.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new GroupSummaryModel(e)).ToList() });
        }

        public JsonResult Details(GroupSearchViewModel sc)
        {
            var result = uow.Groups.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new GroupModel(e)).ToList() });
        }

        public JsonResult Save(GroupModel model)
        {
            var entity = uow.Groups.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new Group();
                }
                else
                {
                    uow.GroupPageActions.Delete(entity.GroupPageActions);
                }
                model.FillEntity(entity);
                uow.Groups.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.Groups.DeleteById(Id);

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

        private void ValidateModel(GroupModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("NameAr");
            else if (uow.Countries.GetFirst(a => a.NameAr.Equals(model.NameAr, StringComparison.OrdinalIgnoreCase) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameAr");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("NameEn");
            else if (uow.Countries.GetFirst(a => a.NameEn.Equals(model.NameEn, StringComparison.OrdinalIgnoreCase) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameEn");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class VersionTypeController : BaseController
    {
        // GET: VersionType
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List(VersionTypeSearchViewModel sc)
        {
            var result = uow.VersionType.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new VersionTypeModel(e)).ToList() });
        }

        public JsonResult Details(VersionTypeSearchViewModel sc)
        {
            var result = uow.VersionType.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new VersionTypeModel(e)).ToList() });
        }

        public JsonResult Save(VersionTypeModel model)
        {
            var entity = uow.VersionType.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new VersionType();
                }

                model.FillEntity(entity);

                uow.VersionType.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.VersionType.DeleteById(Id);
                uow.Save();

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

        private void ValidateModel(VersionTypeModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.Name))
                bex.AddRequiredMessage("Name");
            else if (uow.VersionType.GetFirst(a => a.Name.Equals(model.Name) && a.Id != model.Id) != null)
                bex.AddExistsMessage("Name");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
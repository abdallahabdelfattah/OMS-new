using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OMS.Model.ViewModel;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;

namespace OMS.Web.Controllers
{
    public class SystemPageController : BaseController
    {
        // GET: SystemPage
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(SystemPageSearchViewModel sc)
        {
            var result = uow.SystemPages.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new SystemPageSummaryModel(e)).ToList() });
        }

        public JsonResult Details(SystemPageSearchViewModel sc)
        {
            var result = uow.SystemPages.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new SystemPageModel(e)).ToList() });
        }

        public JsonResult Save(SystemPageModel model)
        {
            var entity = uow.SystemPages.GetById(model.Id);

            try
            {
                if (entity == null)
                {
                    entity = new SystemPage();
                }
                model.FillEntity(entity);

                ValidateModel(model);
                uow.SystemPages.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.SystemPages.DeleteById(Id);

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
    }
}
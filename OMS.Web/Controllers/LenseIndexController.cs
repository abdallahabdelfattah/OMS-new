using OMS.Model.ViewModel;
using OMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.Web.Globalization;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel.SC;

namespace OMS.Web.Controllers
{
    public class LenseIndexController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(LenseIndexSearchViewModel sc)
        {
            var result = uow.LenseIndex.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new LenseIndexModel(e)).ToList() });
        }

        public JsonResult Details(LenseIndexSearchViewModel sc)
        {
            var result = uow.LenseIndex.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new LenseIndexModel(e)).ToList() });
        }

        public JsonResult Save(LenseIndexModel model)
        {
            var entity = uow.LenseIndex.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new LenseIndex();
                }

                model.FillEntity(entity);
                
                uow.LenseIndex.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.LenseIndex.DeleteById(Id);
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

        private void ValidateModel(LenseIndexModel model)
        {
            BusinessException bex = new BusinessException();

            if (model.Index.HasValue)
                bex.AddRequiredMessage("Index");
            else if (uow.LenseIndex.GetFirst(a => a.Index == model.Index) != null)
                bex.AddExistsMessage("NameAr");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
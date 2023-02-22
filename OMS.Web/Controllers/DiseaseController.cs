using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Resources;
using OMS.Web.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class DiseaseController : BaseController
    {
        // GET: Disease
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(DiseaseSearchViewModel sc)
        {
            var result = uow.Diseases.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new DiseaseSummaryModel(e)).ToList() });
        }

        public JsonResult Details(DiseaseSearchViewModel sc)
        {
            var result = uow.Diseases.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new DiseaseModel(e)).ToList() });
        }

        public JsonResult Save(DiseaseModel model)
        {
            var entity = uow.Diseases.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new Disease();
                }
                model.FillEntity(entity);
                
                uow.Diseases.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.Diseases.DeleteById(Id);
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

        private void ValidateModel(DiseaseModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage(AppResource.ArabicName);
            else if (uow.Diseases.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage(AppResource.ArabicName);

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage(AppResource.EnglishName);
            else if (uow.Diseases.GetFirst(a => a.NameAr.Equals(model.NameEn) && a.Id != model.Id) != null)
                bex.AddExistsMessage(AppResource.EnglishName);

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
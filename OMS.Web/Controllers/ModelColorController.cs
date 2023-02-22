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
using OMS.BLL;

namespace OMS.Web.Controllers
{
    public class ModelColorController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.BrandModels = uow.BrandModel.Get().Select(s => new LookupModel() { Id = s.Id, Name = s.ModelName }).ToList();
            ViewBag.Brands = uow.Brand.Get().Select(s => new LookupModel() { Id = s.Id, Name = s.BrandName }).ToList();
            ViewBag.ColorCategories = uow.ColorCategory.Get().Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.Categories = uow.Categories.Get().Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            return View();
        }

        public JsonResult List(ColorSearchViewModel sc)
        {
            var result = uow.Color.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new ColorSummeryViewModel(e)).ToList() });
        }
        public JsonResult UpdateBrand(long Id)
        {
            if (Id != null)
            {
                var result = uow.Brand.Get(d => d.ProductCategoryId == Id);
                return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.BrandName }).ToList());
            }
            return null;
        }

        public JsonResult GetBrandModels(long BrandId)
        {
            return Json(uow.BrandModel.Get(e => e.BrandId== BrandId)
                .Select(s => new LookupModel()
                    { Id = s.Id, Name =  s.ModelName }).ToList());
        }
        public JsonResult Details(ColorSearchViewModel sc)
        {
            var result = uow.Color.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new ColorViewModel(e)).ToList() });
        }

        public JsonResult Save(ColorViewModel model)
        {
            try
            {
                ValidateModel(model);

                var entity = model.MapFromViewModeToEntity(model);
                
                uow.Color.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.Color.DeleteById(Id);
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

        private void ValidateModel(ColorViewModel model)
        {
            BusinessException bex = new BusinessException();
            if (string.IsNullOrEmpty(model.ColorName))
                bex.AddRequiredMessage("Color Name");
            if (model.BrandId == null)
                bex.AddRequiredMessage("Brand");
            if (model.BrandModelId == null)
                bex.AddRequiredMessage("Model");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
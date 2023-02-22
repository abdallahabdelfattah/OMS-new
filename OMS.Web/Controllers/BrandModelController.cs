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
    public class BrandModelController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.Categories = uow.Categories.Get().Select(s => new LookupModel() { Id = s.Id,Name= CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.Brands = uow.Brand.Get().Select(s => new LookupModel() { Id = s.Id, Name = s.BrandName }).ToList();
            return View();
        }

        public JsonResult List(ModelSearchViewModel sc)
        {
            var result = uow.BrandModel.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new ModelSummeryViewModel(e)).ToList() });
        }

        public JsonResult Details(ModelSearchViewModel sc)
        {
            var result = uow.BrandModel.Search(sc);
            return Json(new { TotalCount = result.Count(), Data = result.Select(e => new BrandModelViewModel(e)).ToList() });
        }

        public JsonResult Save(BrandModelViewModel model)
        {
            try
            {
                ValidateModel(model);

                var entity = model.MapFromViewModeToEntity(model);
                
                uow.BrandModel.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.BrandModel.DeleteById(Id);
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
        public JsonResult UpdateBrand(long Id)
        {
            if (Id != null)
            {
                var result = uow.Brand.Get(d => d.ProductCategoryId == Id);
                return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.BrandName }).ToList());
            }
            return null;
        }

        private void ValidateModel(BrandModelViewModel model)
        {
            BusinessException bex = new BusinessException();
            if (string.IsNullOrEmpty(model.ModelName))
                bex.AddRequiredMessage("Model Name");
            else if (model.BrandId == null)
                bex.AddRequiredMessage("Brand");
            else if (uow.BrandModel.GetFirst(a => a.ModelName.ToLower().Equals(model.ModelName.ToLower()) && a.Id != model.Id&&a.BrandId==model.BrandId) != null)
                bex.AddExistsMessage("model Name excist");
            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
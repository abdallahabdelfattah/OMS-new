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

namespace OMS.Web.Controllers
{
    public class BrandController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.ProductCategorys = uow.Categories.Get().Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic? s.NameAr : s.NameEn }).ToList();

            return View();
        }

        public JsonResult List(BrandSearchViewModel sc)
        {
            var result = uow.Brand.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new BrandSummeryViewModel(e, CultureHelper.IsArabic)).ToList() });
        }

        public JsonResult Details(BrandSearchViewModel sc)
        {
            var result = uow.Brand.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new BrandViewModel(e, CultureHelper.IsArabic)).ToList() });
        }

        public JsonResult Save(BrandViewModel model)
        {
            try
            {
                ValidateModel(model);

                var entity = model.MapFromViewModeToEntity(model);
                
                uow.Brand.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.Brand.DeleteById(Id);
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

        private void ValidateModel(BrandViewModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.BrandName))
                bex.AddRequiredMessage("Brand Name");
            //else if (uow.Cities.GetFirst(a => a.NameAr.Equals(model.BrandName) && a.Id != model.Id) != null)
            //    bex.AddExistsMessage("Brand");
            else if (uow.Brand.GetFirst(a => a.BrandName.ToLower().Equals(model.BrandName.ToLower()) && a.ProductCategoryId!=model.ProductCategoryId  && a.Id != model.Id) != null)
                bex.AddExistsMessage($"Product Catergory Contains has {model.BrandName} ");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
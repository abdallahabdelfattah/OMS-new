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
    public class ColorCategoryController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.Brands = uow.Brand.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = s.BrandName}).ToList();

           


            return View();
        }

        public JsonResult List(BaseSearchViewModel sc)
        {
            var result = uow.ColorCategory.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new ColorCategoryModel(e)).ToList() });
        }

        public JsonResult Details(BaseSearchViewModel sc)
        {
            var result = uow.ColorCategory.Search(sc);
            return Json(new { TotalCount = result.Count(), Data = result.Select(e => new ColorCategoryModel(e)).ToList() });
        }

        public JsonResult Save(ColorCategoryModel model)
        {
            var entity = uow.ColorCategory.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new ColorCategoy();
                }
                model.FillEntity(entity);
                
                uow.ColorCategory.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.ColorCategory.DeleteById(Id);
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

        public JsonResult GetBrandModels(int BrandId)
        {
           var BrandModels =  uow.BrandModel.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.ModelName }).ToList();

            return Json(BrandModels);
        }

        private void ValidateModel(ColorCategoryModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("NameAr");
            else if (uow.ColorCategory.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameAr");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("NameEn");
            else if (uow.ColorCategory.GetFirst(a => a.NameEn.Equals(model.NameEn) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameEn");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
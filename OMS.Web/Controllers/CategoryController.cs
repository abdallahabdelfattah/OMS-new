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
    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.AllCategories = uow.Categories.Search(new CategorySearchViewModel())
                .Select(c => new LookupModel() { Id = c.Id, Name = CultureHelper.IsArabic? c.NameAr: c.NameEn }).ToList();
            return View();
        }

        public JsonResult List(CategorySearchViewModel sc)
        {
            var result = uow.Categories.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new CategorySummaryModel(e)).ToList() });
        }

        public JsonResult Details(CategorySearchViewModel sc)
        {
            var result = uow.Categories.Search(sc);
            return Json(new { TotalCount = result.Count, Data = result.Select(e => new CategoryModel(e)).ToList() });
        }

        public JsonResult Save(CategoryModel model)
        {
            var entity = uow.Categories.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new ProductCategory();
                }
                model.FillEntity(entity);
                
                uow.Categories.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.Categories.DeleteById(Id);
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

        private void ValidateModel(CategoryModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage(AppResource.ArabicName);
            else if (uow.Categories.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage(AppResource.ArabicName);

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage(AppResource.EnglishName);
            else if (uow.Categories.GetFirst(a => a.NameEn.Equals(model.NameEn) && a.Id != model.Id) != null)
                bex.AddExistsMessage(AppResource.EnglishName);

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
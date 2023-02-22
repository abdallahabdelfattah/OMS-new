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
    public class PowerController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.Categories = uow.Categories.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Brands = new List<LookupModel>();
            ViewBag.Models = new List<LookupModel>();


            return View();
        }

        public JsonResult List(PowerSearchViewModel sc)
        {
            var result = uow.Power.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new PowerSummaryModel(e, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });
        }

        public JsonResult Details(PowerSearchViewModel sc)
        {
            var result = uow.Power.Search(sc);
            return Json(new { TotalCount = result.Count(), Data = result.Select(e => new PowerModel(e, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });

        }

        public JsonResult Save(PowerModel model)
        {
            var entity = uow.Power.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new Power();
                }
                model.FillEntity(entity);
                
                uow.Power.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.Power.DeleteById(Id);
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
        public JsonResult UpdateModels(long Id)
        {
            var result = uow.BrandModel.Get(d => d.BrandId == Id);
            return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.ModelName }).ToList());
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

        private void ValidateModel(PowerModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.Value))
                bex.AddRequiredMessage("Value");         
            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
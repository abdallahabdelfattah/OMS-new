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
    public class RegionController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.Countries = uow.Countries.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            
            return View();
        }

        public JsonResult List(RegionSearchViewModel sc)
        {
            var result = uow.Regions.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new RegionSummaryModel(e)).ToList() });
        }

        public JsonResult Details(RegionSearchViewModel sc)
        {
            var result = uow.Regions.Get(e=>e.Id == sc.Id,null,e=>e.City,e=>e.City.Country);
            return Json(new { TotalCount = result.Count(), Data = result.Select(e => new RegionModel(e)).ToList() });
        }

        public JsonResult Save(RegionModel model)
        {
            var entity = uow.Regions.GetById(model.Id);

            try
            {
                ValidateModel(model);
                if (entity == null)
                {
                    entity = new Region();
                }
                model.FillEntity(entity);

                uow.Regions.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.Regions.DeleteById(Id);

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

        public JsonResult GetCities(long CountryId)
        {
            return Json(uow.Cities.Get(e => e.CountryId == CountryId)
                .Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList());
        }

        private void ValidateModel(RegionModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("NameAr");
            else if (uow.Regions.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameAr");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("NameEn");
            else if (uow.Regions.GetFirst(a => a.NameEn.Equals(model.NameEn) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameEn");
            
            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
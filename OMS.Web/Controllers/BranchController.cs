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
    public class BranchController : BaseController
    {
        // GET: Branch
        public ActionResult Index()
        {
            ViewBag.Countries = uow.Countries.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.Cities = uow.Cities.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.Regions = uow.Regions.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.Employees = uow.Users.Get().Select(s => new LookupModel()
                {Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn}).ToList();
            return View();
        }

        public JsonResult List(BranchSearchViewModel sc)
        {
            var result = uow.Branches.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new BranchSummaryModel(e)).ToList() });
        }

        public JsonResult Details(BranchSearchViewModel sc)
        {
            var result = uow.Branches.Get(e => e.Id == sc.Id, null, e => e.Region, e => e.Region.City, e => e.Region.City.Country);
            return Json(new { TotalCount = result.Count(), Data = result.Select(e => new BranchModel(e)).ToList() });
        }

        public JsonResult Save(BranchModel model)
        {
            var entity = uow.Branches.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new Branch();
                }
                model.FillEntity(entity);
                uow.Branches.AddOrUpdate(entity, e => e.Id == model.Id);
                //ToDO fix this 
                uow.Warehouses.AddOrUpdate(entity.Warehouse, e => e.Id == model.Id);
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
                uow.Branches.DeleteById(Id);
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

        public JsonResult GetCities(long CountryId)
        {
            return Json(uow.Cities.Get(e => e.CountryId == CountryId)
                .Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList());
        }

        public JsonResult GetRegions(long CountryId)
        {
            return Json(uow.Regions.Get(e => e.CountryId == CountryId)
                .Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList());
        }

        private void ValidateModel(BranchModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("Name");
            else if (uow.Cities.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage("Name");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("Arabic Title");
            else if (uow.Cities.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage("Name");

            if (!model.CountryId.HasValue)
                bex.AddRequiredMessage("Country");
            if (!model.CityId.HasValue)
                bex.AddRequiredMessage("City");
            if (!model.RegionId.HasValue)
                bex.AddRequiredMessage("Region");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }


    }
}
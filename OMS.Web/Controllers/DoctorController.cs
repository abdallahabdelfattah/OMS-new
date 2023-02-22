using OMS.Model.ViewModel;
using OMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class DoctorController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {

            ViewBag.Countries = uow.Countries.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            return View();
        }

        public JsonResult List(DoctorSearchViewModel vm)
        {
            var result = uow.Doctors.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new DoctorSummaryModel(p, CultureHelper.CurrentLang)).ToList()
            });
        }

        public JsonResult GetCities(long CountryId)
        {
            return Json(uow.Cities.Get(e => e.CountryId == CountryId)
                .Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList());
        }

        public JsonResult GetRegions(long CityId)
        {
            return Json(uow.Regions.Get(e => e.CityId == CityId)
                .Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList());
        }

        public JsonResult Details(DoctorSearchViewModel vm)
        {
            var result = uow.Doctors.Get(d => d.Id == vm.Id, null, d => d.Region, d => d.Region.City);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new DoctorViewModel(p, CultureHelper.CurrentLang)).ToList()
            });
        }

        public JsonResult Save(DoctorViewModel model)
        {
            var mappedEntity = uow.Doctors.MapViewModelToEntity(model);

            try
            {
                uow.Doctors.AddOrUpdate(mappedEntity, e => e.Id == model.Id);
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

        public JsonResult Delete(long ID)
        {
            try
            {

                uow.Doctors.DeleteById(ID);
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
    }
}
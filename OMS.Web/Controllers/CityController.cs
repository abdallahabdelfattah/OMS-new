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
    public class CityController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            ViewBag.Countries = uow.Countries.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            return View();
        }

        public JsonResult List(CitySearchViewModel sc)
        {
            var result = uow.Cities.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new CitySummaryModel(e)).ToList() });
        }

        public JsonResult Details(CitySearchViewModel sc)
        {
            var result = uow.Cities.Search(sc);
            return Json(new { TotalCount = result.Count(), Data = result.Select(e => new CityModel(e)).ToList() });
        }

        public JsonResult Save(CityModel model)
        {
            var entity = uow.Cities.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new City();
                }
                model.FillEntity(entity);
                
                uow.Cities.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.Cities.DeleteById(Id);
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

        private void ValidateModel(CityModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("NameAr");
            else if (uow.Cities.GetFirst(a => a.NameAr.Equals(model.NameAr) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameAr");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("NameEn");
            else if (uow.Cities.GetFirst(a => a.NameEn.Equals(model.NameEn) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameEn");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
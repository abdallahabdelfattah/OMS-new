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
    public class SupplierController : BaseController
    {
        // GET: Region
        public ActionResult Index()
        {
            var regions = uow.Regions.Get();
            ViewBag.Regions = regions.Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            return View();
        }

        public JsonResult List(SupplierSearchViewModel sc)
        {
            var result = uow.Suppliers.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new SupplierSummaryModel(e)).ToList() });
        }

        public JsonResult Details(SupplierSearchViewModel sc)
        {
            var result = uow.Suppliers.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new SupplierModel(e)).ToList() });
        }

        public JsonResult Save(SupplierModel model)
        {
            var entity = uow.Suppliers.GetById(model.Id);
            if (entity == null)
                entity = new Supplier();

            model.FillEntity(entity);

            try
            {
                ValidateModel(model);
                uow.Suppliers.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.Suppliers.DeleteById(Id);

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
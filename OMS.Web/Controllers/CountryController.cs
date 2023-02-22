using OMS.Model.ViewModel;
using OMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;

namespace OMS.Web.Controllers
{
    public class CountryController : BaseController
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(CountrySearchViewModel vm)
        {
            var result = uow.Countries.Search(vm);
            return Json(new {TotalCount = result.Count(),Data = result.Select(e => new CountrySummaryModel(e)).ToList()});
        }

        public JsonResult Details(CountrySearchViewModel vm)
        {
            var result = uow.Countries.Search(vm);
            return Json(new { TotalCount = 1, Data = result.Select(e => new CountryModel(e)).ToList() });
        }

        [HttpPost]
        public JsonResult Save(CountryModel model)
        {
            using (var uow= new OMSUoW())
            {
                var entity = uow.Countries.GetById(model.Id);

                try
                {
                    ValidateModel(model);
                    
                    if (entity == null)
                    {
                        entity = new Country();
                    }
                    
                    uow.Countries.Add(uow.Countries.MapViewModelToEntity(model));
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
        }

        public JsonResult Delete(long Id)
        {
            try
            {
                uow.Countries.DeleteById(Id);
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

        private void ValidateModel(CountryModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage("NameAr");
            else if (uow.Countries.GetFirst(a => a.NameAr.Equals(model.NameAr, StringComparison.OrdinalIgnoreCase) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameAr");

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage("NameEn");
            else if (uow.Countries.GetFirst(a => a.NameEn.Equals(model.NameEn, StringComparison.OrdinalIgnoreCase) && a.Id != model.Id) != null)
                bex.AddExistsMessage("NameEn");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
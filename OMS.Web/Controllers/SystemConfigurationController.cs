using OMS.Model.ViewModel;
using OMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Resources;

namespace OMS.Web.Controllers
{
    public class SystemConfigurationController : BaseController
    {
        // GET: SystemConfiguration
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Details()
        {
            var result = uow.SystemConfigurations.Get();
            return Json(new { TotalCount = 1, Data = result.Select(e => new SystemConfigurationModel(e)).ToList() });
        }

        [HttpPost]
        public JsonResult Save(SystemConfigurationModel model)
        {
            using (var uow = new OMSUoW())
            {
                var entity = uow.SystemConfigurations.GetById(model.Id);

                try
                {
                    ValidateModel(model);

                    if (entity == null)
                    {
                        entity = new SystemConfiguration();
                    }
                    model.FillEntity(entity);
                    uow.SystemConfigurations.AddOrUpdate(entity, e => e.Id == model.Id);
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

        private void ValidateModel(SystemConfigurationModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.CompanyNameAr))
                bex.AddRequiredMessage(AppResource.ArabicName);
            
            if (string.IsNullOrEmpty(model.CompanyNameEn))
                bex.AddRequiredMessage(AppResource.EnglishName);
            
            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
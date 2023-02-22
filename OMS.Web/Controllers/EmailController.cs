using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;
using OMS.Resources;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class EmailController : BaseController
    {
        // GET: email
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(EmailSearchViewModel vm)
        {
            var result = uow.Email.Search(vm);

            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new EmailViewModel()).ToList()
            });
        }

        public ActionResult Save()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult GetAllCustomer(EmailViewModel model)
        {
            var result = uow.Email.AddCustomerToReceiversList(model);
            return Json(new
            {   
                data = result.EmailReceiversModel
            });
        }

        //public ActionResult GetAllDoctors(EmailViewModel model)
        //{
        //    //var result = uow.Email.AddDoctorsToReceiversList(model);
        //    //return Json(new
        //    //{
        //    //    data = result.EmailReceiversModel
        //    //});
        //}
        public ActionResult GetAllEmployees(EmailViewModel model)
        {
            var result = uow.Email.AddEmployeesToReceiversList(model);
            return Json(new
            {
                data = result.EmailReceiversModel
            });
        }

        public ActionResult GetAllSuppliers(EmailViewModel model)
        {
            var result = uow.Email.AddSuppliersToReceiversList(model);
            return Json(new
            {
                data = result.EmailReceiversModel
            });
        }

        public ActionResult FillEmailReceiversList(EmailViewModel model)
        {
            var result = uow.Email.FillEmailReceiversList(model);
            return Json(new
            {
                data = result.EmailReceiversModel
            });
        }
    }
}
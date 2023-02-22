using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;

namespace OMS.Web.Controllers
{
    public class SmsController : BaseController
    {
        // GET: Messages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(SmsSearchViewModel vm)
        {
            var result = uow.Sms.Search(vm);

            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new SmsViewModel()).ToList()
            });
        }

        public ActionResult Save()
        {
            throw new NotImplementedException();
        }

        public ActionResult Delete()
        {
            throw new NotImplementedException();
        }

        public ActionResult Details()
        {
            throw new NotImplementedException();
        }
    }
}
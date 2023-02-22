
using System;
using System.Collections.Generic;
using System.Linq;
using OMS.Web.Globalization;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Categories = uow.Categories.Count();
            ViewBag.PurchasesCount = uow.PurchaseTransactionMaster.Count();
            ViewBag.SalesCount = uow.SalesTransactions.Count();
            ViewBag.MedicalLensCount = uow.MedicalLensMaster.Count();
            return View();
        }
    }
}
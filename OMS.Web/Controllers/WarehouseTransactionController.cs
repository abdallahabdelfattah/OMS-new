using OMS.Model.ViewModel;
using OMS.Web.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class WarehouseTransactionController : BaseController
    {
        // GET: WarehouseTransaction
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(WarehouseTransactionSearchViewModel sc)
        {
            var result = uow.WarehouseTransactions.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(p => new WarehouseTransactionSummaryModel(p,CultureHelper.CurrentLanguage)).ToList() });
        }

        public JsonResult Details(WarehouseTransactionSearchViewModel sc)
        {
            var result = uow.WarehouseTransactions.Get(e => e.Id == sc.Id);
            return Json(new { TotalCount = 1, Data = result.Select(p => new WarehouseTransactionModel(p,CultureHelper.CurrentLanguage)).ToList() });
        }
    }
}
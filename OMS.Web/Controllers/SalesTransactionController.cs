using OMS.Model.ViewModel;
using OMS.Web.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class SalesTransactionController : BaseController
    {
        // GET: SalesTransactionMaster
        public ActionResult Index()
        {
            ViewBag.Warehouses = uow.Warehouses.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic? s.NameAr: s.NameEn }).ToList();
            
            ViewBag.Customers = uow.Customers.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            
            return View();
        }

        public JsonResult List(SalesTransactionSearchViewModel sc)
        {
            var result = uow.SalesTransactions.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new SalesTransactionSummaryModel(e, CultureHelper.CurrentLang)).ToList() });
        }
        public JsonResult Details(SalesTransactionSearchViewModel sc)
        {
            var result = uow.SalesTransactions.Get(e => e.Id == sc.Id);
            return Json(new { TotalCount = 1, Data = result.Select(e => new SalesTransactionModel(e,CultureHelper.CurrentLang)).ToList() });
        }
    }
}
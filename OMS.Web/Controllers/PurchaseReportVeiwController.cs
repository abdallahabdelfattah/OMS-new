using System.Linq;
using System.Web.Mvc;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class PurchaseReportVeiwController : BaseController
    {
        [Route("PurchaseReport/Index")]
        public ActionResult Index()
        {
            FillViewBags();
            return View();
        }
        public JsonResult List(PurchaseReportViewSearchViewModel vm)
        {
            var result = uow.PurchaseReportView.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new PurchaseReportViewSummeryModel(p, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });
        }
        private void FillViewBags()
        {
            ViewBag.Suppliers = uow.Suppliers.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();
            ViewBag.Warehouses = uow.Warehouses.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();
            ViewBag.Products = uow.Products.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();
            ViewBag.Currencies = uow.Currencies.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();
        }
    }
}
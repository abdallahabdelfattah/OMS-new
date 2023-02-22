using System.Linq;
using System.Web.Mvc;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class SalesTrackingReportViewController : BaseController
    {
        //[Route("SalesTrackingReport/Index")] 
        public ActionResult Index()
        {
            FillViewBags();
            return View();
        }
        public JsonResult List(SalesTrackingReportViewSearchViewModel vm)
        {
            var result = uow.SalesTrackingReportView.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new SalesTrackingReportViewSummeryModel(p)).ToList()
            });
        }
        private void FillViewBags()
        {
            
            ViewBag.Warehouses = uow.Warehouses.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();         
        }
    }
}
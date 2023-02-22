//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Repositories;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class MedicalLensDetailsController : BaseController
    {
        public ActionResult Index(long id)
        {
            var medicalLensMasterObj = uow.MedicalLensMaster.GetById(id);
            fillViewBags();
            return View(medicalLensMasterObj);
        }
        public ActionResult GetMedicalLensDetailsList(int id, int typeId=1)
        {
            var model = uow.MedicalLensDetails.PrepareDtails(id, typeId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        
        [HttpPost]
        public ActionResult SaveMedicalLensDetails(List<MedicalLensDetail> _data )
        {
            var detailsObj = new MedicalLensDetailsListVM();
            detailsObj.MedicalLensDetailsList = _data; 
            uow.MedicalLensDetails.SaveLensDetails(_data);
            return Json(new { message = "done" });
        }
        private void fillViewBags()
        {      
            ViewBag.DetailsType = uow.MedicalLensDtailsType.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn  }).ToList();
        }
    }
}
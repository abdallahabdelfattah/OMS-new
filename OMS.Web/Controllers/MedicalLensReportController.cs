//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Resources;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class MedicalLensReportController : BaseController
    {
        public ActionResult Index(MedicalLenseReportModel model)
        {
            //model = uow.MedicalLensMaster.GetReport(model);
            return View();
        }
        
        public JsonResult GetDocumentNumber()
        {
            var documentNo = uow.MedicalLensMaster.GetDocumentNumber();
            return Json(documentNo);
        }

        private void ValidateModel(MedicalLensMaster model)
        {
            BusinessException bex = new BusinessException();

        }

       
    }
}
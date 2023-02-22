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
using OMS.Web.Globalization;
using OfficeOpenXml;

namespace OMS.Web.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index()
        {
            FillViewBags();
            return View();
        }

        private void FillViewBags()
        {
            ViewBag.Doctors = uow.Doctors.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.CustomerTitles = uow.CustomerTitles.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.Genders = new List<LookupModel> {
                new LookupModel{Id=1,Name=CultureHelper.IsArabic?"ذكر":"Male"},
                new LookupModel{Id=2,Name=CultureHelper.IsArabic?"أنثى":"Female"}};
            ViewBag.Nationalities = uow.Nationalities.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.CustomerDiseases = uow.Diseases.Get().Select(s => new
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn, Selected = false }).ToList();
        }

        public JsonResult List(CustomerSearchViewModel vm)
        {
            var result = uow.Customers.Search(vm);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new CustomerSummaryModel(e, CultureHelper.CurrentLang)).ToList() });
        }

        public JsonResult Details(CustomerSearchViewModel vm)
        {
            var result = uow.Customers.Get(e => e.Id == vm.Id, null, e => e.Examinations);
            return Json(new { TotalCount = 1, Data = result.Select(e => new CustomerModel(e, CultureHelper.CurrentLang)).ToList() });
        }

        [HttpPost]
        public JsonResult Save(CustomerModel model)
        {
            using (var uow = new OMSUoW())
            {
                var entity = uow.Customers.InsertOrUpdate(model);

                try
                {
                    ValidateModel(model);

                    
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

        public JsonResult Delete(long Id)
        {
            try
            {
                uow.Customers.DeleteById(Id);
                uow.Save();

                return Json(new { Exceptions = new List<string>() });
            }
            catch (BusinessException bexp)
            {
                return Json(new { Exceptions = bexp.Exceptions });
            }
            catch (Exception ex)
            {
                List<string> lstExceptions = new List<string>();
                lstExceptions.Add(ex.Message);
                return Json(new { Exceptions = lstExceptions });
            }
        }

        public JsonResult GetDocumentNumber()
        {
            var documentNo = uow.Customers.GetDocumentNumber();
            return Json(documentNo);
        }

        public void Export(CustomerSearchViewModel vm)
        {
            vm.RowCount = 50000;//get all customers

            var result = uow.Customers.Search(vm).Select(e => new CustomerSummaryModel(e, CultureHelper.CurrentLang)).ToList();
            ExcelPackage pack = new ExcelPackage();
            pack.Workbook.Worksheets.Add(AppResource.ManageCustomers);
            ExcelWorksheet ws = pack.Workbook.Worksheets[0];
            ws.Name = AppResource.ManageCustomers;
            ws.View.RightToLeft = CultureHelper.IsArabic;
            ws.Cells["A1:E1"].Style.Font.Bold = true;
            ws.Cells["A1:E1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["A1:E1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            ws.Cells["A1:E1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["A1:E1"].Style.HorizontalAlignment = CultureHelper.IsArabic ? OfficeOpenXml.Style.ExcelHorizontalAlignment.Right: OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            ws.Cells["A1"].Value = AppResource.Code;
            ws.Cells["B1"].Value = AppResource.ArabicName;
            ws.Cells["C1"].Value = AppResource.EnglishName;
            ws.Cells["D1"].Value = AppResource.Email;
            ws.Cells["E1"].Value = AppResource.Mobile;
            int rownumber = 2;

            foreach (var item in result)
            {
                ws.Cells["A" + rownumber].Value = item.Code;
                ws.Cells["B" + rownumber].Value = item.NameAr;
                ws.Cells["C" + rownumber].Value = item.NameEn;
                ws.Cells["D" + rownumber].Value = item.Email;
                ws.Cells["E" + rownumber].Value = item.Mobile;
                rownumber++;
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", $"attachment;  filename={AppResource.ManageCustomers}.xlsx");
                pack.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            
        }

        private void ValidateModel(CustomerModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.NameAr))
                bex.AddRequiredMessage(AppResource.ArabicName);
            
            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage(AppResource.EnglishName);
    
            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
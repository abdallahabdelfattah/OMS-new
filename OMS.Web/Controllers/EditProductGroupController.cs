//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using OMS.Resources;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class EditProductGroupController : BaseController
    {
        public ActionResult Index()
        {
            fillViewBags();
            return View();
        }
        public JsonResult List(EditProductGroupSearchViewModel vm)
        {
            var result = uow.Products.ProductGroupSearch(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new EditProductGroupSummaryModel(p)).ToList()
            });
        }

       
        public async  Task<JsonResult> SaveAsync(EditProductGroupSearchViewModel vm, decimal newValue,int column, int operation)
        {
          

            try
            {
                 
                var col = (EditProductColumnEnum)column;
                var op = (OperationTypeEnum)operation;
                await  uow.Products.EditProductGroupSearch(vm, newValue, col, op);
                return Json(new { Model = vm, Exceptions = new List<string>() });

            }
            catch (BusinessException bexp)
            {
                return Json(new { item = vm, Exceptions = bexp.Exceptions });
            }
            catch (Exception ex)
            {
                List<string> lstExceptions = new List<string>();
                lstExceptions.Add(ex.Message);
                return Json(new { Model = vm, Exceptions = lstExceptions });
            }
        }
              
        

        

        public JsonResult GetDocumentNumber()
        {
            var documentNo = uow.Products.GetDocumentNumber();
            return Json(documentNo);
        }

        private void ValidateModel(EditProductGroupModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.Code))
                bex.AddRequiredMessage(AppResource.Code);

            if (string.IsNullOrEmpty(model.Name))
                bex.AddRequiredMessage(AppResource.ArabicName);

            if (string.IsNullOrEmpty(model.NameEn))
                bex.AddRequiredMessage(AppResource.EnglishName);

            if (!model.CategoryId.HasValue)
                bex.AddRequiredMessage(AppResource.Category);

            if (model.OfficialPrice <= 0)
                bex.AddRequiredMessage(AppResource.OfficialPrice);
           

            if (bex.Exceptions.Count > 0)
                throw bex;
        }

        private void fillViewBags()
        {
            ViewBag.Categories = uow.Categories.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Suppliers = uow.Suppliers.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();           

            ViewBag.Colors = uow.Color.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.ColorName }).ToList();
            
            ViewBag.Models = uow.BrandModel.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.ModelName }).ToList();
            
            ViewBag.Brands = uow.Brand.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.BrandName }).ToList();
            ViewBag.EditTypes = new List<LookupModel>
            {
                new LookupModel { Id=1,Name="تعديل سعر البيع"},
                new LookupModel { Id = 2, Name = "تعديل الكميه" }
             };

            ViewBag.Operations = new List<LookupModel>
            {
                new LookupModel { Id=1,Name="زياده بمقدار"},
                new LookupModel { Id = 2, Name = "نقص بمقدار" },
                new LookupModel { Id = 3, Name = "زياده بنسبه " },
                new LookupModel { Id = 4, Name = "نقص بنسبه" }
             };


        }
    }
}
//using OfficeOpenXml;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Resources;
using OMS.Web.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OMS.Model.SystemEnums;

namespace OMS.Web.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult Index()
        {
            fillViewBags();
            return View();
        }
        public JsonResult List(ProductSearchViewModel vm)
        {
            var result = uow.Products.Search(vm);
            return Json(new
            {
                TotalCount = result.TotalItemCount,
                Data = result.Select(p => new ProductSummaryModel(p, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });
        }

        public JsonResult Details(ProductSearchViewModel vm)
        {
            var result = uow.Products.Get(d => d.Id == vm.Id, null, d => d.ProductCategory, d => d.Supplier);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new ProductViewModel(p, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });
        }
        public JsonResult UpdateBrand(long? Id)
        {
            if(Id != null)
            {
                var result = uow.Brand.Get(d => d.ProductCategoryId == Id);
                return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.BrandName }).ToList());
            }
            return null;
        }
        public JsonResult UpdateColorCategory(long? Id)
        {
            if (Id != null)
            {
                var result = uow.Color.Get(d => d.ModelId == Id);
                return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.ColorName }).ToList());
            }
            return null;
        }
        public JsonResult UpdateModels(long? Id)
        {
            var result = uow.BrandModel.Get(d => d.BrandId == Id);
            return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.ModelName }).ToList());
        }
       
        public JsonResult UpdateColors(long? Id)
        {
            var result = uow.Color.Get(d => d.ModelId == Id);
            return Json(result.Select(s => new LookupModel() { Id = s.Id, Name = s.ColorName }).ToList());
        }
        public FileStreamResult GenerateBarCode(string barcode)
        {
            var Content = Commons.Framework.Drawing.Barcode.GenerateBarCode(barcode, 100, 50);
            Stream stream = new MemoryStream(Content);
            FileStreamResult file = new FileStreamResult(stream, "data:image/png;base64,{0}");
            file.FileDownloadName = $"BarCodeFile_{barcode}.png";
            return file;
        }
        public JsonResult Save(ProductViewModel model)
        {
            var entity = uow.Products.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new Product();
                }
                model.FillEntity(entity);

                uow.Products.AddOrUpdate(entity, e => e.Id == model.Id);
                uow.Save();
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

        public JsonResult Delete(long ID)
        {
            try
            {
                uow.Products.DeleteById(ID);
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
            var documentNo = uow.Products.GetDocumentNumber();
            return Json(documentNo);
        }

        private void ValidateModel(ProductViewModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.Code))
                bex.AddRequiredMessage(AppResource.Code);


            if (!model.CategoryId.HasValue)
                bex.AddRequiredMessage(AppResource.Category);


            if (bex.Exceptions.Count > 0)
                throw bex;
        }

        private void fillViewBags()
        {
            ViewBag.Categories = uow.Categories.Get(a => a.NameEn.ToLower() != "cl").Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Suppliers = uow.Suppliers.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Materials = uow.Material.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            ViewBag.Colors = uow.Color.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.ColorName }).ToList();

            ViewBag.Models = uow.BrandModel.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.ModelName }).ToList();

            ViewBag.Brands = uow.Brand.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.BrandName }).ToList();

            ViewBag.CLSPAndSolutionTypes = uow.CLSPAndSolutionType.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Powers = uow.Power.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Value }).ToList();

            ViewBag.UsePeriods = uow.UsePeriod.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.ProductGrads = uow.Grade.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            //Business hint: Color category is the models where it's category is CL 
            ViewBag.ColorCategories = uow.BrandModel.Get(a=>a.Brand.ProductCategoryId == (int)ProductCategories.CL).Select(s => new LookupModel()
            { Id = s.Id, Name = s.ModelName }).ToList();
            
            ViewBag.Warehouses = uow.Warehouses.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();



        }
}
}
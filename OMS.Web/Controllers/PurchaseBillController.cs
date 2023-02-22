using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PurchaseBillController : BaseController
    {
        // GET: PurchaseBill
        public ActionResult Index(PurchaseTransactionMasterViewModel model)
        {
            FillViewBags();
            var documentNo = uow.PurchaseTransactionMaster.GetDocumentNumber();
            model.InvoiceNo = documentNo;
            return View(model);
        }

        private void FillViewBags()
        {
            ViewBag.Suppliers = uow.Suppliers.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();
            ViewBag.Brands = uow.Brand.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.BrandName }).ToList();

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
            ViewBag.MedicalLenses = uow.MedicalLensMaster.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.Currencies = uow.Currencies.Get().Select(s => new LookupModel()
            {
                Id = s.Id,
                Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn
            }).ToList();
            ViewBag.SPHs = uow.SPH.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            ViewBag.CYLs = uow.CYL.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();
        }
        public JsonResult Save(PurchaseTransactionMasterViewModel model)
        {
            var mappedEntity = uow.PurchaseTransactionMaster.MapViewModelToEntity(model);

            try
            {
                uow.PurchaseTransactionMaster.AddNewPurchase(model);
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
        public JsonResult GetDocumentNumber()
        {
            var documentNo = uow.PurchaseTransactionMaster.GetDocumentNumber();
            return Json(documentNo);
        }


        public ActionResult GetProductDetail(string selectedProductId)
        {
            //get product details
            if (string.IsNullOrEmpty(selectedProductId))
            {
                BusinessException bex = new BusinessException();
                bex.AddRequiredMessage(AppResource.Product);
            }
            var result = uow.Products.GetById(long.Parse(selectedProductId));

            return Json(new PurchaseTransactionDetailsSummeryModel
            {
                ProductId = result.Id,
                ProductName = CultureHelper.IsArabic ? result.NameAr : result.NameEn,
                OfficialUnitPrice = result.OfficialPrice,
                ChangeRate = 0,
                Qty = 1,
                PurchasePrice = result.OfficialPrice,

            });
        }
        public ActionResult GetMedicalLensDetail(string selectedProductId)
        {
            //get product details
            if (string.IsNullOrEmpty(selectedProductId))
            {
                BusinessException bex = new BusinessException();
                bex.AddRequiredMessage(AppResource.Product);
            }
            var result = uow.MedicalLensMaster.GetById(long.Parse(selectedProductId));

            return Json(new PurchaseTransactionDetailsSummeryModel
            {
                MedicalLensId = result.Id,
                ChangeRate = 0,
                Qty = 1,

            });
        }

        private void ValidateModel(PurchaseTransactionMaster model)
        {
            BusinessException bex = new BusinessException();

            if (!model.InvoiceAmount.HasValue)
                bex.AddRequiredMessage(AppResource.InvoiceAmount);
            //if (!model.SupplierInvoiceDate.HasValue)
            //    bex.AddRequiredMessage(AppResource.InvoiceDate);
            if (string.IsNullOrEmpty(model.SupplierInvoiceNo))
                bex.AddRequiredMessage(AppResource.SupplierInvoiceNo);
            if (!model.SupplierId.HasValue)
                bex.AddRequiredMessage(AppResource.Supplier);
            if (!model.WarehouseId.HasValue)
                bex.AddRequiredMessage(AppResource.Warehouse);
            if (model.PurchaseTransactionDetails == null || model.PurchaseTransactionDetails?.Count() == 0)
                bex.AddRequiredMessage(AppResource.Product);

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
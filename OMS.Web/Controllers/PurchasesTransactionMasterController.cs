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
    public class PurchasesTransactionMasterController : BaseController
    {
        // GET: Purchases
        public ActionResult Index()
        {
            FillViewBags();
            return View();
        }
        //public ActionResult Add()
        //{
        //    FillViewBags();
        //    return View();
        //}
        public JsonResult List(PurchaseTransactionMasterSearchViewModel vm)
        {
            FillViewBags();
            var result = uow.PurchaseTransactionMaster.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new PurchaseTransactionMasterSearchViewModel(p,CultureHelper.IsArabic?"ar":"en")).ToList()
            });
        }
        
        public ActionResult Details(PurchaseTransactionMasterSearchViewModel vm)
        {
            var result = uow.PurchaseTransactionMaster.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new PurchaseTransactionMasterViewModel(p)).ToList()
            });
        }

        public JsonResult Save(PurchaseTransactionMasterViewModel model)
        {
            var mappedEntity = uow.PurchaseTransactionMaster.MapViewModelToEntity(model);

            try
            {
                ValidateModel(mappedEntity);
                //save purchase transaction details
                foreach (var detail in mappedEntity.PurchaseTransactionDetails)
                {
                    uow.PurchaseTransactionDetails.AddOrUpdate(detail, d => d.Id == detail.Id);
                }
                //save purchase transaction master
                uow.PurchaseTransactionMaster.AddOrUpdate(mappedEntity, e => e.Id == model.Id);
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

                uow.PurchaseTransactionMaster.DeleteById(ID);
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
        private void ValidateModel(PurchaseTransactionMaster model)
        {
            BusinessException bex = new BusinessException();

            if (!model.InvoiceAmount.HasValue)
                bex.AddRequiredMessage(AppResource.InvoiceAmount);
            if (!model.SupplierInvoiceDate.HasValue)
                bex.AddRequiredMessage(AppResource.InvoiceNo); 
            if (string.IsNullOrEmpty(model.InvoiceNo))
                bex.AddRequiredMessage(AppResource.InvoiceNo);
            if (string.IsNullOrEmpty(model.SupplierInvoiceNo))
                bex.AddRequiredMessage(AppResource.SupplierInvoiceNo);
            if (!model.SupplierId.HasValue)
                bex.AddRequiredMessage(AppResource.Supplier);
            if (!model.WarehouseId.HasValue)
                bex.AddRequiredMessage(AppResource.Warehouse);
            if (model.PurchaseTransactionDetails ==null || model.PurchaseTransactionDetails?.Count ==0)
                bex.AddRequiredMessage(AppResource.Product);

            if (bex.Exceptions.Count > 0)
                throw bex;
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
        public JsonResult GetDocumentNumber()
        {
            var documentNo = uow.PurchaseTransactionMaster.GetDocumentNumber();
            return Json(documentNo);
        }
    }
}
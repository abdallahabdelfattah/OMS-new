using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using OMS.Resources;
using OMS.Web.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class SalesReturnController : BaseController
    {
        // GET: SalesReturn
        public ActionResult Index()
        {
            ViewBag.Warehouses = uow.Warehouses.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic? s.NameAr: s.NameEn }).ToList();
            
            ViewBag.Customers = uow.Customers.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            
            return View();
        }

        public JsonResult Save(SalesInvoiceModel model)
        {
            var entity = uow.SalesTransactions.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new SalesTransaction();
                }
                model.FillEntity(entity);

                entity.Type = model.IsSalesRequest ? (byte)SalesTransactionEnum.SalesRequest : (byte)SalesTransactionEnum.SalesInvoice;



                //add warehouse subtract and update product stock
                WarehouseTransaction warehouseTrans = new WarehouseTransaction
                {
                    CustomerId = model.CustomerId,
                    Date = model.Date,
                    DocNo = model.DocNo,
                    EmployeeName = model.WarehouserName,
                    WarehouseId = model.WarehouseId,
                    Notes = "sales invoice transaction no {invoice no}",
                    TransactionType = (byte)WarehouseTransactionEnum.Subtract,
                    WarehouseTransactionDetails = model.Details.Select(e => new WarehouseTransactionDetail
                    {
                        ProductId = e.ProductId,
                        Qty = e.Qty,
                    }).ToList(),
                };

                foreach (var item in model.Details)
                {
                    var product = uow.Products.GetById(item.ProductId);//.Qty;
                        product.Qty += item.Qty;
                }
                foreach (var item in model.MedicalLensDetail)
                {
                    //Get CYL column name 
                    var cylColumnName = uow.CYL.Get(a => a.Id == item.CYLValue).Select(a => a.Value).FirstOrDefault();

                    var existedQty = uow.MedicalLensDetails.GetMedicalLensQty(item.MedicalLensMasterId.Value, item.SPHId.Value, cylColumnName);
                    //TODO Add existedQty + item.Qty

                }

                //save warehouse transaction
                uow.WarehouseTransactions.AddOrUpdate(warehouseTrans, e => e.Id == model.Id);

                //save sales transaction
                uow.SalesTransactions.AddOrUpdate(entity, e => e.Id == model.Id);

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
            var documentNo = uow.SalesTransactions.GetDocumentNumber();
            return Json(documentNo);
        }

        public JsonResult FillProducts(long transactionId)
        {
            var transaction = uow.SalesTransactions.Get(e=>e.Id== transactionId,null,e=>e.SalesTransactionDetails, e => e.MedicalLensSalesDetails)
                .FirstOrDefault();
            return Json(new SalesTransactionModel(transaction, CultureHelper.CurrentLang));
        }

        public JsonResult GetCustomerData(long customerId)
        {
            var customer = uow.Customers.GetById(customerId);
            var customerInvoices = uow.SalesTransactions.GetCustomerInvoices(customerId);

            return Json(new CustomerInvoiceModel
            {
                Code = customer.Code,
                Email = customer.Email,
                Mobile = customer.Mobile,
                Invoices= customerInvoices
            });
        }

        private void ValidateModel(SalesTransactionModel model)
        {
            BusinessException bex = new BusinessException();

            if (!model.Date.HasValue)
                bex.AddRequiredMessage(AppResource.Date);

            if (string.IsNullOrEmpty(model.DocNo))
                bex.AddRequiredMessage(AppResource.DocNo);

            if (!model.WarehouseId.HasValue)
                bex.AddRequiredMessage(AppResource.Warehouse);

            if (model.Details.Count == 0)
                bex.AddRequiredMessage(AppResource.ProductsData);

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
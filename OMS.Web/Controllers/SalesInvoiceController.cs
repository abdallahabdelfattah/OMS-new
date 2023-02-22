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
    public class SalesInvoiceController : BaseController
    {
        // GET: SalesInvoice
        public ActionResult Index()
        {
            FillData();
            return View();
        }

        private void FillData()
        {
            ViewBag.Warehouses = uow.Warehouses.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            ViewBag.Customers = uow.Customers.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            ViewBag.SPHs = uow.SPH.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = s.Name }).ToList();
            ViewBag.CYLs = uow.CYL.GetCYLLookUpModel();
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



                //add warehouse subtract and update product stock - إذن الصرف
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
                foreach (var item in model.MedicalLensDetail)
                {
                    if (warehouseTrans.WarehouseTransactionDetails == null)
                        warehouseTrans.WarehouseTransactionDetails = new List<WarehouseTransactionDetail>();
                    warehouseTrans.WarehouseTransactionDetails.Add(new WarehouseTransactionDetail
                    {
                        MedicalLensIMasterd = item.MedicalLensMasterId,
                        Qty = item.Qty
                    });
                }
                foreach (var item in model.Details)
                {
                    var product = uow.Products.GetById(item.ProductId);//.Qty;
                    if (product.Qty > item.Qty)
                        product.Qty -= item.Qty;
                    else
                    {
                        BusinessException bex = new BusinessException();
                        bex.AddNotValid(AppResource.Qty);
                        throw bex;
                    }
                }
                foreach (var item in model.MedicalLensDetail)
                {
                    //Get CYL column name 
                    var cylColumnName = uow.CYL.Get(a => a.Id == item.CYLValue).Select(a => a.Value).FirstOrDefault();

                    var existedQty = uow.MedicalLensDetails.GetMedicalLensQty(item.MedicalLensMasterId.Value, item.SPHId.Value, cylColumnName);
                    if (existedQty > item.Qty)
                    {
                        var newQty = item.Qty - existedQty;
                        uow.MedicalLensDetails.UpdateMedicalLensQty(item.MedicalLensMasterId.Value, item.SPHId.Value, cylColumnName, newQty);
                        // fill  medicalSalesDetails
                        var medicalSalesDetails = new MedicalLensSalesDetail
                        {
                            
                        };
                        item.FillEntity(medicalSalesDetails);
                        medicalSalesDetails.SalesTransactionMasterID = entity.Id;
                        uow.MedicalLensSalesDetails.Add(medicalSalesDetails);

                    }
                    else
                    {
                        BusinessException bex = new BusinessException();
                        bex.AddQtyNotExists("Medical Lens");
                        throw bex;                        //
                    }

                }
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

        public JsonResult FillProducts(long warehouseId)
        {
            var products = uow.WarehouseProductsViews.GetByWarehouse(warehouseId, CultureHelper.CurrentLang);
            var MedicalLenses = uow.MedicalLensMaster.Get().Select(c => new LookupModel() { Id = c.Id, Name = c.Name })
                .ToList(); ;
            return Json(new { products = products, MedicalLenses = MedicalLenses });
        }

        public JsonResult GetCustomerData(long customerId)
        {
            var customer = uow.Customers.GetById(customerId);

            return Json(new CustomerSummaryModel
            {
                Code = customer?.Code,
                Email = customer?.Email,
                Mobile = customer?.Mobile
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
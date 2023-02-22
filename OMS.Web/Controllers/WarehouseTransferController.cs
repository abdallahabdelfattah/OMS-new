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
    public class WarehouseTransferController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.SourWarehouses = uow.Warehouses.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            return View();
        }
        public JsonResult Save(WarehouseTransferModel model)
        {
            var entity = uow.WarehouseTransactions.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new WarehouseTransaction();
                }
                model.FillEntity(entity);

                //add warehouse subtraction operation
                entity.TransactionType = (byte)WarehouseTransactionEnum.Subtract;
                entity.Status = 1;
                entity.Notes = $"subtraction operation in transfer products from warehouse 1 to warehouse 2 with operation";
                //update product stock in products tabel
                //foreach (var item in model.Details)
                //{
                //    uow.MedicalLens.GetById(item.ProductId).Qty -= item.Qty;
                //}

                //save transaction
                uow.WarehouseTransactions.AddOrUpdate(entity, e => e.Id == model.Id);


                var destWarehouse = new WarehouseTransaction();
                model.FillEntity(destWarehouse);

                //add warehouse adding operation
                destWarehouse.TransactionType = (byte)WarehouseTransactionEnum.Add;
                destWarehouse.WarehouseId = model.DestWarehouseId;
                destWarehouse.Status = 1;
                destWarehouse.Notes = $"add operation in transfer products from warehouse 1 to warehouse 2 with operation";
                //update product stock in products tabel
                foreach (var item in model.Details)
                {
                    uow.Products.GetById(item.ProductId).Qty += item.Qty;
                }

                //save transaction
                uow.WarehouseTransactions.AddOrUpdate(destWarehouse, e => e.Id == model.Id);

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
            var documentNo = uow.WarehouseTransactions.GetDocumentNumber();
            return Json(documentNo);
        }

        private void ValidateModel(WarehouseTransferModel model)
        {
            BusinessException bex = new BusinessException();

            if (!model.Date.HasValue)
                bex.AddRequiredMessage(AppResource.Date);

            if (string.IsNullOrEmpty(model.DocNo))
                bex.AddRequiredMessage(AppResource.DocNo);

            if (!model.WarehouseId.HasValue)
                bex.AddRequiredMessage(AppResource.Warehouse);

            if (!model.DestWarehouseId.HasValue)
                bex.AddRequiredMessage(AppResource.Warehouse);

            if (model.Details.Count == 0)
                bex.AddRequiredMessage(AppResource.ProductsData);

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
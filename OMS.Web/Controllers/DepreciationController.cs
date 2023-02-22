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
    public class DepreciationController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Warehouses = uow.Warehouses.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            return View();
        }
        public JsonResult Save(WarehouseTransactionModel model)
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
                entity.TransactionType = (byte)WarehouseTransactionEnum.Dispose;
                entity.Status = 1;

                //update product stock in products tabel
                //foreach (var item in model.Details)
                //{
                //    uow.MedicalLens.GetById(item.ProductId).Qty -= item.Qty;
                //}

                //save transaction
                uow.WarehouseTransactions.AddOrUpdate(entity, e => e.Id == model.Id);

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

        public JsonResult FillProducts(long warehouseId)
        {
            var products = uow.WarehouseProductsViews.GetByWarehouse(warehouseId, CultureHelper.CurrentLang);
            return Json(products);
        }

        private void ValidateModel(WarehouseTransactionModel model)
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
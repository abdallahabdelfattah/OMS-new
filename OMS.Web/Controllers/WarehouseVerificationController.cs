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
    public class WarehouseVerificationController : BaseController
    {
        // GET: WarehouseVerification
        public ActionResult Index()
        {
            ViewBag.Warehouses = uow.Warehouses.Get()
                .Select(s => new LookupModel() { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();

            return View();
        }

        public JsonResult Details(WarehouseVerificationSearchViewModel vm)
        {
            var result = uow.WarehouseVerifications.Get(d => d.Id == vm.Id, null, d => d.Warehouse);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new WarehouseVerificationModel(p, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });
        }

        public JsonResult List(WarehouseVerificationSearchViewModel vm)
        {
            var result = uow.WarehouseVerifications.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new WarehouseVerificationSummaryModel(p, CultureHelper.IsArabic ? "ar" : "en")).ToList()
            });
        }

        public JsonResult Save(WarehouseVerificationModel model)
        {
            var entity = uow.WarehouseVerifications.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new WarehouseVerification();
                }
                model.FillEntity(entity);
                entity.IsSettled = false;

                //save transaction
                uow.WarehouseVerifications.AddOrUpdate(entity, e => e.Id == model.Id);

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

        public JsonResult Delete(long Id)
        {
            try
            {
                uow.WarehouseVerifications.DeleteById(Id);
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
            var documentNo = uow.WarehouseVerifications.GetDocumentNumber();
            return Json(documentNo);
        }

        public JsonResult FillDetails(long WarehouseId)
        {
            var products = uow.WarehouseProductsViews.Get(e => e.WarehouseID == WarehouseId).ToList();
            var details = products.Select(e=> new WarehouseVerificationDetailsModel
            {
                ActualQty = e.total,
                TransQty = e.total,
                ProductId = e.ProductID,
                ProductName = CultureHelper.IsArabic ? e.ProductAr : e.ProductEn,
                UnitOfficialPrice = GetProductPrice(e.ProductID)
            }).ToList();
            
            return Json(details);
        }

        private decimal GetProductPrice(long ProductId)
        {
            return uow.Products.GetById(ProductId)?.OfficialPrice ?? 0;
        }

        private void ValidateModel(WarehouseVerificationModel model)
        {
            BusinessException bex = new BusinessException();

            if (!string.IsNullOrEmpty(model.StrDate))
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
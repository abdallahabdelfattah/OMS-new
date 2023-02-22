//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class MedicalLensMasterController : BaseController
    {
        public ActionResult Index()
        {
            fillViewBags();
            return View();
        }
        public JsonResult List(MedicalLensMasterSearchViewModel vm)
        {
            var result = uow.MedicalLensMaster.Search(vm);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new MedicalLensMasterSummaryModel(p)).ToList()
            });
        }

        public JsonResult Details(MedicalLensMasterSearchViewModel vm)
        {
            var result = uow.MedicalLensMaster.Get(d => d.Id == vm.Id, null);
            return Json(new
            {
                TotalCount = result.Count(),
                Data = result.Select(p => new MedicalLensMasterViewModel(p)).ToList()
            });
        }
        
        public JsonResult Save(MedicalLensMasterViewModel model)
        {
            var entity = uow.MedicalLensMaster.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new MedicalLensMaster();
                }
                model.FillEntity(entity);

                uow.MedicalLensMaster.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.MedicalLensMaster.DeleteById(ID);
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
            var documentNo = uow.MedicalLensMaster.GetDocumentNumber();
            return Json(documentNo);
        }

        private void ValidateModel(MedicalLensMasterViewModel model)
        {
            BusinessException bex = new BusinessException();

        }

        private void fillViewBags()
        {
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

            ViewBag.CoatingDiagrams = uow.CoatingDiagram.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            ViewBag.LenseIndexs = uow.LenseIndex.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Index.ToString() }).ToList();

            ViewBag.MultifocalDesigns = uow.MultifocalDesign.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            ViewBag.VersionTypes = uow.VersionType.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();
            
            ViewBag.CoatingDiagrams = uow.CoatingDiagram.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            ViewBag.SPHs = uow.SPH.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();

            ViewBag.CYLs = uow.CYL.Get().Select(s => new LookupModel()
            { Id = s.Id, Name = s.Name }).ToList();
        }
    }
}
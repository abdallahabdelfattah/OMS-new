using OMS.Model.ViewModel;
using OMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.Web.Globalization;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel.SC;

namespace OMS.Web.Controllers
{
    public class CoatingDiagramController : BaseController
    {
        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(CoatingDiagramSearchViewModel sc)
        {
            var result = uow.CoatingDiagram.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new CoatingDiagramModel(e)).ToList() });
        }

        public JsonResult Details(CoatingDiagramSearchViewModel sc)
        {
            var result = uow.CoatingDiagram.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new CoatingDiagramModel(e)).ToList() });
        }

        public JsonResult Save(CoatingDiagramModel model)
        {
            var entity = uow.CoatingDiagram.GetById(model.Id);

            try
            {
                ValidateModel(model);
                if (entity == null)
                {
                    entity = new CoatingDiagram();
                }
                model.FillEntity(entity);
                
                uow.CoatingDiagram.AddOrUpdate(entity,e=>e.Id == model.Id);
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
                uow.CoatingDiagram.DeleteById(Id);
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

        private void ValidateModel(CoatingDiagramModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.Name))
                bex.AddRequiredMessage("Name");
            else if (uow.CoatingDiagram.GetFirst(a => a.Name.Equals(model.Name) && a.Id != model.Id) != null)
                bex.AddExistsMessage("Name");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }
    }
}
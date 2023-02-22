using OMS.Model.ViewModel;
using OMS.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OMS.BLL.Utilities;
using OMS.Model.ViewModel.SC;
using OMS.DAL.DataAccess;

namespace OMS.Web.Controllers
{
    public class MultifocalDesignController : BaseController
    {
        // GET: MultifocalDesign
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(MultifocalDesignSearchViewModel sc)
        {
            var result = uow.MultifocalDesign.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new MultifocalDesignModel(e)).ToList() });
        }

        public JsonResult Details(MultifocalDesignSearchViewModel sc)
        {
            var result = uow.MultifocalDesign.Search(sc);
            return Json(new { TotalCount = result.TotalItemCount, Data = result.Select(e => new MultifocalDesignModel(e)).ToList() });
        }

        public JsonResult Save(MultifocalDesignModel model)
        {
            var entity = uow.MultifocalDesign.GetById(model.Id);

            try
            {
                ValidateModel(model);

                if (entity == null)
                {
                    entity = new MultifocalDesign();
                }

                model.FillEntity(entity);

                uow.MultifocalDesign.AddOrUpdate(entity, e => e.Id == model.Id);
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
                uow.MultifocalDesign.DeleteById(Id);
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

        private void ValidateModel(MultifocalDesignModel model)
        {
            BusinessException bex = new BusinessException();

            if (string.IsNullOrEmpty(model.Name))
                bex.AddRequiredMessage("Name");
            else if (uow.MultifocalDesign.GetFirst(a => a.Name.Equals(model.Name) && a.Id != model.Id) != null)
                bex.AddExistsMessage("Name");

            if (bex.Exceptions.Count > 0)
                throw bex;
        }

    }
}
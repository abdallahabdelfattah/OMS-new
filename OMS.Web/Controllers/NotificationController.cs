using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OMS.Web.Controllers
{
    public class NotificationController : BaseController
    {
        public ActionResult Index()
        {
            using (GroupBL bl = new GroupBL())
            {
                List<Group> Groups = bl.Search(new GroupSC()).Result;
                ViewBag.Groups = Groups.Select(s => new LookupModel() { ID = s.ID, Name = s.Name }).ToList();
            }
            return View();
        }
        public JsonResult List(NotificationSC sc)
        {
            using (NotificationBL bl = new NotificationBL())
            {
                if (SessionRegistery.CurrentUser.GroupID != 1)
                {
                    sc.GroupID = SessionRegistery.CurrentUser.GroupID;
                }

                var result = bl.Search(sc);
                return Json(new { TotalCount = result.TotalCount, Data = result.Result.Select(p => new NotificationModel(p)).ToList() });
            }
        }
        public JsonResult Save(NotificationModel model)
        {
            using (NotificationBL bl = new NotificationBL())
            {
                //if (!model.IsAdmin)
                //    throw new Exception("Only admin can add or edit notification");

                var entity = model.ID > 0 ? bl.Find(model.ID) : new Notification();
                if (entity == null)
                {
                    entity = new Notification();
                }
                model.FillEntity(entity);

                try
                {
                    bl.Save(entity);

                    return Json(new { Model = new NotificationModel(bl.Find(entity.ID)), Exceptions = new List<string>() });
                }
                catch (BL.BusinessException bexp)
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
        }

        public JsonResult Details(NotificationSC sc)
        {
            using (NotificationBL bl = new NotificationBL())
            {
                var result = bl.Search(sc);

                //update notification as seen
                var entity = result.Result.FirstOrDefault();
                if (!entity.NotificationSeens.Any(n => n.SeenByID == SessionRegistery.CurrentUser.ID))
                {
                    entity.NotificationSeens.Add(new NotificationSeen { SeenByID = SessionRegistery.CurrentUser.ID, SeenDate = DateTime.Now });
                    bl.SaveChanges();
                }

                return Json(new { TotalCount = result.TotalCount, Data = result.Result.Select(p => new NotificationModel(p)).ToList() });
            }
        }
        public JsonResult Delete(long ID)
        {
            using (NotificationBL bl = new NotificationBL())
            {
                try
                {
                    var entity = bl.Find(ID);
                    entity.IsDeleted = true;
                    bl.SaveChanges();

                    return Json(new { Exceptions = new List<string>() });
                }
                catch (BL.BusinessException bexp)
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
        }
        public JsonResult AddResponse(string NotiResponse, long NotiID)
        {
            using (NotificationBL bl = new NotificationBL())
            {
                try
                {
                    NotificationResponse res = new NotificationResponse
                    {
                        Date = DateTime.Now,
                        NotificationID = NotiID,
                        ResponseByID = SessionRegistery.CurrentUser.ID,
                        Description = NotiResponse
                    };
                    bl.AddResponse(res);

                    var result = bl.Search(new NotificationSC { ID = NotiID });
                    return Json(new { Exceptions = new List<string>(), Data = result.Result.Select(p => new NotificationModel(p)).ToList() });
                    //return Json(new { Exceptions = new List<string>() });
                }
                catch (BL.BusinessException bexp)
                {
                    return Json(new { Exceptions = bexp.Exceptions });
                }
                catch (Exception ex)
                {
                    return Json(new { Exceptions = new List<string> { ex.Message } });
                }
            }
        }
    }
}
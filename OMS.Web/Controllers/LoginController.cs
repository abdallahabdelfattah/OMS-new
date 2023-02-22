using OMS.BLL;
using OMS.Model.SystemEnums;
using OMS.Web.Utilities;
using OMS.Model.ViewModel;
using System.Web.Mvc;
using System.Linq;
using OMS.Web.Globalization;

namespace OMS.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string Logout = null)
        {
            if (Logout == "true")
            {
                SessionRegistery.CurrentUser = null;
            }
            using (OMSUoW uow = new OMSUoW())
            {
                ViewBag.Branches = uow.Branches.Get().Select(s => new LookupModel()
                { Id = s.Id, Name = CultureHelper.IsArabic ? s.NameAr : s.NameEn }).ToList();
            }

            return View();
        }

        public ActionResult SignIn(string username, string password, long branchId=0)
        {
            using (OMSUoW uow = new OMSUoW())
            {
                var user = uow.Users.GetFirst(u=>u.UserName == username && u.Password == password);
                if (user != null)
                {
                    if (user.Status == (int)UserStatus.Active)
                    {
                        SessionRegistery.CurrentUser = new UserSummaryModel(user);
                        SessionRegistery.CurrentUser.BranchId = branchId;

                        return Json(new { Result = "OK" });
                    }
                    else
                    {
                        return Json(new { Result = "NotActive" });
                    }
                }
                else
                {
                    return Json(new { Result = "Error" });
                }
            }
        }

        //public JsonResult forgetPassword(string email)
        //{
        //    using (var cbl = new UserBL())
        //    {
        //        try
        //        {
        //            cbl.ForgetPassword(email);
        //            return Json(new { Result = "ok" });
        //        }
        //        catch (BL.BusinessException bexp)
        //        {
        //            return Json(new { Exceptions = bexp.Exceptions });
        //        }
        //        catch (Exception ex)
        //        {
        //            List<string> lstExceptions = new List<string>();
        //            lstExceptions.Add(ex.Message);
        //            return Json(new { Exceptions = lstExceptions });
        //        }
        //    }
        //}
    }
}
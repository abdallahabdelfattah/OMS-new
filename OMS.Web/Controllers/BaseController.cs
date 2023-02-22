using OMS.BLL;
using OMS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using OMS.Web.Utilities;
using OMS.Web.Globalization;
using OMS.BLL.Logging;
using OMS.DAL.DataAccess;
using System.IO;

namespace OMS.Web.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class BaseController : Controller
    {
        /// <summary>
        ///     The unit of work.
        /// </summary>
        public OMSUoW uow;
        protected Logger Logger { get; }

        public bool _existURL = false;
        public string[] _customFormats = { "dd/MM/yyyy", "yyyy-MM-dd" };

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        public BaseController()
        {
            //this.Cache = new MemoryCacheManager();
            this.uow = new OMSUoW();
            var baseType = this.GetType().BaseType ?? this.GetType();
            this.Logger = new Logger(baseType);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName.ToLower() == "index")
            {
                GetUserPrivilages();
                if (_existURL == false)
                {
                    filterContext.Result = new RedirectResult("/accessDenied");

                }
            }



            base.OnActionExecuted(filterContext);
        }
        
        private void GetUserPrivilages()
        {
            var Menu = uow.SystemPages.GetUserMenu(SessionRegistery.CurrentUser.Id, CultureHelper.IsArabic);
            ViewBag.UserPages = Menu;


            string _currentController = RouteData.Values["Controller"].ToString();
            string _currentUR = System.Web.HttpContext.Current.Request.RawUrl;
            
            if (string.Compare(_currentController, "Notification", true) != 0 &&
                string.Compare(_currentController, "Home", true) != 0 &&
                string.Compare(_currentController, "AccessDenied", true) != 0 &&
                string.Compare(_currentController, "Profile", true) != 0)
            {
                foreach (var item in Menu)
                {
                    bool found = CheckMenuItem(item, _currentUR);
                    if (found)
                    {
                        _existURL = true;
                    }

                }
            }
            else
                _existURL = true;
        }
        
        private bool CheckMenuItem(OMS.BLL.Repositories.MenuModel item, string currentURL)
        {
            if (currentURL.ToLower().Contains("import") && item.URL != null)
            {
                if (item.URL.ToLower().Contains("import"))
                    return true;
            }
            if (string.Compare(item.URL, currentURL.Split('?')[0], true) == 0 || string.Compare("/" + item.URL, currentURL.Split('?')[0], true) == 0)
            {
                return true;
            }
            if (string.Compare(item.URL, currentURL, true) == 0 || string.Compare("/" + item.URL, currentURL, true) == 0)
            {
                return true;
            }
            else
            {
                foreach (var child in item.Children)
                {
                    bool found = CheckMenuItem(child, currentURL);
                    if (found)
                        return true;

                }
            }
            return false;
        }

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (SessionRegistery.CurrentUser == null)
            {
                filterContext.Result = new RedirectResult("/login");
            }
            base.OnAuthentication(filterContext);
        }

        //todo: upload control
        [HttpPost]
        public JsonResult UploadFile(string ImageFolder)
        {
            List<LookupModel> uploadids = new List<LookupModel>();
            string outFileName = string.Empty;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var hpf = Request.Files[i];

                Upload upload = new Upload();
                if (hpf.ContentLength == 0)
                    continue;

                string FileType = Path.GetExtension(hpf.FileName);
                List<string> ExtensionList = new List<string> { ".png", ".jpg", ".jpeg", ".gif" };
                if (ExtensionList.Contains(FileType.ToLower()))
                {
                    outFileName = Guid.NewGuid().ToString().Replace("-", "") + FileType;
                    var UploadFilePath = Path.Combine(Server.MapPath(@"~/Uploads/" + ImageFolder), outFileName);

                    hpf.SaveAs(UploadFilePath);

                    upload.FileName = outFileName;
                    uow.Uploads.Add(upload);
                    uow.Save();
                    uploadids.Add(new LookupModel { Id = upload.Id, Name = outFileName });
                }
            }
            return Json(uploadids);
        }

        public string GetImageURL(long? imageid)
        {
            if (!imageid.HasValue || imageid == 0) return "/Uploads/not-available.jpg";
            if (imageid == 0) return "/Uploads/not-available.jpg";

            string path =
                    $"/Uploads/{uow.Uploads.GetFirst(u => u.Id == imageid)?.FileName}";

            return path;
        }

        public ActionResult Logout()
        {
            SessionRegistery.CurrentUser = null;

            return RedirectToAction("Index", "Login");
        }
        public ActionResult ChangeCulture()
        {
            var globalization = new GlobalizationModule();
            globalization.changeCulture();
            string url = Request.UrlReferrer.AbsoluteUri.ToString();  ; 
            return  Redirect(url);
        }

        //public JsonResult NotificationList()
        //{
        //    using (NotificationBL bl = new NotificationBL())
        //    {
        //        DateTime dt = DateTime.Now.Date;
        //        var NotiList = bl.GetUserNotifications(SessionRegistery.CurrentUser.ID, SessionRegistery.CurrentUser.GroupID ?? 0);

        //        return Json(new
        //        {
        //            TotalCount = NotiList.Count,
        //            Data = NotiList.Select(n => new
        //            {
        //                n.ID,
        //                n.Title,
        //                n.Type,
        //                n.Description
        //            })       vv 
        //        });
        //    }
        //}

        /// <summary>
        /// The on exception.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        protected override void OnException(ExceptionContext filterContext)
                {
            if (filterContext.ExceptionHandled)
            {
                this.Logger.Error("Exception Message: " + filterContext.Exception.Message + "\n\r");
                this.Logger.Error("InnerException Message: " + filterContext.Exception.InnerException.Message + "\n\r");
                return;
            }

            this.Logger.Error(filterContext.Exception);

            // in case of HttpAntiForgeryException response JS that will open dialog to user telling him that because of
            // security reasons this page have to be reloaded and when he closes this the page reloads!
            // Below fix i a SH*TTY workaround that you MUST NOT EVER THINK OF.
            if (filterContext.Exception is HttpAntiForgeryException)
            {
                // Redirect
                filterContext.Result = this.RedirectToAction("Index", "Home");
                filterContext.ExceptionHandled = true;
                base.OnException(filterContext);
                return;
            }

            if (this.Request.IsAjaxRequest())
            {
                //filterContext.Result = new JsonDotNetResult(
                //    new
                //    {
                //        Success = false,
                //        ErrorMessage = filterContext.Exception.Message,
                //        ExceptionMessage = filterContext.Exception.Message,
                //        ExceptionDetails = filterContext.Exception.ToString()
                //    });
            }
            else
            {
                //var fullErrorPageMessage =
                //    new FullPageMessage
                //    {
                //        Message = filterContext.Exception.Message,
                //        Icon = MessageIcon.Error,
                //        BackLinkText = SharedResources.BackToHomePage,
                //        PageTitle = SharedResources.ErrorOccured,
                //        BackLinkUrl = "/",
                //        Description = string.Empty,
                //        HandleErrorInfo =
                //                new HandleErrorInfo(filterContext.Exception, "Home", "Index")
                //    };
                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "_FullPageMessage",
                //    ViewData = new ViewDataDictionary(fullErrorPageMessage)
                //};
            }
            filterContext.ExceptionHandled = true;
            base.OnException(filterContext);
        }

    }
}
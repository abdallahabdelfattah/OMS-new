using OMS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Web.Utilities
{
    public class SessionRegistery
    {
        public static UserSummaryModel CurrentUser
        {
            get
            {
                try
                {
                    return (UserSummaryModel)HttpContext.Current.Session["CurrentUser"];
                }
                catch (Exception ex)
                {
                    return new UserSummaryModel();
                }
                //todo:fix it osama
                //return (UserSummaryModel)HttpContext.Current.Session["CurrentUser"];
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }

    }
}
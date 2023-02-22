using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OMS.BLL
{
    public static class LookupHelper
    {
        public static List<SelectListItem> SupllierType(bool IsArabic)
        {
            return new List<SelectListItem> {
            new SelectListItem
            {
              Text = IsArabic ? "مورد خارجى" : "External Supplier",
              Value = "1"
            },
            new SelectListItem
            {
               Text = IsArabic ? "مورد داخلى" : "Internal Supplier",
               Value = "0"
            }};
        }

        public static List<SelectListItem> SphList()
        {
            using (var uow = new OMSUoW())
            {
                return uow.SPH.GetNames()
                    .Select(o => new SelectListItem { Text = o.Value, Value = o.Key.ToString() }).ToList();
            }
        }
        public static List<SelectListItem> cylList()
        {
            using (var uow = new OMSUoW())
            {
                return uow.CYL.GetNames()
                    .Select(o => new SelectListItem { Text = o.Value, Value = o.Key.ToString() }).ToList();
            }
        }
    }
}

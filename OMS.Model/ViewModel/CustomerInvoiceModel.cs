using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class CustomerInvoiceModel : BaseModel
    {
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public List<LookupModel> Invoices { get; set; }
    }
}
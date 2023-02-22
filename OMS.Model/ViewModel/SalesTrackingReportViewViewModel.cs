using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class SalesTrackingReportViewSummeryModel : BaseModel
    {
        public SalesTrackingReportViewSummeryModel()
        {

        }
        public SalesTrackingReportViewSummeryModel(SalesTrackingReportView entity)
        {
            

            SalesAgentName =  entity.SalesAgentName;  
            WarehouseName =entity.WarehouserName ;
            CustomerName = entity.CustomerName;
            CreatedOn = entity.CreatedOn;
            DocNo = entity.DocNo;
            InvoiceAmount = entity.InvoiceAmount;
            DiscountAmount = entity.DiscountAmount;
            CostAmount = entity.CostAmount;
            PaidAmount = entity.PaidAmount;
            ReturnAmount = entity.ReturnAmount;
        }

        public string SalesAgentName { get; set; }
        public string WarehouseName { get; set; }
        public string CustomerName { get; set; }
        public string DocNo { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? CostAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? ReturnAmount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long WarehouseId { get; set; }

    }
    public class SalesTrackingReportViewViewModel : SalesTrackingReportViewSummeryModel
    {

        public SalesTrackingReportViewViewModel() { }
        public SalesTrackingReportViewViewModel(SalesTrackingReportView entity) : base(entity)
        { }
    }
}

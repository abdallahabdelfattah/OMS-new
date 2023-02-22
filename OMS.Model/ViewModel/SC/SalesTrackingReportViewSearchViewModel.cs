using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OMS.Model.ViewModel
{
    public class SalesTrackingReportViewSearchViewModel : BaseSearchViewModel
    {
        public long? WarehouseId { get; set; }
        public string ProductCode { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? FromDate { get; set; }
    }
}

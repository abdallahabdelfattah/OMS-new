using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class SalesTransactionSearchViewModel : BaseSearchViewModel
    {
        public string CustomerName { get; set; }
        public long? CustomerId { get; set; }
        public string DocNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SalesAgentName { get; set; }
        public bool? IsDelivered { get; set; }
        public byte? Type { get; set; }
    }
}

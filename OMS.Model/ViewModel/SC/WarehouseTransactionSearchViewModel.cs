using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class WarehouseTransactionSearchViewModel : BaseSearchViewModel
    {
        public string DocNo { get; set; }
        public string EmployeeName { get; set; }
        public long? WarehouseId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public byte? TransactionType { get; set; }
    }
}

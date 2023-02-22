using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class PurchaseTransactionMasterSearchViewModel : BaseSearchViewModel
    {
        public PurchaseTransactionMasterSearchViewModel()
        {
        }

        public PurchaseTransactionMasterSearchViewModel(PurchaseTransactionMaster entity, string language)
        {
            Id = entity.Id;
            InvoiceNo = entity.InvoiceNo;
            SupplierId = entity.SupplierId.ToString();
            SupplierInvoiceDate = entity.SupplierInvoiceDate;
            SupplierInvoiceNo = entity.SupplierInvoiceNo;
            FeesAmount = entity.FeesAmount;
            InvoiceDate = entity.InvoiceDate;
            if (entity.InvoiceAmount != null) InvoiceAmount = entity.InvoiceAmount.Value;
            InvoiceNo = entity.InvoiceNo;
            Notes = entity.Notes;
            OtherFees = entity.OtherFees;
            PurchaseAgentName = entity.PurchaseAgentName;
            Details = new List<PurchaseTransactionDetailsSummeryModel>();
        }
        public DateTime? SupplierInvoiceDate { get; set; }

        public string PurchaseAgentName { get; set; }

        public decimal? OtherFees { get; set; }

        public string Notes { get; set; }
        public decimal InvoiceAmount { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public decimal? FeesAmount { get; set; }

        public string SupplierInvoiceNo { get; set; }

        public string InvoiceNo { get; set; }
        public decimal? TaxFees { get; set; }
        public bool? Status { get; set; }
        public string SupplierId { get; set; }
        public long? WarehouseId { get; set; }
        public List<PurchaseTransactionDetailsSummeryModel> Details { get; set; }
    }
    public class PurchaseTransactionDetailsSearchViewModel : BaseSearchViewModel
    {
        public long ProductId { get; set; }
        public long? PurchaseTransactionMasterId { get; set; }
    }
}

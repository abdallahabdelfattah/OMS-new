using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class PurchaseTransactionMasterSummaryModel : BaseModel
    {
        public PurchaseTransactionMasterSummaryModel()
        { }
        public PurchaseTransactionMasterSummaryModel(PurchaseTransactionMaster entity)
        {
            Id = entity.Id;
            InvoiceNo = entity.InvoiceNo;
            SupplierInvoiceDate = entity.SupplierInvoiceDate;
            SupplierInvoiceNo = entity.SupplierInvoiceNo;
            FeesAmount = entity.FeesAmount;
            InvoiceDate = entity.InvoiceDate;
            if (entity.InvoiceAmount != null) InvoiceAmount = entity.InvoiceAmount.Value;
            InvoiceNo = entity.InvoiceNo;
            Notes = entity.Notes;
            SupplierName = entity?.Supplier?.NameEn;
            WarehouseName = entity?.Warehouse?.NameEn;
            TaxFees = entity.TaxFees;
            OtherFees = entity.OtherFees;
            PurchaseAgentName = entity.PurchaseAgentName;
        }
        [Required]
        public DateTime? SupplierInvoiceDate { get; set; }

        public string PurchaseAgentName { get; set; }

        public decimal? OtherFees { get; set; }

        public string Notes { get; set; }
        [Required]
        public decimal InvoiceAmount { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public decimal? FeesAmount { get; set; }

        public string SupplierInvoiceNo { get; set; }
        public string WarehouseName { get; set; }
        public string SupplierName { get; set; }
        public string InvoiceNo { get; set; }
        public decimal? TaxFees { get; set; }
        public bool? Status { get; set; }
    }
    public class PurchaseTransactionMasterViewModel : PurchaseTransactionMasterSummaryModel
    {
        public PurchaseTransactionMasterViewModel() : base()
        {
            if (Details == null)
                Details = new List<PurchaseTransactionDetailsSummeryModel>();
        }
        public PurchaseTransactionMasterViewModel(PurchaseTransactionMaster entity) : base(entity)
        {
            SupplierId = entity?.Supplier?.Id;
            WarehouseId = entity?.Warehouse?.Id;
            Details = entity.PurchaseTransactionDetails.Select(p => new PurchaseTransactionDetailsSummeryModel(p)).ToList();
        }



        public long? SupplierId { get; set; }
        public long? WarehouseId { get; set; }
        public long SelectedProductId { get; set; }
        public long SelectedBrandId { get; set; }
        public List<PurchaseTransactionDetailsSummeryModel> Details { get; set; }
    }
}

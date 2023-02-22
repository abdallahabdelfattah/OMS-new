using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class SalesInvoiceModel : BaseModel
    {
        public SalesInvoiceModel()
        {
            Details = new List<SalesTransactionDetailsModel>();
            MedicalLensDetail = new List<MedicalLensSalesDetailModel>();
        }
        public SalesInvoiceModel(SalesTransaction entity, string lang)
        {
            Id = entity.Id;
            Date = entity.Date;
            DeliveryDate = entity.DeliveryDate;
            DocNo = entity.DocNo;
            SalesAgentName = entity.SalesAgentName;
            CostAmount = entity.CostAmount ?? 0;
            PaidAmount = entity.PaidAmount ?? 0;
            WarehouserName = entity.WarehouserName;
            WarehouseId = entity.WarehouseId;
            MedicalLensMasterId = entity.MedicalLensSalesDetails.FirstOrDefault()?.MedicalLensMasterId;
            VATAmount = entity.VATValue ?? 0;
            Discount = entity.Discount ?? 0;
            Notes = entity.Notes;
            Status = 1;
            Type = entity.Type ?? 1;
            CustomerId = entity.CustomerId;
            IsDelivered = entity.DeliveryDate.HasValue;
            Details = entity.SalesTransactionDetails.Select(i => new SalesTransactionDetailsModel(i, lang)).ToList();
            MedicalLensDetail = entity.MedicalLensSalesDetails.Select(i => new MedicalLensSalesDetailModel(i)).ToList();
        }

        public long? MedicalLensMasterId { get; set; }

        public string DocNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SalesAgentName { get; set; }
        public decimal CostAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string WarehouserName { get; set; }
        public long SalesAgentId { get; set; }
        public long? WarehouseId { get; set; }
        public long? BranchId { get; set; }
        public string Notes { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public decimal Discount { get; set; }
        public decimal VATAmount { get; set; }
        public long? CustomerId { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerNo { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsSalesRequest { get; set; }
        public List<SalesTransactionDetailsModel> Details { get; set; }
        public List<MedicalLensSalesDetailModel> MedicalLensDetail { get; set; }
        public List<TransactionPaymentModel> TransactionPayment { get; set; }
        public void FillEntity(SalesTransaction entity)
        {
            entity.Date = Date;
            entity.DocNo = DocNo;
            entity.WarehouseId = WarehouseId;
            entity.Type = Type;
            entity.Notes = Notes;
            entity.CustomerId = CustomerId;
            entity.Discount = Discount;
            entity.SalesTransactionDetails = Details.Select(i => new SalesTransactionDetail()
            {
                ProductId = i.ProductId,
                Qty = i.Qty,
                SalesPrice = i.SalesPrice,
                Discount = i.Discount,
                VATValue = i.VATValue,
            }).ToList();
            entity.MedicalLensSalesDetails = MedicalLensDetail.Select(a =>new MedicalLensSalesDetail
            {
                MedicalLensMasterId = a.MedicalLensMasterId,
                Id = a.Id,
                Qty = a.Qty.ToString(),
                SalesPrice = a.SalesPrice,
                Discount = a.Discount,
                VATValue = a.VATValue,
                SPHId = a.SPHId,
                CYL = a.CYLValue
            }).ToList();
            //invoiceAmount
            decimal InvoiceAmount = 0;
            decimal InvoiceCostingAmount = 0;
            foreach (var item in Details)
            {
                InvoiceAmount += (item.SalesPrice * item.Qty);
            }

            entity.VATValue = VATAmount;
            entity.DiscountAmount = Discount;
            entity.InvoiceAmount = InvoiceAmount - entity.DiscountAmount;
            entity.CostAmount = InvoiceCostingAmount;

            entity.UpdatedOn = DateTime.Now;

            if (entity.Id == 0)
            {
                entity.CreatedOn = DateTime.Now;
            }
        }
    }
    public class TransactionPaymentModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal PaidAmount { get; set; }
        public string CardNo { get; set; }
        public string BankName { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        public string ReciteNo { get; set; }
        public byte PaymentType { get; set; }
    }
}
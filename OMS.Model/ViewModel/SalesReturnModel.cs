using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class SalesReturnModel : BaseModel
    {
        public SalesReturnModel()
        {
            Date = DateTime.Now;
            Details = new List<SalesTransactionDetailsModel>(); 
            MedicalLensDetail = new List<MedicalLensSalesDetailModel>();
        }
        public SalesReturnModel(SalesTransaction entity, string lang)
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
            CustomerId = entity.CustomerId;
            Discount = entity.Discount ?? 0;
            Notes = entity.Notes;
            Status = 1;
            Type = entity.Type ?? 1;
            Details = entity.SalesTransactionDetails.Select(i => new SalesTransactionDetailsModel(i, lang)).ToList();
            MedicalLensDetail = entity.MedicalLensSalesDetails.Select(i => new MedicalLensSalesDetailModel(i)).ToList();
        }

        public string DocNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SalesAgentName { get; set; }
        public decimal CostAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal VATValue { get; set; }
        public string WarehouserName { get; set; }
        public long SalesAgentId { get; set; }
        public long? WarehouseId { get; set; }
        public long? BranchId { get; set; }
        public long? CustomerId { get; set; }
        public long? InvoiceId { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerNo { get; set; }
        public string Notes { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public decimal Discount { get; set; }
        public List<SalesTransactionDetailsModel> Details { get; set; }
        public List<MedicalLensSalesDetailModel> MedicalLensDetail { get; set; }

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
                SalesPrice = i.SalesPrice,
                Qty = i.Qty,
            }).ToList();

            //invoiceAmount
            decimal InvoiceAmount = 0;
            decimal InvoiceCostingAmount = 0;
            foreach (var item in Details)
            {
                InvoiceAmount += (item.SalesPrice * item.Qty);
            }

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
}
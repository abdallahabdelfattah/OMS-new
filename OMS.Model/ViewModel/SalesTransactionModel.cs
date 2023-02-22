using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class SalesTransactionSummaryModel : BaseModel
    {
        public SalesTransactionSummaryModel()
        {
            
        }
        public SalesTransactionSummaryModel(SalesTransaction entity, string lang)
        {
            Id = entity.Id;
            StrDate = entity.Date.HasValue? entity.Date.Value.ToString("dd/MM/yyyy"):"";
            DocNo = entity.DocNo;
            SalesAgentName = entity.SalesAgentName;
            CustomerName = lang =="ar"? entity.Customer?.NameAr : entity.Customer?.NameEn;
            CostAmount = entity.CostAmount ?? 0;
            PaidAmount = entity.PaidAmount ?? 0;
            VATValue = entity.VATValue ?? 0;
            StatusName = entity.Type.ToString();

            switch (entity.Type)
            {
                case (byte)SalesTransactionEnum.SalesRequest:
                    TypeName = lang == "ar" ? "حجز مبيعات" : "Sales Request";
                    break;
                case (byte)SalesTransactionEnum.SalesInvoice:
                    TypeName = lang == "ar" ? "فاتورة بيع" : "Sales Invoice";
                    break;
                case (byte)SalesTransactionEnum.CanceledRequest:
                    TypeName = lang == "ar" ? "حجز تم إلغائه" : "Cancel Sales Request";
                    break;
                case (byte)SalesTransactionEnum.SalesReturn:
                    TypeName = lang == "ar" ? "مرتجع" : "Sales Return";
                    break;
                default:
                    break;
            }
        }

        public string StrDate { get; set; }
        public string DocNo { get; set; }
        public string SalesAgentName { get; set; }
        public string CustomerName { get; set; }
        public string StatusName { get; set; }
        public string TypeName { get; set; }
        public decimal CostAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal VATValue { get; set; }
        

    }
    public class SalesTransactionModel : SalesTransactionSummaryModel
    {
        public SalesTransactionModel():base()
        {
            Details = new List<SalesTransactionDetailsModel>();
            MedicalLensDetail = new List<MedicalLensSalesDetailModel>();
        }
        public SalesTransactionModel(SalesTransaction entity,string lang):base(entity, lang)
        {
            Date= entity.Date;
            DeliveryDate = entity.DeliveryDate;
            WarehouserName = entity.WarehouserName;
            WarehouseId = entity.WarehouseId;
            Discount = entity.Discount ?? 0;
            Notes = entity.Notes;
            Status = 1;
            Type = entity.Type??1;
            CustomerId = entity.CustomerId;
            IsDelivered = entity.DeliveryDate.HasValue;
            Details = entity.SalesTransactionDetails.Select(i => new SalesTransactionDetailsModel(i, lang)).ToList();
            MedicalLensDetail = entity.MedicalLensSalesDetails.Select(i => new MedicalLensSalesDetailModel(i)).ToList();
        }

        public DateTime? Date { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public long? InvoiceId { get; set; }
        public string WarehouserName { get; set; }
        public long SalesAgentId { get; set; }
        public long? WarehouseId { get; set; }
        public string Notes { get; set; }
        public string WarehouseName { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public decimal Discount { get; set; }
        public long? CustomerId { get; set; }
        public bool IsDelivered { get; set; }
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
                ProductId = i.ProductId,
                VATValue = i.VATValue,
                Discount = i.Discount,
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

            //entity.ModifiedBy = SessionRegistery.CurrentUser.ID;
            entity.UpdatedOn = DateTime.Now;

            if (entity.Id == 0)
            {
                //entity.CreatedBy = SessionRegistery.CurrentUser.ID;
                entity.CreatedOn = DateTime.Now;
            }
        }
        
    }

    public class SalesTransactionDetailsModel : BaseModel
    {
        public SalesTransactionDetailsModel()
        { }
        public SalesTransactionDetailsModel(SalesTransactionDetail entity, string lang)
        {
            Id = entity.Id;
            ProductName = lang == "ar" ? entity.Product?.NameAr : entity.Product?.NameEn;
            Qty = entity.Qty ?? 0;
            Discount = entity.Discount ?? 0;
            VATValue = entity.VATValue ?? 0;
            SalesPrice = entity.SalesPrice ?? 0;
        }

        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal VATValue { get; set; }
        public int Qty { get; set; }
        public int ReturnedQty { get; set; }
    }

    public class MedicalLensSalesDetailModel:BaseModel
    {
        public MedicalLensSalesDetailModel()
        { }
        public MedicalLensSalesDetailModel(MedicalLensSalesDetail entity)
        {
            Id = entity.Id;
            Qty = int.Parse(entity.Qty);
            LensName = entity.MedicalLensMaster?.Name;
            Discount = entity.Discount ?? 0;
            VATValue = entity.VATValue ?? 0;
            SalesPrice = entity.SalesPrice ?? 0;
            SPHId = entity.SPHId;
            CYLValue = entity.CYL;
        }

        public int? SPHId { get; set; }
        public int? CYLValue { get; set; }

        public long Id { get; set; }

        public int Qty { get; set; }
        public string LensName { get; set; }

        public double SalesPrice { get; set; }

        public double VATValue { get; set; }

        public double Discount { get; set; }
        public long? MedicalLensMasterId { get; set; }
        public int ReturnedQty { get; set; }

        public void FillEntity(MedicalLensSalesDetail entity)
        {
    
            entity.Qty = Qty.ToString();
            entity.Discount = Discount == null ? (double?)0 : Discount;
            entity.MedicalLensMasterId = MedicalLensMasterId;
            entity.SalesPrice = SalesPrice;
            entity.SPHId = SPHId;
            entity.VATValue = VATValue;
        }

    }
}
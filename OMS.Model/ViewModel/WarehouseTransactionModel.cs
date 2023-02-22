using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class WarehouseTransactionSummaryModel : BaseModel
    {
        public WarehouseTransactionSummaryModel()
        {
            strDate = DateTime.Now.ToString("dd/MM/yyyy");
            Date = DateTime.Now;
        }
        public WarehouseTransactionSummaryModel(WarehouseTransaction entity, string lang)
        {
            Id = entity.Id;
            strDate = entity.Date.HasValue ? entity.Date.Value.ToString("dd/MM/yyyy") : "";
            Date = DateTime.Now;
            DocNo = entity.DocNo;
            EmployeeName = entity.EmployeeName;
            WarehouseName = entity.Warehouse?.NameAr;
            switch (entity.TransactionType)
            {
                case (byte)WarehouseTransactionEnum.Add:
                    TransactionTypeName = lang == "ar" ? "إضافة مخزني" : "Warehouse Add";
                    break;
                case (byte)WarehouseTransactionEnum.Subtract:
                    TransactionTypeName = lang == "ar" ? "صرف مخزني" : "Warehouse Subtract";
                    break;
                case (byte)WarehouseTransactionEnum.Return:
                    TransactionTypeName = lang == "ar" ? "مرتجع" : "Warehouse Return";
                    break;
                case (byte)WarehouseTransactionEnum.Dispose:
                    TransactionTypeName = lang == "ar" ? "إهلاك مخزني" : "Warehouse Dispose";
                    break;
                default:
                    break;
            }
        }

        public string strDate { get; set; }
        public DateTime? Date { get; set; }
        public string DocNo { get; set; }
        public string EmployeeName { get; set; }
        public string WarehouseName { get; set; }
        public string TransactionTypeName { get; set; }

    }

    public class WarehouseTransactionModel : WarehouseTransactionSummaryModel
    {
        public WarehouseTransactionModel() : base()
        {
            Details = new List<WarehouseTransactionDetailsModel>();
        }
        public WarehouseTransactionModel(WarehouseTransaction entity, string lang) : base(entity, lang)
        {
            Notes = entity.Notes;
            CustomerId = entity.CustomerId;
            WarehouseId = entity.WarehouseId;
            TransactionType = entity.TransactionType ?? 1;
            Details = entity.WarehouseTransactionDetails.Select(i => new WarehouseTransactionDetailsModel(i)).ToList();
        }
        public string Notes { get; set; }
        public long EmployeeId { get; set; }
        public long? CustomerId { get; set; }
        public byte TransactionType { get; set; }
        public long? WarehouseId { get; set; }
        public long? DestWarehouseId { get; set; }
        public List<WarehouseTransactionDetailsModel> Details { get; set; }

        public void FillEntity(WarehouseTransaction entity)
        {
            entity.Date = Date;
            entity.DocNo = DocNo;
            entity.CustomerId = CustomerId;
            entity.EmployeeName = EmployeeName;
            entity.Id = Id;
            entity.Notes = Notes;
            entity.TransactionType = TransactionType;
            entity.WarehouseId = WarehouseId;
            entity.WarehouseTransactionDetails = Details.Select(d => new WarehouseTransactionDetail
            {
                Id = d.Id,
                ProductId = d.ProductId,
                Qty = d.Qty,
            }).ToList();
        }
    }

    public class WarehouseTransactionDetailsModel : BaseModel
    {
        public WarehouseTransactionDetailsModel()
        { }
        public WarehouseTransactionDetailsModel(WarehouseTransactionDetail entity)
        {
            Id = entity.Id;
            ProductId = entity.ProductId;
            ProductName = entity.Product != null ? entity.Product?.NameAr : entity.MedicalLensMaster?.Name;
            Qty = entity.Qty ?? 0;
        }

        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
    }
}
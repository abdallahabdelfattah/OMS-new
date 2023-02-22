using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class WarehouseTransferModel : BaseModel
    {
        public WarehouseTransferModel()
        {
            Details = new List<WarehouseTransactionDetailsModel>();
        }
        public WarehouseTransferModel(WarehouseTransaction entity, string lang)
        {
            Date = entity.Date;
            DocNo = entity.DocNo;
            EmployeeName = entity.EmployeeName;
            Notes = entity.Notes;
            WarehouseId = entity.WarehouseId;
            Details = entity.WarehouseTransactionDetails.Select(i => new WarehouseTransactionDetailsModel(i)).ToList();
        }

        public DateTime? Date { get; set; }
        public string DocNo { get; set; }
        public string EmployeeName { get; set; }
        public string Notes { get; set; }
        public long EmployeeId { get; set; }
        public long? WarehouseId { get; set; }
        public long? DestWarehouseId { get; set; }
        public List<WarehouseTransactionDetailsModel> Details { get; set; }

        public void FillEntity(WarehouseTransaction entity)
        {
            entity.Date = Date;
            entity.DocNo = DocNo;
            entity.EmployeeName = EmployeeName;
            entity.Id = Id;
            entity.Notes = Notes;
            entity.WarehouseId = WarehouseId;
            entity.WarehouseTransactionDetails = Details.Select(d=> new WarehouseTransactionDetail { 
                Id=d.Id,
                ProductId=d.ProductId,
                Qty=d.Qty,
            }).ToList();
        }
    }
}
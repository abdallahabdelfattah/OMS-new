using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.Model.ViewModel
{
    public class WarehouseVerificationSummaryModel : BaseModel
    {
        public WarehouseVerificationSummaryModel()
        {
            StrDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
            
        }
        public WarehouseVerificationSummaryModel(WarehouseVerification entity, string language)
        {
            Id = entity.Id;
            StrDate = entity.Date.Value.ToString("dd/MM/yyyy hh:mm");
            DocNo = entity.DocumentNo;
            WarehouserName = entity.WarehouserName;
            WarehouseName = language == "ar" ? entity.Warehouse?.NameAr : entity.Warehouse?.NameEn;
            IsSettled = entity.IsSettled;
        }

        public string StrDate { get; set; }
        public string DocNo { get; set; }
        public string WarehouserName { get; set; }
        public string WarehouseName { get; set; }
        public bool IsSettled { get; set; }
        
    }

    public class WarehouseVerificationModel : WarehouseVerificationSummaryModel
    {
        public DateTime? Date { get; set; }
        public long? WarehouseId { get; set; }
        public string Notes { get; set; }
        public List<WarehouseVerificationDetailsModel> Details { get; set; }

        public WarehouseVerificationModel() : base()
        {
            Date = DateTime.Now;
            Details = new List<WarehouseVerificationDetailsModel>();
        }
        public WarehouseVerificationModel(WarehouseVerification entity, string language) : base(entity, language)
        {
            Date = entity.Date;
            WarehouseId = entity.WarehouseId;
            WarehouserName = entity.WarehouserName;
            Notes = entity.Notes;
            IsSettled = entity.IsSettled;
            Details = entity.WarehouseVerificationDetails.Select(i => new WarehouseVerificationDetailsModel(i, language)).ToList();
        }

        public void FillEntity(WarehouseVerification entity)
        {
            entity.Date = DateTime.Now;
            entity.DocumentNo = DocNo;
            entity.WarehouserName = WarehouserName;
            entity.WarehouseId = WarehouseId;
            entity.Notes = Notes;
            
            entity.WarehouseVerificationDetails = Details.Select(i => new WarehouseVerificationDetail()
            {
                WarehouseVerificationId = i.WarehouseVerificationId,
                ProductId=i.ProductId,
                TransQty = i.TransQty,
                ActualQty = i.ActualQty,
                UnitOfficialPrice = i.UnitOfficialPrice
            }).ToList();

            if (entity.Id == 0)
            {
                entity.CreatedOn = DateTime.Now;
            }
        }
    }
}
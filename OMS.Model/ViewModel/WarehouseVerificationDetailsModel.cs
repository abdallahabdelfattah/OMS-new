using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class WarehouseVerificationDetailsModel : BaseModel
    {
        public WarehouseVerificationDetailsModel()
        {

        }
        public WarehouseVerificationDetailsModel(WarehouseVerificationDetail entity, string language)
        {
            Id = entity.Id;
            ProductId = entity.ProductId;
            TransQty = entity.TransQty ?? 0;
            ActualQty = entity.ActualQty ?? 0;
            WarehouseVerificationId = entity.WarehouseVerificationId;
            UnitOfficialPrice = entity.UnitOfficialPrice??0;
            ProductName = language == "ar"? entity?.Product?.NameAr: entity?.Product?.NameEn;
        }

        public long? ProductId { get; set; }
        public string ProductName { get; set; }
        public long? WarehouseVerificationId { get; set; }
        public decimal UnitOfficialPrice { get; set; }
        public int TransQty { get; set; }
        public int ActualQty { get; set; }
    }
}
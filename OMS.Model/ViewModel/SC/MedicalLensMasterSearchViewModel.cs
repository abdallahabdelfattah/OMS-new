namespace OMS.Model.ViewModel
{
    public class MedicalLensMasterSearchViewModel : BaseSearchViewModel
    {
        public string Code { get; set; }
        public long? BrandId { get; set; }
        public long? ColorId { get; set; }
        public long? WarehouseId { get; set; }
        public long? MaterialId { get; set; }
        public long LenseIndexId { get; set; }
    }
}
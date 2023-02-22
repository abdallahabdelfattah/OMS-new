namespace OMS.Model.ViewModel
{
    public class MedicalLensDetailsSearchViewModel : BaseSearchViewModel
    {
        public string Code { get; set; }
        public long? BrandId { get; set; }
        public long? WarehouseId { get; set; }
    }
}
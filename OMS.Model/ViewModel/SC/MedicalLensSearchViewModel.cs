namespace OMS.Model.ViewModel
{
    public class MedicalLensSearchViewModel : BaseSearchViewModel
    {
        public string Code { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public long? WarehouseId { get; set; }
        public long? SPHId { get; set; }
        public long? CYLId { get; set; }
    }
}
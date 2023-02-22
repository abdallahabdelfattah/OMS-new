namespace OMS.Model.ViewModel
{
    public class ProductSearchViewModel : BaseSearchViewModel
    {
        public string Code { get; set; }
        public long? CategoryId { get; set; }
        public long? SupplierId { get; set; }
        public long? WarehouseId { get; set; }
        public long? BrandId { get; set; }
        public long? ColorCategoryId { get; set; }
        public long? PowerId { get; set; }
        public long? UsePeriodId { get; set; }
        public long? ColorId { get; set; }



    }
}
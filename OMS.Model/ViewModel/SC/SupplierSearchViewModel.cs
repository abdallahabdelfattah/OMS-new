namespace OMS.Model.ViewModel
{
    public class SupplierSearchViewModel : BaseSearchViewModel
    {
        public string Code { get; set; }
        public string SalesManName { get; set; }
        public bool? IsExternal { get; set; }
    }
}

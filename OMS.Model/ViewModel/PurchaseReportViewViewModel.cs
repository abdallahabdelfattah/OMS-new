using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class PurchaseReportViewSummeryModel : BaseModel
    {
        public PurchaseReportViewSummeryModel()
        {

        }
        public PurchaseReportViewSummeryModel(PurchaseReportView entity, string language)
        {
            ProductQty = entity.ProductQty;
            ProductCode = entity.ProductCode;
            ProductPurchasePrice = entity.ProductPurchasePrice;
            ProductChangeRate = entity.ProductChangeRate;
            CreatedOn = entity.CreatedOn;
            productName = language.ToLower() == "ar" ? entity.productNameAr : entity.productNameEn;
            SupplierName = language.ToLower() == "ar" ? entity.SupplierNameAr : entity.SupplierNameEn;
            WarehouseName  = language.ToLower() == "ar" ? entity.WarehouseNameAr: entity.WarehouseNameEn;
        }
        public int? ProductQty { get; set; }
        public decimal? ProductChangeRate { get; set; }
        public string ProductCode { get; set; }
        public string productName { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<decimal> ProductPurchasePrice { get; set; }
        public string SupplierName { get; set; }
        public string WarehouseName { get; set; }
    }
    public class PurchaseReportViewViewModel : PurchaseReportViewSummeryModel
    {

        public PurchaseReportViewViewModel() { }
        public PurchaseReportViewViewModel(PurchaseReportView entity, string language) : base(entity, language)
        { }
    }
}

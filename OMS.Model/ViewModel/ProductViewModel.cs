using OMS.DAL.DataAccess;
using System;

namespace OMS.Model.ViewModel
{
    public class ProductSummaryModel : BaseModel
    {
        public ProductSummaryModel()
        { }
        public ProductSummaryModel(Product entity, string language)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            Code = entity.Code;
            OfficialPrice = entity.OfficialPrice ?? 0;
            CategoryName = language.ToLower() == "ar" ? entity.ProductCategory?.NameAr : entity.ProductCategory?.NameEn;
            SupplierName = language.ToLower() == "ar" ? entity.Supplier?.NameAr : entity.Supplier?.NameEn;
            MinQty = entity.MinQty ?? 0;
            Qty = entity.Qty ?? 0;
            BrandId = entity.BrandId; BrandName = entity.Brand?.BrandName;
            ColorName = entity.Color?.ColorName;
            ModelName = entity.Model?.ModelName;
            ExpireDate = entity.ExpireDate;
            RetailPrice = entity.RetailPrice ?? 0;
            WholeSellPrice = entity.WholeSellPrice ?? 0;
            AllowedDiscount = entity.AllowedDiscount?? 0;
            BarCode = entity.BarCode; 
            Power = entity.Power?.Value;
            ColorCategoryId = entity.Color?.ColorCategoryId;
            ColorCategory = entity.Color?.ColorName;
        }
        public long? ColorCategoryId { get; set; }
        public string ColorCategory { get; set; }
        public string BarCode { get; set; }
        public double? WholeSellPrice { get; set; }
        public double? AllowedDiscount { get; set; }
        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal OfficialPrice { get; set; }
        public double? RetailPrice { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        public int MinQty { get; set; }
        public int Qty { get; set; }
        public long? BrandId { get; set; }
        public long? Suppliers { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ColorName { get; set; }
        public string Power { get; set; }
    }

    public class ProductViewModel : ProductSummaryModel
    {
        public long? GradeId { get; set; }
        public long? CategoryId { get; set; }
        public long? SupplierId { get; set; }
        public long? MaterialId { get; set; }
        public long? ColorId { get; set; }
        public long? ModelId { get; set; }
       // public long? BrandId { get; set; }
        public long? PowerId { get; set; }
        public long? CLSPAndSolutionTypeId { get; set; }
        public long? UsePeriodId { get; set; }
        public string Design { get; set; }
        public string Volume { get; set; }
        
        public ProductViewModel() : base()
        { }
        public ProductViewModel(Product entity, string language) : base(entity, language)
        {
            CategoryId = entity.CategoryId;
            SupplierId = entity.SupplierId;
            MaterialId = entity.MaterialId;
            ColorId = entity.ColorId;
            ModelId = entity.ModelId;
            BrandId = entity.BrandId;
            PowerId = entity.PowerId;
            GradeId = entity.GradeId;
            RetailPrice = entity.RetailPrice ?? 0;
            CLSPAndSolutionTypeId = entity.CLSPAndSolutionTypeId;
            UsePeriodId = entity.UsePeriodId;
            Design = entity.Design;
            Volume = entity.Volume;
        }

        public void FillEntity(Product entity)
        {
            entity.NameAr = CategoryName + "-" + Code + "-" + ModelName + "-" + BrandName + "-" + ColorName;
            entity.NameEn = CategoryName + "-" + Code + "-" + ModelName + "-" + BrandName;
            entity.Code = Code;
            entity.OfficialPrice = OfficialPrice;
            entity.RetailPrice = RetailPrice;
            entity.WholeSellPrice = WholeSellPrice;
            entity.AllowedDiscount = AllowedDiscount;
            entity.CategoryId = CategoryId;
            entity.SupplierId = SupplierId;
            entity.BrandId = BrandId;
            entity.MinQty = MinQty;
            entity.ColorId = ColorId;
            entity.MaterialId = MaterialId;
            entity.PowerId = PowerId;
            entity.GradeId = GradeId;
            entity.CLSPAndSolutionTypeId = CLSPAndSolutionTypeId;
            entity.UsePeriodId = UsePeriodId;
            entity.Design = Design;
            entity.Volume = Volume;
            entity.BarCode = BarCode;
            entity.ExpireDate = ExpireDate;
            entity.ModelId = ModelId;

        }
    }
}
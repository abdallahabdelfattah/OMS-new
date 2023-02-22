using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class PowerSummaryModel : BaseModel
    {
        public PowerSummaryModel()
        { }
        public PowerSummaryModel(Power entity,string language)
        {
            Id = entity.Id;
            Value = entity.Value;
            ModelId = entity.ModelId;
            CategoryId = entity.CategoryId;
            BrandId = entity.BrandId;
            BrandId = entity.BrandId; BrandName = entity.Brand?.BrandName;
            ModelName = entity.Model?.ModelName;
            CategoryName = language.ToLower() == "ar" ? entity.ProductCategory?.NameAr : entity.ProductCategory?.NameEn;


        }


        public string Value { get; set; }
        public long? ModelId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string CategoryName { get; set; }

    }

    public class PowerModel : PowerSummaryModel
    {
        public PowerModel() : base()
        { }
        public PowerModel(Power entity,string language) : base(entity, language)
        {  }
        public void FillEntity(Power entity)
        {
           entity.Value = Value;
           entity.ModelId=ModelId;
           entity.CategoryId=CategoryId;
           entity.BrandId=BrandId;
        }
    }
}
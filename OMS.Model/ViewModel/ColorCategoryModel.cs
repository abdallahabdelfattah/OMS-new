using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class ColorCategoryModel : BaseModel
    {
        public ColorCategoryModel()
        {
        }
        public ColorCategoryModel(ColorCategoy entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            ModelId = entity.Model?.Id;
            BrandId = entity.Model?.BrandId;
            CategoryId = entity.Model?.Brand?.ProductCategoryId;
            ModelName = entity.Model?.ModelName;
        }

        public long? ModelId { get; set; }
        public long? CategoryId { get; set; }
        public long? BrandId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ModelName { get; set; }

        public void FillEntity(ColorCategoy entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.ModelId = ModelId;
        }
    }

}
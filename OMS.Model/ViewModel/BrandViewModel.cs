using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class BrandSummeryViewModel: BaseModel
    {
        public BrandSummeryViewModel()
        {   
        }
        public BrandSummeryViewModel(Brand entity, bool isArabic)
        {
            Id = entity.Id;
            BrandName = entity.BrandName;
            ProductCategoryId = entity.ProductCategoryId;
            CategoryName = isArabic ? entity.ProductCategory?.NameAr : entity.ProductCategory?.NameEn;

        }
        public string CategoryName { get; set; }
        public long Id { get; set; }
        public long? ProductCategoryId { get; set; }
        public string BrandName { get; set; }
    }
    public class BrandViewModel: BrandSummeryViewModel
    {
        public BrandViewModel() : base()
        {

        }
        public BrandViewModel(Brand entity, bool isArabic) : base(entity,isArabic)
        {
            BrandName = entity.BrandName;
        }


        public Brand MapFromViewModeToEntity(BrandViewModel model)
        {
            if (model != null)
                return new Brand
                {
                    Id = model.Id,
                    BrandName = model.BrandName,
                    ProductCategoryId = model.ProductCategoryId

                };
            return new Brand();
        }

    }
}

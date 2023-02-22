using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class ModelSummeryViewModel: BaseModel
    {
        public ModelSummeryViewModel()
        {
            
        }
        public ModelSummeryViewModel(DAL.DataAccess.Model model)
        {
            Id = model.Id;
            ModelName = model.ModelName;
            BrandId = model.BrandId;
            CategoryId = model.Brand.ProductCategoryId;
            BrandName = model.Brand?.BrandName;
            CategoryName = model.Brand?.ProductCategory?.NameAr;
        }

        public string BrandName { get; set; }
        public string CategoryName { get; set; }

        public long Id { get; set; }
        public string ModelName { get; set; }
        public long? BrandId { get; set; }
        public long? CategoryId { get; set; }

    }
    public class BrandModelViewModel : ModelSummeryViewModel
    {
        public BrandModelViewModel(DAL.DataAccess.Model model) : base(model)
        {
        }
        public BrandModelViewModel()
        {
            
        }
        public DAL.DataAccess.Model MapFromViewModeToEntity(BrandModelViewModel model)
        {
            //Note: Check if Brand in category cc
            //Select categoryId
            
            return new DAL.DataAccess.Model
            {
                BrandId = model.BrandId,
                Id = model.Id,
                ModelName = model.ModelName
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class ColorSummeryViewModel : BaseModel
    {
        public ColorSummeryViewModel()
        {
        }
        public ColorSummeryViewModel(Color entity)
        {
            Id = entity.Id;
            BrandModelId = entity.ModelId;
            BrandId = entity.Model?.BrandId;
            BrandName = entity.Model?.Brand.BrandName;
            ModelName = entity.Model?.ModelName;
            ColorName = entity.ColorName;
            ColorCategoryId = entity.ColorCategoryId;
            ColorCategoryName = entity.ColorCategoy?.NameAr;
        }

        public string BrandName { get; set; }

        public long? BrandModelId { get; set; }
        public long? BrandId { get; set; }
        public long? ColorCategoryId { get; set; }

        public string ColorName { get; set; }

        public string ModelName { get; set; }
        public string ColorCategoryName { get; set; }

        public long Id { get; set; }
    }
    public class ColorViewModel : ColorSummeryViewModel
    {
        public ColorViewModel():base()
        {
        }

        public ColorViewModel(Color entity):base(entity)
        {
            
        }

        public Color MapFromViewModeToEntity(ColorViewModel model)
        {
            return new Color
            {
                Id = model.Id,
                ColorName = model.ColorName,
                ModelId = model.BrandModelId,
                ColorCategoryId=model.ColorCategoryId
               
            };
        }
    }
}

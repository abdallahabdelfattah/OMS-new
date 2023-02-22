using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class CategorySummaryModel : BaseModel
    {
        public CategorySummaryModel()
        {}
        public CategorySummaryModel(ProductCategory entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            ParentCategory = entity.Parent?.NameAr;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string ParentCategory { get; set; }
    }

    public class CategoryModel : CategorySummaryModel
    {
        public long? ParentCategoryId { get; set; }
        public List<CategoryModel> Children
        {
            get; set;
        }
        public long Depth { get; set; }
        public CategoryModel() : base()
        { Children = new List<CategoryModel>(); }
        public CategoryModel(ProductCategory entity, long depth = 0) : base(entity)
        {
            Depth = depth;
            if (entity.Parent != null)
            {
                ParentCategory = entity.Parent.NameAr;
                ParentCategoryId = entity.ParentId;
            }
            else
            {
                ParentCategoryId = 0;
            }

            Children = entity.Children.Select(c => new CategoryModel(c, Depth + 1)).ToList();
        }
        public void FillEntity(ProductCategory entity)
        {
            entity.Id = Id;
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;

            if (ParentCategoryId > 0)
            {entity.ParentId = ParentCategoryId;}
            else
            {entity.ParentId = null;}
        }
    }
}
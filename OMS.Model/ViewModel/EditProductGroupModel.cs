using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class EditProductGroupSummaryModel : BaseModel
    {
        public EditProductGroupSummaryModel()
        { }
        public EditProductGroupSummaryModel(Product entity)
        {
            Id = entity.Id;
            Name = entity.NameAr;
            CategoryName = entity.ProductCategory?.NameAr;
            CategoryId = entity.CategoryId;
            OfficialPrice = entity.OfficialPrice;
            Qty = entity.Qty;
            Code = entity.Code;

        }

        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public long? CategoryId { get; set; }
        public decimal? OfficialPrice { get; set; }
        public decimal NewValue { get; set; }
        public int? Qty { get; set; }
        public string OperationSign { get; set; }
        public int OperationTypeId { get; set; }
    }

    public class EditProductGroupModel : EditProductGroupSummaryModel
    {
        public EditProductGroupModel() : base()
        { }
        public EditProductGroupModel(Product entity) : base(entity)
        {  }
        public void FillEntity(Product entity,decimal newValue,string sign)
        {
            var isAdd = sign == "+" ? true : false;
            entity.OfficialPrice=isAdd?entity.OfficialPrice+newValue: entity.OfficialPrice - newValue;
            entity.Qty = isAdd ?entity.Qty + (int)newValue : entity.Qty - (int)newValue;
        }
    }
}
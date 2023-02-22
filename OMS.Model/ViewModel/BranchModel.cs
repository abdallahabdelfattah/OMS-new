using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class BranchSummaryModel : BaseModel
    {
        public BranchSummaryModel()
        { }
        public BranchSummaryModel(Branch entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            CountryName = entity.Region?.Country?.NameAr;
            CityName = entity.Region?.City?.NameAr;
            RegionName = entity.Region?.NameAr;
            Phone = entity.Phone;
            Mobile = entity.Mobile;
            ImagePath = entity.Upload?.FileName;
            WareHouseEmployeeName = entity.User?.NameAr;

        }

        public string NameAr { get; set; }
        public string WareHouseEmployeeName { get; set; }
        public string NameEn { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ImagePath { get; set; }
    }

    public class BranchModel : BranchSummaryModel
    {
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public long? RegionId { get; set; }
        public long? ImageId { get; set; }
        public long? WareHouseEmployeeId { get; set; }
        public BranchModel() : base()
        { }
        public BranchModel(Branch entity) : base(entity)
        {
            CountryId = entity.Region?.CountryId;
            CityId = entity.Region?.CityId;
            RegionId = entity.Region?.Id;
            ImageId = entity.ImageId;
            WareHouseEmployeeId = entity.User?.Id;
        }
        public void FillEntity(Branch entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.RegionId = RegionId;
            entity.ImageId = ImageId;
            entity.Phone = Phone;
            entity.Mobile = Mobile;

            if (entity.User == null)
                entity.User = new User();
            entity.User.Id = WareHouseEmployeeId ?? 0;
            if (entity.Warehouse == null)
                entity.Warehouse = new Warehouse();
            entity.Warehouse.NameAr = NameAr;
            entity.Warehouse.NameEn = NameEn;
            entity.Warehouse.RegionId = RegionId;
            if (entity.Warehouse.User == null)
                entity.Warehouse.User = new User();
            entity.Warehouse.User.Id = WareHouseEmployeeId ?? 0;
        }
    }
}
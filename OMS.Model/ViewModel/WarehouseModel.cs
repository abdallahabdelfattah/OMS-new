using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class WarehouseSummaryModel : BaseModel
    {
        public WarehouseSummaryModel()
        {}
        public WarehouseSummaryModel(Warehouse entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            CountryName = entity.Region?.City?.Country.NameAr;
            CityName = entity.Region?.City?.NameAr;
            RegionName = entity.Region?.NameAr;
            //Address = entity.Address;
            EmployeeName = entity.User?.NameAr;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string EmployeeName { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string Address { get; set; }
    }

    public class WarehouseModel : WarehouseSummaryModel
    {
        public long? EmployeeId { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public long? RegionId { get; set; }
        //public long? WarehouseTypeID { get; set; }

        public WarehouseModel() : base()
        {}
        public WarehouseModel(Warehouse entity) : base(entity)
        {
            EmployeeId = entity.EmployeeId;
            CountryId = entity.Region?.City?.CountryId;
            CityId = entity.Region?.CityId;
            RegionId = entity.RegionId;
            //WarehouseTypeID = entity.WarehouseTypeID;
        }

        public void FillEntity(Warehouse entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.RegionId = RegionId;
            entity.EmployeeId = EmployeeId;
            //entity.Address = Address;
        }
    }
}
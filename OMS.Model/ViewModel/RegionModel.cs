using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class RegionSummaryModel : BaseModel
    {
        public RegionSummaryModel()
        { }
        public RegionSummaryModel(Region entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            CountryName = entity.Country?.NameAr;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string CountryName { get; set; }
    }

    public class RegionModel : RegionSummaryModel
    {
        public long? CountryId { get; set; }
        public RegionModel() : base()
        { }
        public RegionModel(Region entity) : base(entity)
        {
            CountryId = entity.City?.CountryId; 
        }
        public void FillEntity(Region entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.CountryId = CountryId;
        }
    }
}
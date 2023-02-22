using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class CitySummaryModel : BaseModel
    {
        public CitySummaryModel()
        { }
        public CitySummaryModel(City entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            CountryName = entity.Country?.NameAr;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public string CountryName { get; set; }
    }

    public class CityModel : CitySummaryModel
    {
        public long? CountryId { get; set; }
        public CityModel() : base()
        { }
        public CityModel(City entity) : base(entity)
        { CountryId = entity.CountryId; }
        public void FillEntity(City entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.CountryId = CountryId;
        }
    }
}
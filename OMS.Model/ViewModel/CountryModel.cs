using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class CountrySummaryModel : BaseModel
    {
        public CountrySummaryModel()
        {}
        public CountrySummaryModel(Country entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public class CountryModel : CountrySummaryModel
    {
        public CountryModel() : base()
        {}
        public CountryModel(Country entity) : base(entity)
        {
        }

        public void FillEntity(Country entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
        }
    }
}
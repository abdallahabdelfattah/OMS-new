using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class DoctorSummaryModel : BaseModel
    {
        public DoctorSummaryModel()
        { }
        public DoctorSummaryModel(Doctor entity, string language)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            Mobile = entity.Mobile;
            Phone = entity.Phone;
            Address = entity.Address;
            RegionName = language.ToLower() == "ar" ? entity.Region?.NameAr : entity.Region?.NameEn;
            CityName = language.ToLower() == "ar" ? entity.Region?.City?.NameAr : entity.Region?.City?.NameEn;
            CountryName = language.ToLower() == "ar" ? entity.Region?.City?.Country?.NameAr : entity.Region?.City?.Country?.NameEn;

        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CountryName { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
    }

    public class DoctorViewModel : DoctorSummaryModel
    {
        public DoctorViewModel() : base()
        { }
        public DoctorViewModel(Doctor entity, string language) : base(entity, language)
        {
            CityId = entity.Region?.CityId;
            RegionId = entity.RegionId;
            CountryId = entity.Region?.City?.CountryId;
        }
        public long? RegionId { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }
    }
}
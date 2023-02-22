using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class SystemConfigurationSummaryModel : BaseModel
    {
        public SystemConfigurationSummaryModel()
        { }
        public SystemConfigurationSummaryModel(SystemConfiguration entity)
        {
            Id = entity.Id;
            CompanyNameAr = entity.CompanyNameAr;
            CompanyNameEn = entity.CompanyNameEn;
            DisableSMSNotifications = entity.DisableSMSNotifications ?? false;
            DisableEmailNotifications = entity.DisableEmailNotifications ?? false;
            EmailFromName = entity.EmailFromName;
            EmailFromAddress = entity.EmailFromAddress;
            LogoPath = entity.Upload?.FileName;
        }

        public string CompanyNameAr { get; set; }
        public string CompanyNameEn { get; set; }
        public bool DisableSMSNotifications { get; set; }
        public bool DisableEmailNotifications { get; set; }
        public string EmailFromName { get; set; }
        public string EmailFromAddress { get; set; }
        public string LogoPath { get; set; }
    }

    public class SystemConfigurationModel : SystemConfigurationSummaryModel
    {
        public SystemConfigurationModel() : base()
        { }
        public SystemConfigurationModel(SystemConfiguration entity) : base(entity)
        {
            LogoId = entity.LogoId;
        }
        public long? LogoId { get; set; }
        public void FillEntity(SystemConfiguration entity)
        {
            entity.CompanyNameAr = CompanyNameAr;
            entity.CompanyNameEn = CompanyNameEn;
            entity.DisableSMSNotifications = DisableSMSNotifications;
            entity.DisableEmailNotifications = DisableEmailNotifications;
            entity.EmailFromName = EmailFromName;
            entity.EmailFromAddress = EmailFromAddress;
            entity.LogoId = LogoId;
        }
    }
}
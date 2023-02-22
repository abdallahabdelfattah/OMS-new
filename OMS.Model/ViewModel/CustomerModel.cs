using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class CustomerSummaryModel : BaseModel
    {
        public CustomerSummaryModel()
        {
            
        }
        public CustomerSummaryModel(Customer entity, string lang)
        {
            Id = entity.Id;
            Code = entity.Code;
            Title = lang =="ar"? entity.CustomerTitle?.NameAr : entity.CustomerTitle?.NameEn;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            Phone = entity.Phone;
            Mobile = entity.Mobile;
            Email = entity.Email;
            

            //RegionName = entity.Region?.Name;
            //CityName = entity.City?.Name;
            //CountryName = entity.Country?.Name;
        }

        public string Code { get; set; }
        public string Title { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        

        //public string Country { get; set; }
        //public string City { get; set; }
        //public string Region { get; set; }
    }

    public class CustomerModel : CustomerSummaryModel
    {
        public string InsuranceCardNo { get; set; }
        public DateTime? CreationFileDate { get; set; }
        public string CreationFileDateStr { get; set; }
        public byte Gendar { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public GenderEnum Gender { get; set; }
        public string IdentityNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public long? TitleId { get; set; }
        public long? NationalityId { get; set; }
        public long? CountryId { get; set; }
        public long? CityId { get; set; }
        public bool IsEnabled { get; set; }

        public ExaminationModel LastExamination { get; set; }
        public List<ExaminationModel> Examinations { get; set; }
        public List<DiseaseSummaryModel> CustomerDisease { get; set; }
        public List<SalesTransactionSummaryModel> CustomerSales { get; set; }

        public CustomerModel() : base()
        {
            CreationFileDate = DateTime.Now;
            Examinations = new List<ExaminationModel>();
            NewExamination = new ExaminationModel();
            CustomerDisease = new List<DiseaseSummaryModel>();
        }

        public ExaminationModel NewExamination { get; set; }

        public CustomerModel(Customer entity, string lang) : base(entity, lang)
        {
            Address = entity.Address;
            Notes = entity.Notes;
            Gender = entity.Gender.HasValue? (GenderEnum)entity.Gender : GenderEnum.Male;
            IdentityNo = entity.IdentityNo;
            DateOfBirth = entity.DateOfBirth;
            TitleId = entity.TitleId;
            IsActive = entity.IsActive;
            NationalityId = entity.NationalityId;
            InsuranceCardNo = entity.InsuranceCardNo;
            CreationFileDate = entity.CreationFileDate;
            CreationFileDateStr = entity.CreationFileDate.HasValue? entity.CreationFileDate.Value.ToString("dd/MM/yyyy"):"";
            NewExamination = new ExaminationModel();
            var Examination = entity.Examinations.LastOrDefault();

            if (Examination != null)
            {
                LastExamination = new ExaminationModel(Examination);
            }
            
            Examinations = entity.Examinations.Select(e=> new ExaminationModel(e)).ToList();
            CustomerDisease = entity.CustomerDiseases.Select(e=> new DiseaseSummaryModel(e.Disease)).ToList();
            CustomerSales = entity.SalesTransactions.Select(e => new SalesTransactionSummaryModel(e, lang)).ToList();
        }

        public void FillEntity(Customer entity)
        {
            entity.Code = Code;
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.Phone = Phone;
            entity.Mobile = Mobile;
            entity.Email = Email;
            entity.Address = Address;
            entity.Notes = Notes;
            entity.Gender = (byte?)Gender;
            entity.IdentityNo = IdentityNo;
            entity.DateOfBirth = DateOfBirth;
            entity.IsActive = IsActive;
            entity.TitleId = TitleId;
            entity.NationalityId = NationalityId;
            entity.InsuranceCardNo = InsuranceCardNo;
            
            entity.IsActive = IsActive;

            //entity.ModifiedBy = SessionRegistery.CurrentUser.ID;
            entity.UpdatedOn = DateTime.Now;

            if (entity.Id == 0)
            {
                //entity.CreatedBy = SessionRegistery.CurrentUser.ID;
                entity.CreatedOn = DateTime.Now;
                entity.CreationFileDate = DateTime.Now;
            }
        }
    }

    public class ContactPersonModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Email { get; set; }
    }
}
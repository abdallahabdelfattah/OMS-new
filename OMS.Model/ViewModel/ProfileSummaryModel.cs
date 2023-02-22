using OMS.DAL.DataAccess;
using System;
using System.Linq;

namespace OMS.Model.ViewModel
{
    public class ProfileSummaryModel : BaseModel
    {
        public ProfileSummaryModel()
        { }

        public ProfileSummaryModel(User entity, string language = "ar")
        {
            Id = entity.Id;
            UserName = entity.UserName;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            Email = entity.Email;
            ImagePath = entity.Upload?.FileName;
            Password = entity.Password;
            
            if (entity.UserGroups.Count > 0)
            {
                var ug = entity.UserGroups.FirstOrDefault();
                var group = ug.Group;
                if (group != null)
                {
                    GroupName = language == "ar" ? group?.NameAr : group?.NameEn;
                }
            }
            
        }
        public string UserName { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Password { get; set; }
        public string GroupName { get; set; }
    }

    public class ProfileModel : ProfileSummaryModel
    {
        public ProfileModel() : base()
        {
        }
        public ProfileModel(User entity, string language = "ar") : base(entity, language)
        {
            Password = ConfirmPassword = entity.Password;
            ImageId = entity.ImageId;
        }

        public string ConfirmPassword { get; set; }
        public long? ImageId { get; set; }
        public void FillEntity(User entity)
        {
            entity.UpdatedOn = DateTime.Now;
            entity.UserName = UserName;
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.Email = Email;
            entity.Password = Password;
            entity.ImageId = ImageId;
        }
    }
}
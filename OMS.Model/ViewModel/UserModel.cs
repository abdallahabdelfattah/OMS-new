using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class UserSummaryModel : BaseModel
    {
        public UserSummaryModel()
        { }

        public UserSummaryModel(User entity)
        {
            Id = entity.Id;
            Code = entity.Code;
            UserName = entity.UserName;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            Email = entity.Email;
            ImagePath = entity.Upload?.FileName;

            if (entity.UserGroups.Count > 0)
            {
                var ug = entity.UserGroups.FirstOrDefault();
                if (ug.Group != null)
                {
                    GroupNameAr = ug.Group.NameAr;
                    GroupNameEn = ug.Group.NameEn;
                    GroupId = ug.GroupId;
                }
            }
        }

        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public long? GroupId { get; set; }
        public string GroupNameAr { get; set; }
        public string GroupNameEn { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public long BranchId { get; set; }

    }

    public class UserModel : UserSummaryModel
    {
        public UserModel() : base()
        {
            PageActionsModes = new List<UserPageActionModel>();
            UserBranches = new List<WarehouseSummaryModel>();
        }
        public UserModel(User entity) : base(entity)
        {
            Password = ConfirmPassword = entity.Password;
            PageActionsModes = entity.UserPageActions.Select(upa => new UserPageActionModel() { PageActionId = upa.PageActionId, Mode = upa.Mode }).ToList();
            Status = (UserStatus)entity.Status;
            IdentityNo = entity.IdentityNo;
            WorkEmail = entity.WorkEmail;
            HomePhone = entity.HomePhone;
            Mobile = entity.Mobile;
            Qualification = entity.Qualification;
            GraduationYear = entity.GraduationYear;
            Skills = entity.Skills;
            Address = entity.Address;
            JoiningDate = entity.JoiningDate;
            DateOfBirth = entity.DateOfBirth;
            JobId = entity.JobId;
            NationalityId = entity.NationalityId;
            ImageId = entity.ImageId;
            MorningFrom = entity.MorningFrom;
            MorningTo = entity.MorningTo;
            EveningFrom = entity.EveningFrom;
            EveningTo = entity.EveningTo;

            UserBranches = entity.UserWarehouses.Select(e => new WarehouseSummaryModel(e.Warehouse)).ToList();
        }

        public string IdentityNo { get; set; }
        public string WorkEmail { get; set; }
        public string HomePhone { get; set; }
        public string Mobile { get; set; }
        public string Qualification { get; set; }
        public string GraduationYear { get; set; }
        public string Skills { get; set; }
        public string Address { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? JobId { get; set; }
        public long? NationalityId { get; set; }
        public long? ImageId { get; set; }
        public TimeSpan? MorningFrom { get; set; }
        public TimeSpan? MorningTo { get; set; }
        public TimeSpan? EveningFrom { get; set; }
        public TimeSpan? EveningTo { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<WarehouseSummaryModel> UserBranches { get; set; }
        public List<UserPageActionModel> PageActionsModes { get; set; }
        public UserStatus? Status { get; set; }

        public void FillEntity(User entity)
        {
            //entity.ModifiedBy = SessionRegistery.CurrentUser.ID;
            entity.UpdatedOn = DateTime.Now;
            entity.UserName = UserName;
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.Email = Email;
            entity.Status = (int?)Status;
            entity.Password = Password;

            entity.Code = Code;
            entity.DateOfBirth = DateOfBirth;
            entity.IdentityNo = IdentityNo;
            entity.WorkEmail = WorkEmail;
            entity.HomePhone = HomePhone;
            entity.Mobile = Mobile;
            entity.Qualification = Qualification;
            entity.GraduationYear = GraduationYear;
            entity.Skills = Skills;
            entity.Address = Address;
            entity.JoiningDate = JoiningDate;
            entity.JobId = JobId;
            entity.NationalityId = NationalityId;
            entity.ImageId = ImageId;
            entity.MorningFrom = MorningFrom;
            entity.MorningTo = MorningTo;
            entity.EveningFrom = EveningFrom;
            entity.EveningTo = EveningTo;

            var ug = entity.UserGroups.FirstOrDefault();
            if (GroupId.HasValue)
            {
                if (ug == null)
                {
                    ug = new UserGroup() { GroupId = GroupId };
                    entity.UserGroups.Add(ug);
                }
                else
                {
                    ug.GroupId = GroupId;
                }
            }

            //if (entity.UserPageActions.Count() > 0)
            //{
            //   // UserPageActionBL _UserPageActionBL = new UserPageActionBL();
            //    //_UserPageActionBL.DeleteUserPageAction(entity.ID);
            //    //delete old records
            //}

            foreach (var item in PageActionsModes)
            {
                entity.UserPageActions.Add(new UserPageAction() { PageActionId = item.PageActionId, Mode = (int)item.Mode });
            }
        }
    }

    public class UserPageActionModel
    {
        public long PageActionId { get; set; }
        //public PageActionMode Mode { get; set; }
        public int Mode { get; set; }
    }
}
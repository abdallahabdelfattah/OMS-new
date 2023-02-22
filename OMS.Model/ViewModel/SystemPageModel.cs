using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using System.Collections.Generic;
using System.Linq;

namespace OMS.Model.ViewModel
{
    public class SystemPageSummaryModel : BaseModel
    {
        public SystemPageSummaryModel()
        {

        }
        public SystemPageSummaryModel(SystemPage entity)
        {
            var pa = entity.PageActions.Where(v => v.NameEn == "View").FirstOrDefault();
            
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            InGroup = false;

            Children = entity.Children.Select(c => new UserSystemPageSummaryModel(c)).ToList();
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool InGroup { get; set; }
        public List<UserSystemPageSummaryModel> Children { get; set; }


    }
    public class UserSystemPageSummaryModel : SystemPageSummaryModel
    {
        public PageActionMode Mode { get; set; } = PageActionMode.FromGroup;
        public UserSystemPageSummaryModel()
        {

        }
        public UserSystemPageSummaryModel(SystemPage entity) : base(entity)
        {

        }
    }

    public class SystemPageModel : SystemPageSummaryModel
    {
        public SystemPageModel() : base()
        {

        }
        public SystemPageModel(SystemPage entity) : base(entity)
        {

        }
        public void FillEntity(SystemPage entity)
        {

        }
    }
}
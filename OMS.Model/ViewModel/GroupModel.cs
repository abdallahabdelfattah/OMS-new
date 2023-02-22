using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class GroupSummaryModel : BaseModel
    {
        public GroupSummaryModel()
        {}
        public GroupSummaryModel(Group entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            Description = entity.Description;
        }

        public string Description { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public class GroupModel : GroupSummaryModel
    {
        public List<long> ActionsIds { get; set; }
        public GroupModel() : base()
        {
            ActionsIds = new List<long>();
        }
        public GroupModel(Group entity) : base(entity)
        {
            ActionsIds = new List<long>();
            if (entity.Id != 0)
            {
                ActionsIds = entity.GroupPageActions.Select(e => e.PageActionId).ToList();
            }
        }

        public void FillEntity(Group entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
            entity.Description = Description;
            //using (GroupBL _BL = new GroupBL())
            //{
            //    _BL.DeleteAllGroupActionIds(entity.ID);
            //}
            //   entity.GroupPageActions.Clear();
            foreach (var item in ActionsIds)
            {
                entity.GroupPageActions.Add(new GroupPageAction() { GroupId = entity.Id, PageActionId = item });
            }
        }
    }
}
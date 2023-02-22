using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class NotificationSummaryModel : BaseModel
    {
        public NotificationSummaryModel()
        { }
        public NotificationSummaryModel(Notification entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Type = entity.Type;
            Description = entity.Description;
            CreationDate = entity.CreatedOn?.ToString("dd/MM/yyyy");
            ExpirDate = entity.ExpirDate;
            GroupName=entity.Group == null? "All": entity.Group.NameAr;
        }

        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public DateTime? ExpirDate { get; set; }
        
        public string GroupName { get; set; }
    }
    public class NotificationModel : NotificationSummaryModel
    {
        public long? GroupId { get; set; }
        public bool IsAdmin { get; set; }

        public List<NotificationResponseModel> NotificationResponses { get; set; } = new List<NotificationResponseModel>();
        public NotificationModel() : base()
        {
            CreationDate = DateTime.Now.ToString("dd/MM/yyyy");
            //IsAdmin = SessionRegistery.CurrentUser.GroupID == 1 ? true : false;
            //NotificationResponses 
        }
        public NotificationModel(Notification entity) : base(entity)
        {
            GroupId = entity.GroupId;
            //IsAdmin = SessionRegistery.CurrentUser.GroupID == 1 ? true : false;

            //if (SessionRegistery.CurrentUser.GroupID != 1)
            //{
            //    var list = entity.NotificationResponses.Where(r => r.ResponseByID == SessionRegistery.CurrentUser.ID);
            //    foreach (var item in list)
            //    {
            //        NotificationResponses.Add(new NotificationResponseModel() { Description = item.Description, ResponseByID = item.ResponseByID, ResponseName = item.User?.Name, ShortDescription = entity.Title, ResponseDate = item.Date?.ToString("dd/MM/yyyy") });/**/
            //    }
            //}
            //else
            //{
            //    foreach (var item in entity.NotificationResponses)
            //    {
            //        NotificationResponses.Add(new NotificationResponseModel() { Description = item.Description, ResponseByID = item.ResponseByID, ResponseName = item.User?.Name, ShortDescription = entity.Title, ResponseDate = item.Date?.ToString("dd/MM/yyyy") });/**/
            //    }
            //}
        }

        public void FillEntity(Notification entity)
        {
            entity.Title = Title;
            entity.Description = Description;
            entity.ExpirDate = ExpirDate;
            entity.GroupId = GroupId;

            //entity.ModifiedBy = SessionRegistery.CurrentUser.ID;
            entity.UpdatedOn = DateTime.Now;
            if (entity.Id == 0)
            {
                //entity.CreatedByID = SessionRegistery.CurrentUser.ID;
                entity.CreatedOn = DateTime.Now;
            }
        }
    }

    public class NotificationResponseModel
    {
        public long ID { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public long? ResponseByID { get; set; }
        public string ResponseName { get; set; }
        public string ResponseDate { get; set; }
    }
}
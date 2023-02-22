using OMS.DAL;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BLL.Repositories
{
    public partial class NotificationRepository : RepositoryBase<Notification>
    {
        public NotificationRepository(DbContext context)
            : base(context)
        {
        }
    }
    public partial class NotificationRepository : RepositoryBase<Notification>
    {
        //public PagedList<NotificationModel> Search(NotificationSearchViewModel sc)
        //{
        //    var query = this.DbSet.AsQueryable();
        //    if (sc.Id > 0)
        //    {
        //        query = query.Where(e => e.Id == sc.Id);
        //    }
        //    if (!string.IsNullOrEmpty(sc.Name))
        //    {
        //        query = query.Where(e => e.NameAr.Contains(sc.Name) || e.NameEn.Contains(sc.Name));
        //    }

        //    var result = query.Select(e => new NotificationModel(e)).GetPaged(
        //        l => l.Id,
        //        true,
        //        sc.PageIndex,
        //        10);

        //    return result;
        //}
        //public Notification MapViewModelToEntity(NotificationModel vm)
        //{
        //    if (vm != null)
        //        return new Notification
        //        {
        //            NameAr = vm.NameAr,
        //            NameEn = vm.NameEn,
        //        };
        //    return null;
        //}
        //public NotificationModel MapEntityToViewModel(Notification entity)
        //{
        //    if (entity != null)
        //        return new NotificationModel
        //        {
        //            NameAr = entity.NameAr,
        //            NameEn = entity.NameEn,
        //        };
        //    return null;
        //}
    }
}

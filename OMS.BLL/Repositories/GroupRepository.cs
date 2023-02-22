using OMS.BLL.Data;
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
    public partial class GroupRepository : RepositoryBase<Group>
    {
        public GroupRepository(DbContext context)
            : base(context)
        {
        }
    }
    public partial class GroupRepository : RepositoryBase<Group>
    {
        public PagedList<Group> Search(GroupSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.NameAr.Contains(sc.Name) || e.NameEn.Contains(sc.Name));
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }
        public Group MapViewModelToEntity(GroupModel vm)
        {
            if (vm != null)
                return new Group
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                };
            return null;
        }
        public GroupModel MapEntityToViewModel(Group entity)
        {
            if (entity != null)
                return new GroupModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                };
            return null;
        }
    }
}

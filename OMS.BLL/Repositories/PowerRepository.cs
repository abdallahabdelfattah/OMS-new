using OMS.BLL.Data;
using OMS.Model.ViewModel;
using OMS.DAL;
using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BLL.Repositories
{
    public partial class PowerRepository : RepositoryBase<Power>
    {
        public PowerRepository(DbContext context): base(context)
        {
        }

        
    }
    public partial class PowerRepository : RepositoryBase<Power>
    {
        public PagedList<Power> Search(PowerSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.Value.Contains(sc.Value));
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }
        public Power MapViewModelToEntity(PowerModel vm)
        {
            if (vm != null)
                return new Power
                {
                    Value= vm.Value
                    
                };
            return null;
        }
        public PowerModel MapEntityToViewModel(Power entity)
        {
            if (entity != null)
                return new PowerModel
                {
                  Value=entity.Value
                };
            return null;
        }
    }
}

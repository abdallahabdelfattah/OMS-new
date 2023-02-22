using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel.SC;
using Z.EntityFramework.Plus;

namespace OMS.BLL.Repositories
{
    public class SPHRepository : RepositoryBase<SPH>
    {
        public Dictionary<int, string> GetNames() => GetAllFromCache().ToDictionary(o => o.Id, o => o.Name);
        internal List<SPH> GetAllFromCache() => DbSet.AsNoTracking().ToList();
        public SPHRepository(DbContext context) : base(context)
        {
        }

        public PagedList<SPH> Search(SPHSearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();
            if (!string.IsNullOrEmpty(vm.Name))
                query = query.Where(s => s.Name.Contains(vm.Name));

            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                50);

            return result;
        }
    }
}

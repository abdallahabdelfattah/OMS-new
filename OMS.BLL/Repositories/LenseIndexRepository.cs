using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel.SC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BLL.Repositories
{
    public class LenseIndexRepository: RepositoryBase<LenseIndex>
    {
        public LenseIndexRepository(DbContext context) : base(context)
        {
        }
        public PagedList<LenseIndex> Search(LenseIndexSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (sc.Index.HasValue)
            {
                query = query.Where(e => e.Index == sc.Index);
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }
    }
}

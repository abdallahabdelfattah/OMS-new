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
    public class MultifocalDesignRepository : RepositoryBase<MultifocalDesign>
    {
        public MultifocalDesignRepository(DbContext context) : base(context)
        {
        }
        public PagedList<MultifocalDesign> Search(MultifocalDesignSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.Name.Contains(sc.Name.Trim()));
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

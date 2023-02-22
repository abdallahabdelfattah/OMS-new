using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;

namespace OMS.BLL.Repositories
{
    public class ColorCategoryRepository : RepositoryBase<DAL.DataAccess.ColorCategoy>
    {
        public ColorCategoryRepository(DbContext context) : base(context)
        {
        }
        public PagedList<DAL.DataAccess.ColorCategoy> Search(BaseSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.NameAr.Contains(sc.Name)|| e.NameAr.Contains(sc.Name));
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

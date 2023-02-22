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
    public class ModelRepository : RepositoryBase<DAL.DataAccess.Model>
    {
        public ModelRepository(DbContext context) : base(context)
        {
        }
        public PagedList<DAL.DataAccess.Model> Search(ModelSearchViewModel sc)
        {
            var query = this.DbSet.Include(a=>a.Brand).Include(a=>a.Brand.ProductCategory).AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.ModelName.Contains(sc.Name));
            }
            if (sc.CategoryId.HasValue)
            {
                query = query.Where(e => e.Brand.ProductCategoryId== sc.CategoryId);
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

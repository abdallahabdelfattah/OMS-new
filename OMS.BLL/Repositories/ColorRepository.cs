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
    public class ColorRepository : RepositoryBase<DAL.DataAccess.Color>
    {
        public ColorRepository(DbContext context) : base(context)
        {
        }
        public PagedList<DAL.DataAccess.Color> Search(ColorSearchViewModel sc)
        {
            var query = this.DbSet.Include(a=>a.ColorCategoy).AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.ColorName.Contains(sc.Name));
            }
            if (sc.BrandId.HasValue)
            {
                query = query.Where(e => e.Model.BrandId== sc.BrandId);
            }
            if (sc.CategoryId.HasValue)
            {
                query = query.Where(e => e.Model.Brand.ProductCategoryId == sc.CategoryId);
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

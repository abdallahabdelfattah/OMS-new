using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class DiseaseRepository : RepositoryBase<Disease>
    {
        public DiseaseRepository(DbContext context): base(context)
        {
        }
        
    }
    public partial class DiseaseRepository : RepositoryBase<Disease>
    {
        public PagedList<Disease> Search(DiseaseSearchViewModel sc)
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

            var result = query
                .GetPaged(l => l.CreatedOn, true, sc.PageIndex, sc.RowCount);

            return result;
        }
    }
}

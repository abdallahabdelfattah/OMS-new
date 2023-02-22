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
    public partial class RegionRepository : RepositoryBase<Region>
    {
        public RegionRepository(DbContext context) : base(context)
        {
        }
    }
    public partial class RegionRepository : RepositoryBase<Region>
    {
        public PagedList<Region> Search(RegionSearchViewModel sc)
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

        public Region MapViewModelToEntity(RegionModel vm)
        {
            if (vm != null)
                return new Region
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                };
            return null;
        }

        public RegionModel MapEntityToViewModel(Region entity)
        {
            if (entity != null)
                return new RegionModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                };
            return null;
        }
    }
}

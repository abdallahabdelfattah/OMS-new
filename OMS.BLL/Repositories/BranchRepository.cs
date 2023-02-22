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
using Commons.Framework.Extensions;

namespace OMS.BLL.Repositories
{
    public partial class BranchRepository : RepositoryBase<Branch>
    {
        public BranchRepository(DbContext context) : base(context)
        {
        }
    }
    public partial class BranchRepository : RepositoryBase<Branch>
    {
        public PagedList<Branch> Search(BranchSearchViewModel sc)
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
            if (!sc.CountryId.IsNullOrDefault<long>())
            {
                query = query.Include(a => a.Region.City.Country).Where(e => e.Region.City.CountryId ==sc.CountryId);
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }

        public Branch MapViewModelToEntity(BranchModel vm)
        {
            if (vm != null)
                return new Branch
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                    RegionId = vm.RegionId
                };
            return null;
        }

        public BranchModel MapEntityToViewModel(Branch entity)
        {
            if (entity != null)
                return new BranchModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    RegionId = entity.RegionId,
                    RegionName=entity.Region.NameAr
                };
            return null;
        }
    }
}

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
    public partial class CityRepository : RepositoryBase<City>
    {
        public CityRepository(DbContext context): base(context)
        {
        }

        
    }
    public partial class CityRepository : RepositoryBase<City>
    {
        public PagedList<City> Search(CitySearchViewModel sc)
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
        public City MapViewModelToEntity(CityModel vm)
        {
            if (vm != null)
                return new City
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                    CountryId = vm.CountryId,
                };
            return null;
        }
        public CityModel MapEntityToViewModel(City entity)
        {
            if (entity != null)
                return new CityModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    CountryId = entity.CountryId,
                };
            return null;
        }
    }
}

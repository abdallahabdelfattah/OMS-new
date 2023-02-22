using OMS.BLL.Data;
using OMS.Model.ViewModel;
using OMS.DAL.DataAccess;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class CountryRepository : RepositoryBase<Country>
    {
        public CountryRepository(DbContext context) : base(context)
        {
        }

    }
    public partial class CountryRepository : RepositoryBase<Country>
    {
        public PagedList<Country> Search(CountrySearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);


            var result = query.GetPaged(l => l.Id,true,vm.PageIndex, vm.RowCount);

            return result;
        }
        public Country MapViewModelToEntity(CountryModel countryViewModel)
        {
            if (countryViewModel != null)
                return new Country
                {
                    Id = countryViewModel.Id,
                    NameAr = countryViewModel.NameAr,
                    NameEn = countryViewModel.NameEn
                };
            return null;
        }
        public CountryModel MapEntityToViewModel(Country countryEntity)
        {
            if (countryEntity != null)
                return new CountryModel
                {
                    NameAr = countryEntity.NameAr,
                    NameEn = countryEntity.NameEn
                };
            return null;
        }
    }
}

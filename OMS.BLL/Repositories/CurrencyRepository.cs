using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class CurrencyRepository : RepositoryBase<Currency>
    {
        public CurrencyRepository(DbContext context): base(context)
        {
        }
        
    }
    public partial class CurrencyRepository : RepositoryBase<Currency>
    {
        public PagedList<Currency> Search(CurrencySearchViewModel sc)
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
                l => l.CreatedOn,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }

        public Currency MapViewModelToEntity(CurrencyModel vm)
        {
            if (vm != null)
                return new Currency
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                    IsDefault = vm.IsDefault,
                    ExchangeRate = vm.ExchangeRate,
                };
            return null;
        }
        
        public CurrencyModel MapEntityToViewModel(Currency entity)
        {
            if (entity != null)
                return new CurrencyModel(entity);  
            return null;
        }
    }
}

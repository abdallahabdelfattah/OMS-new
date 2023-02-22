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
    public partial class WarehouseRepository : RepositoryBase<Warehouse>
    {
        public WarehouseRepository(DbContext context): base(context)
        {
        }
    }
    public partial class WarehouseRepository : RepositoryBase<Warehouse>
    {
        public PagedList<Warehouse> Search(WarehouseSearchViewModel sc)
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

        public Warehouse MapViewModelToEntity(WarehouseModel vm)
        {
            if (vm != null)
                return new Warehouse
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                    Address = vm.Address,
                };
            return null;
        }

        public WarehouseModel MapEntityToViewModel(Warehouse entity)
        {
            if (entity != null)
                return new WarehouseModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    Address = entity.Address,
                };
            return null;
        }
    }
}

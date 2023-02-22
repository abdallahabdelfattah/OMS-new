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
    public partial class WarehouseProductsViewRepository : RepositoryBase<WarehouseProductsView>
    {
        public WarehouseProductsViewRepository(DbContext context) : base(context)
        {
        }
    }
    public partial class WarehouseProductsViewRepository : RepositoryBase<WarehouseProductsView>
    {
        public List<LookupModel> GetByWarehouse(long warehouseId, string lang)
        {
            return this.DbSet.Where(e=>e.WarehouseID == warehouseId)
                .Select(c => new LookupModel() { Id = c.ProductID, Name = (lang =="ar"?c.ProductAr:c.ProductEn) + "(" + c.total + ")" })
                .ToList();
        }
    }
}

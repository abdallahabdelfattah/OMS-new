using System;
using System.Data.Entity;
using System.Linq;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel.SC;

namespace OMS.BLL.Repositories
{
    public class PurchaseReportViewRepository : RepositoryBase<PurchaseReportView>
    {
        public PurchaseReportViewRepository(DbContext context) : base(context)
        {
        }

        public PagedList<PurchaseReportView> Search(PurchaseReportViewSearchViewModel searchModel)
        {
            var query = DbSet.AsQueryable();
            if(searchModel.FromDate.HasValue)
                query = query.Where(q => q.CreatedOn >= searchModel.FromDate);
            if(searchModel.ToDate.HasValue)
                query = query.Where(q => q.CreatedOn <= searchModel.ToDate);

            if (searchModel.SupplierId.HasValue)
                query = query.Where(q => q.SupplierId == searchModel.SupplierId);
            if (searchModel.WarehouseId.HasValue)
                query = query.Where(q => q.WarehouseId== searchModel.WarehouseId);
            if (!string.IsNullOrEmpty(searchModel.Name))
                query = query.Where(q => q.productNameAr.Contains(searchModel.Name) || q.productNameEn.Contains(searchModel.Name));
            if (!string.IsNullOrEmpty(searchModel.ProductCode))
                query = query.Where(q => q.ProductCode.Contains(searchModel.ProductCode));
                
            var result = query.GetPaged(
                l => l.CreatedOn,
                true,
                searchModel.PageIndex,
                searchModel.RowCount);

            return result;
        }
    }
}

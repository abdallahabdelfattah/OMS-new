using System;
using System.Data.Entity;
using System.Linq;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;

namespace OMS.BLL.Repositories
{
    public class SalesTrackingReportViewRepository : RepositoryBase<SalesTrackingReportView>
    {
        public SalesTrackingReportViewRepository(DbContext context) : base(context)
        {
        }

        public PagedList<SalesTrackingReportView> Search(SalesTrackingReportViewSearchViewModel searchModel)
        {
            var query = DbSet.AsQueryable();
            if(searchModel.FromDate.HasValue)
                query = query.Where(q => q.CreatedOn >= searchModel.FromDate);
            if(searchModel.ToDate.HasValue)
                query = query.Where(q => q.CreatedOn <= searchModel.ToDate);
  
            if (searchModel.WarehouseId.HasValue)
                query = query.Where(q => q.WarehouseId== searchModel.WarehouseId);   
            var result = query.GetPaged(
                l => l.CreatedOn,
                true,
                searchModel.PageIndex,
                searchModel.RowCount);

            return result;
        }
    }
}

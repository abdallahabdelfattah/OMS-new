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
    public partial class WarehouseTransactionRepository : RepositoryBase<WarehouseTransaction>
    {
        public WarehouseTransactionRepository(DbContext context): base(context)
        {
        }
    }
    public partial class WarehouseTransactionRepository : RepositoryBase<WarehouseTransaction>
    {
        public PagedList<WarehouseTransaction> Search(WarehouseTransactionSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.EmployeeName))
            {
                query = query.Where(e => e.EmployeeName.Contains(sc.EmployeeName));
            }
            if (!string.IsNullOrEmpty(sc.DocNo))
            {
                query = query.Where(e => e.DocNo == sc.DocNo);
            }
            if (sc.WarehouseId.HasValue)
            {
                query = query.Where(e => e.WarehouseId == sc.WarehouseId);
            }
            if (sc.FromDate.HasValue)
            {
                query = query.Where(e => e.Date >= sc.FromDate);
            }
            if (sc.ToDate.HasValue)
            {
                query = query.Where(e => e.Date <= sc.ToDate);
            }
            if (sc.TransactionType.HasValue)
            {
                query = query.Where(e => e.TransactionType == sc.TransactionType);
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }

        public string GetDocumentNumber()
        {
            var lastEntity = this.DbSet.ToList().LastOrDefault();
            string docNo = lastEntity?.DocNo;
            if (string.IsNullOrEmpty(docNo))
            {
                return "000001";
            }
            else
            {
                return String.Format("{0:D6}", (int.Parse(docNo) + 1));
            }
        }
    }
}

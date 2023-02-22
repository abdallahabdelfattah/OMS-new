using OMS.BLL.Data;
using OMS.DAL;
using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BLL.Repositories
{
    public partial class SalesTransactionRepository : RepositoryBase<SalesTransaction>
    {
        public SalesTransactionRepository(DbContext context) : base(context)
        {
        }
    }
    public partial class SalesTransactionRepository : RepositoryBase<SalesTransaction>
    {
        public PagedList<SalesTransaction> Search(SalesTransactionSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (sc.CustomerId.HasValue)
            {
                query = query.Where(e => e.CustomerId == sc.CustomerId);
            }
            if (!string.IsNullOrEmpty(sc.CustomerName))
            {
                query = query.Where(e => e.Customer.NameAr.Contains(sc.CustomerName) || e.Customer.NameAr.Contains(sc.CustomerName));
            }
            if (!string.IsNullOrEmpty(sc.DocNo))
            {
                query = query.Where(e => e.DocNo == sc.DocNo);
            }
            if (!string.IsNullOrEmpty(sc.SalesAgentName))
            {
                query = query.Where(e => e.SalesAgentName.Contains(sc.SalesAgentName));
            }
            if (sc.FromDate.HasValue)
            {
                query = query.Where(e => e.Date >= sc.FromDate);
            }
            if (sc.ToDate.HasValue)
            {
                query = query.Where(e => e.Date <= sc.ToDate);
            }
            if (sc.IsDelivered.HasValue)
            {
                query = query.Where(e => (sc.IsDelivered.Value ? e.DeliveryDate.HasValue : e.DeliveryDate == null));
            }
            if (sc.Type.HasValue)
            {
                query = query.Where(e => e.Type == sc.Type);
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }

        public List<LookupModel> GetCustomerInvoices(long customerId)
        {
            return this.DbSet.Where(e => e.CustomerId == customerId && e.IsDelivered!=false && e.Type == (byte)SalesTransactionEnum.SalesInvoice)
                .Select(e => new LookupModel
                {
                    Id = e.Id,
                    Name = e.DocNo
                }).ToList();
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

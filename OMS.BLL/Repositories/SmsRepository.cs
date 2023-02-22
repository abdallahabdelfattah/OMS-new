using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel.SC;

namespace OMS.BLL.Repositories
{
    public class SmsRepository : RepositoryBase<SMSs>
    {
        public SmsRepository(DbContext context) : base(context)
        {
        }

        public PagedList<SMSs> Search(SmsSearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();

            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                50);

            return result;
        }
    }
}

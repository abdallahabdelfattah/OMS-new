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
    public partial class CLSPAndSolutionTypeRepository : RepositoryBase<CLSPAndSolutionType>
    {
        public CLSPAndSolutionTypeRepository(DbContext context): base(context)
        {
        }

        
    }
}

using OMS.Model.SystemEnums;
using OMS.DAL;
using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OMS.Model.ViewModel;
using OMS.BLL.Data;

namespace OMS.BLL.Repositories
{
    public partial class SystemConfigurationRepository : RepositoryBase<SystemConfiguration>
    {
        public SystemConfigurationRepository(DbContext context)
            : base(context)
        {
        }
    }
    public partial class SystemConfigurationRepository : RepositoryBase<SystemConfiguration>
    {
        
    }
}

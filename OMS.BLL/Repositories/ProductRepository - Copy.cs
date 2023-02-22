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
    public partial class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(DbContext context)
            : base(context)
        {
        }
    }
    public partial class ProductRepository : RepositoryBase<Product>
    {
    }
}

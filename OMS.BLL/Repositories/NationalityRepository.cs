using OMS.BLL.Data;
using OMS.Model.ViewModel;
using OMS.DAL.DataAccess;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class NationalityRepository : RepositoryBase<Nationality>
    {
        public NationalityRepository(DbContext context) : base(context)
        {
        }

    }
    public partial class NationalityRepository : RepositoryBase<Nationality>
    {
        
    }
}

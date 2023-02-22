using OMS.DAL.DataAccess;
using System.Data.Entity;

namespace OMS.BLL.Repositories
{
    public partial class MedicalLensDtailsTypeRepository : RepositoryBase<MedicalLensDtailsType>
    {
        public MedicalLensDtailsTypeRepository(DbContext context)
            : base(context)
        {
        }
    }
    
}
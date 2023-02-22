using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class ExaminationRepository : RepositoryBase<Examination>
    {
        public ExaminationRepository(DbContext context): base(context)
        {
        }
        
    }
    public partial class ExaminationRepository : RepositoryBase<Examination>
    {
        public PagedList<Examination> Search(ExaminationSearchViewModel sc)
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

            if (sc.DoctorId.HasValue)
            {
                query = query.Where(e => e.DoctorId == sc.DoctorId);
            }

            var result = query
                .GetPaged(l => l.CreatedOn, true, sc.PageIndex, sc.RowCount);

            return result;
        }
        public Examination MapViewModelToEntity(ExaminationModel model)
        {
            if (model != null)
                return new Examination()
                {
                    Id = model.Id,
                    CustomerId = model.CustomerId,
                    DoctorId = model.DoctorId,
                    Date= model.Date,
                    ADDValue= model.ADDValue,
                    RightCYL = model.RightCYL,
                    LeftCYL = model.LeftCYL,
                    RightAXIS= model.RightAXIS,
                    LeftAXIS = model.LeftAXIS,
                    RightSPH= model.RightSPH,
                    LeftSPH = model.LeftSPH,
                    IPD= model.IPD,
                    RightCL= model.RightCL,
                    LeftCL= model.LeftCL,
                };
            return null;
        }
    }
}

using OMS.BLL.Data;
using OMS.Model.ViewModel;
using OMS.DAL.DataAccess;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class JobRepository : RepositoryBase<Job>
    {
        public JobRepository(DbContext context) : base(context)
        {
        }

    }
    public partial class JobRepository : RepositoryBase<Job>
    {
        public PagedList<Job> Search(JobSearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);


            var result = query.GetPaged(l => l.Id,true,vm.PageIndex, vm.RowCount);

            return result;
        }
        public Job MapViewModelToEntity(JobModel JobViewModel)
        {
            if (JobViewModel != null)
                return new Job
                {
                    Id = JobViewModel.Id,
                    NameAr = JobViewModel.NameAr,
                    NameEn = JobViewModel.NameEn
                };
            return null;
        }
        public JobModel MapEntityToViewModel(Job JobEntity)
        {
            if (JobEntity != null)
                return new JobModel
                {
                    NameAr = JobEntity.NameAr,
                    NameEn = JobEntity.NameEn
                };
            return null;
        }
    }
}

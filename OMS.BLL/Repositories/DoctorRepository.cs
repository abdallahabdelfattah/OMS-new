using OMS.BLL.Data;
using OMS.Model.ViewModel;
using OMS.DAL.DataAccess;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class DoctorRepository : RepositoryBase<Doctor>
    {
        public DoctorRepository(DbContext context)
            : base(context)
        {
        }

    }
    public partial class DoctorRepository : RepositoryBase<Doctor>
    {
        public PagedList<Doctor> Search(DoctorSearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);
            if (!string.IsNullOrEmpty(vm.Name))
                query = query.Where(c => c.NameAr.Contains(vm.Name) || c.NameEn.Contains(vm.Name));

            if (!string.IsNullOrEmpty(vm.Mobile))
                query = query.Where(c => c.Mobile == vm.Mobile);

            if (!string.IsNullOrEmpty(vm.Phone))
                query = query.Where(c => c.Mobile == vm.Phone);


            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                50);

            return result;
        }
        public Doctor MapViewModelToEntity(DoctorViewModel doctorViewModel)
        {
            if (doctorViewModel != null)
                return new Doctor
                {
                    Id = doctorViewModel.Id,
                    NameAr = doctorViewModel.NameAr,
                    NameEn = doctorViewModel.NameEn,
                    Mobile = doctorViewModel.Mobile,
                    Phone = doctorViewModel.Phone,
                    Address = doctorViewModel.Address,
                    CityId = doctorViewModel.CityId,
                    RegionId = doctorViewModel.RegionId,
                    CreatedOn = System.DateTime.Now,
                    CreatedBy = "Current User"//TODO change that with the current user
                };
            return null;
        }
        public DoctorViewModel MapEntityToViewModel(Doctor entity)
        {
            if (entity != null)
                return new DoctorViewModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                };
            return null;
        }
    }
}

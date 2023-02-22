using OMS.BLL.Data;
using OMS.DAL;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace OMS.BLL.Repositories
{
    public partial class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        public PagedList<Customer> Search(CustomerSearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);

            if (!string.IsNullOrEmpty(vm.Name))
                query = query.Where(c => c.NameAr.Contains(vm.Name) || c.NameEn.Contains(vm.Name));


            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                vm.RowCount);

            return result;
        }

        public async Task<Customer> InsertOrUpdate(CustomerModel customer)
        {
            var savedEntity = MapViewModelToEntity(customer);
            if (customer.NewExamination != null)
            {
                var examinationRepo = new ExaminationRepository(this.Context);
                var examination = examinationRepo.MapViewModelToEntity(customer.NewExamination);
                if (savedEntity.Examinations == null)
                {
                    savedEntity.Examinations = new List<Examination>();
                }
                savedEntity.Examinations.Add(examination);

            }
            AddOrUpdate(savedEntity, a => a.Id == savedEntity.Id);
            Context.SaveChanges();
            return savedEntity;
        }
        public Customer MapViewModelToEntity(CustomerModel model)
        {
            if (model != null)
                return new Customer
                {
                    Id = model.Id,
                    NameAr = model.NameAr,
                    Address = model.Address,
                    Code = model.Code,
                    Email = model.Email,
                    IsActive = model.IsEnabled,
                    Mobile = model.Mobile,
                    Phone = model.Phone,
                    NameEn = model.NameAr,
                    Notes = model.Notes,
                    CreationFileDate = model.CreationFileDate,
                };
            return null;
        }
        public CustomerModel MapEntityToViewModel(Customer entity)
        {
            if (entity != null)
                return new CustomerModel
                {
                    Id = entity.Id,
                    NameAr = entity.NameEn,
                    Address = entity.Address,
                    Code = entity.Code,
                    Email = entity.Email,
                    IsEnabled = entity.IsActive,
                    Mobile = entity.Mobile,
                    Phone = entity.Phone
                };
            return null;
        }

        public string GetDocumentNumber()
        {
            var lastEntity = this.DbSet.ToList().LastOrDefault();
            string docNo = lastEntity?.Code;
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

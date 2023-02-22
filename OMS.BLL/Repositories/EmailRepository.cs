using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;
using OMS.Resources;

namespace OMS.BLL.Repositories
{
    public class EmailRepository : RepositoryBase<Email>
    {
        public EmailRepository(DbContext context) : base(context)
        {
        }

        public PagedList<Email> Search(EmailSearchViewModel vm)
        {
            var query = this.DbSet.AsQueryable();

            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                50);

            return result;
        }

        public EmailViewModel AddCustomerToReceiversList(EmailViewModel emailViewModel)
        {
            using (var uow = new OMSUoW())
            {
                if (emailViewModel.EmailReceiversModel == null)
                    emailViewModel.EmailReceiversModel = new List<dynamic>();
                emailViewModel.EmailReceiversModel.AddRange(new List<dynamic> { uow.Customers.Get().Select(c => new { Name = c.NameEn, c.Email }).ToList() });
            }

            return emailViewModel;
        }
        //public List<dynamic> AddDoctorsToReceiversList(EmailViewModel emailViewModel)
        //{
        //    using (var uow = new OMSUoW())
        //    {
        //        return new List<dynamic> {uow.Doctors.Get().Select(c=> new {NameAr = c.NameAr,c.NameEn,c.email}).ToList()};
        //    }
        //}

        public EmailViewModel AddEmployeesToReceiversList(EmailViewModel emailViewModel)
        {
            using (var uow = new OMSUoW())
            {
                if (emailViewModel.EmailReceiversModel == null)
                    emailViewModel.EmailReceiversModel = new List<dynamic>();
                emailViewModel.EmailReceiversModel.AddRange(new List<dynamic> { uow.Users.Get().Select(c => new { Name = c.NameEn, c.Email }).ToList() });
            }

            return emailViewModel;
        }
        public EmailViewModel AddSuppliersToReceiversList(EmailViewModel emailViewModel)
        {
            using (var uow = new OMSUoW())
            {
                if (emailViewModel.EmailReceiversModel == null)
                    emailViewModel.EmailReceiversModel = new List<dynamic>();
                emailViewModel.EmailReceiversModel.AddRange(new List<dynamic> { uow.Suppliers.Get().Select(c => new { Name = c.NameEn, c.Email }).ToList() });
            }

            return emailViewModel;
        }

        public object AddDoctorsToReceiversList(EmailViewModel model)
        {
            throw new NotImplementedException();
        }

        public EmailViewModel FillEmailReceiversList(EmailViewModel emailViewModel)
        {
            using (var uow = new OMSUoW())
            {
                if (emailViewModel.EmailReceiversModel == null)
                    emailViewModel.EmailReceiversModel = new List<dynamic>();
                var customerList = uow.Customers.Get().Select(c => new { Name = c.NameEn, c.Email }).ToList();
                foreach (var customer in customerList)
                {
                    emailViewModel.EmailReceiversModel.Add(customer);
                }
                var employeeList = uow.Users.Get().Select(c => new { Name = c.NameEn, c.Email }).ToList();
                foreach (var employee in employeeList)
                {
                    emailViewModel.EmailReceiversModel.Add(employee);
                }

                var suppliersList = uow.Suppliers.Get().Select(c => new { Name = c.NameEn, c.Email }).ToList();
                foreach (var supplier in suppliersList)
                {
                    emailViewModel.EmailReceiversModel.Add(supplier);
                }

            }

            return emailViewModel;
        }
    }
}

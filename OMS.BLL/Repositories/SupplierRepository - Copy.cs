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

namespace OMS.BLL.Repositories
{
    public partial class SupplierRepository : RepositoryBase<Supplier>
    {
        public SupplierRepository(DbContext context)
            : base(context)
        {
        }
    }
    public partial class SupplierRepository : RepositoryBase<Supplier>
    {
        public PagedList<Supplier> Search(SupplierSearchViewModel sc)
        {
            var query = this.DbSet.AsQueryable();
            if (sc.Id > 0)
            {
                query = query.Where(e => e.Id == sc.Id);
            }
            if (!string.IsNullOrEmpty(sc.Name))
            {
                query = query.Where(e => e.NameAr.Contains(sc.Name) || e.NameEn.Contains(sc.Name));
            }

            var result = query.GetPaged(
                l => l.Id,
                true,
                sc.PageIndex,
                sc.RowCount);

            return result;
        }

        public Supplier MapViewModelToEntity(SupplierModel vm)
        {
            if (vm != null)
                return new Supplier
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                    Code = vm.Code,
                    Address = vm.Address,
                    RegionId = vm.RegionId,
                    Email = vm.Email,
                    Mobile = vm.Mobile,
                    BankAccount = vm.BankAccount,
                    BankName = vm.BankName,
                    CRNo = vm.CRNo,
                    FaxNo = vm.FaxNo,
                    Phone = vm.Phone,
                };
            return null;
        }

        public SupplierModel MapEntityToViewModel(Supplier entity)
        {
            if (entity != null)
                return new SupplierModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    Code = entity.Code,
                    Address = entity.Address,
                    RegionId = entity.RegionId,
                    Email = entity.Email,
                    Mobile = entity.Mobile,
                    BankAccount = entity.BankAccount,
                    BankName = entity.BankName,
                    CRNo = entity.CRNo,
                    FaxNo = entity.FaxNo,
                    Phone = entity.Phone,
                };
            return null;
        }
    }
}

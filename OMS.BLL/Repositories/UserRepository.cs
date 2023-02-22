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
    public partial class UserRepository : RepositoryBase<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
    public partial class UserRepository : RepositoryBase<User>
    {
        public PagedList<User> Search(UserSearchViewModel sc)
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
        public User MapViewModelToEntity(UserModel vm)
        {
            if (vm != null)
                return new User
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                };
            return null;
        }
        public UserModel MapEntityToViewModel(User entity)
        {
            if (entity != null)
                return new UserModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
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

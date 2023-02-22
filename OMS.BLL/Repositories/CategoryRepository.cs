using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;
using System.Data.Entity;
using System.Linq;

namespace OMS.BLL.Repositories
{
    public partial class CategoryRepository : RepositoryBase<ProductCategory>
    {
        public CategoryRepository(DbContext context): base(context)
        {
        }
        
    }
    public partial class CategoryRepository : RepositoryBase<ProductCategory>
    {
        public PagedList<ProductCategory> Search(CategorySearchViewModel sc)
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

            var result = query
                .GetPaged(l => l.Id, true, sc.PageIndex, sc.RowCount);

            return result;
        }
        public ProductCategory MapViewModelToEntity(CategoryModel vm)
        {
            if (vm != null)
                return new ProductCategory
                {
                    NameAr = vm.NameAr,
                    NameEn = vm.NameEn,
                    ParentId=vm.ParentCategoryId
                };
            return null;
        }
        public CategoryModel MapEntityToViewModel(ProductCategory entity)
        {
            if (entity != null)
                return new CategoryModel
                {
                    NameAr = entity.NameAr,
                    NameEn = entity.NameEn,
                    ParentCategoryId=entity.ParentId
                };
            return null;
        }
    }
}

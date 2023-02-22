using OMS.DAL;
using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.Model.ViewModel;
using System.Web.Configuration;
using OMS.Model.SystemEnums;

namespace OMS.BLL.Repositories
{
    public partial class ProductRepository : RepositoryBase<Product>
    {
        private readonly CategoryRepository _categoryRepository;
        public ProductRepository(DbContext context)
            : base(context)
        {
            _categoryRepository = new CategoryRepository(context);
        }
    }

    public partial class ProductRepository : RepositoryBase<Product>
    {
        public PagedList<Product> Search(ProductSearchViewModel vm)
        {
            var cl = _categoryRepository.Get(a => a.NameEn.ToLower() == "cl").FirstOrDefault();
            var query = this.DbSet.Include(a=>a.Color).Where(a=>a.CategoryId!=cl.Id);

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);
            if (!string.IsNullOrEmpty(vm.Name))
                query = query.Where(c => c.NameAr.Contains(vm.Name) || c.NameEn.Contains(vm.Name));
            if (!string.IsNullOrEmpty(vm.Code))
                query = query.Where(c => c.Code.Contains(vm.Code));
            if (vm.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == vm.CategoryId);
            if (vm.SupplierId.HasValue)
                query = query.Where(c => c.SupplierId == vm.SupplierId);
            if (vm.BrandId.HasValue)
                query = query.Where(c => c.BrandId == vm.BrandId);
            if (vm.ColorCategoryId.HasValue)
                query = query.Where(c => c.Color.ColorCategoryId == vm.ColorCategoryId);
            if (vm.PowerId.HasValue)
                query = query.Where(c => c.PowerId == vm.PowerId);
            if (vm.UsePeriodId.HasValue)
                query = query.Where(c => c.UsePeriodId == vm.UsePeriodId);
            if (vm.ColorId.HasValue)
                query = query.Where(c => c.ColorId == vm.ColorId);

            query = query.Where(a => a.CategoryId != (int) ProductCategories.CL);
        query.Include(a => a.Model).Include(a => a.Brand);
            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                vm.RowCount);

            return result;
        }
        public PagedList<Product> CLSearch(ProductSearchViewModel vm)
        {

            var query = this.DbSet.Include(a => a.Color).Where(a=>a.CategoryId==(int)ProductCategories.CL);

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);
            if (!string.IsNullOrEmpty(vm.Name))
                query = query.Where(c => c.NameAr.Contains(vm.Name) || c.NameEn.Contains(vm.Name));
            if (!string.IsNullOrEmpty(vm.Code))
                query = query.Where(c => c.Code.Contains(vm.Code));
            
            if (vm.SupplierId.HasValue)
                query = query.Where(c => c.SupplierId == vm.SupplierId);
            if (vm.BrandId.HasValue)
                query = query.Where(c => c.BrandId == vm.BrandId);
            if (vm.ColorCategoryId.HasValue)
                query = query.Where(c => c.Color.ColorCategoryId == vm.ColorCategoryId);
            if (vm.PowerId.HasValue)
                query = query.Where(c => c.PowerId == vm.PowerId);
            if (vm.UsePeriodId.HasValue)
                query = query.Where(c => c.UsePeriodId == vm.UsePeriodId);
            if (vm.ColorId.HasValue)
                query = query.Where(c => c.ColorId == vm.ColorId);


            query.Include(a => a.Model).Include(a => a.Brand);
            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                vm.RowCount);

            return result;
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

        public Product MapViewModelToEntity(ProductViewModel model)
        {
            if (model != null)
                return new Product
                {
                    Id= model.Id,
                    NameAr = model.NameAr,
                    NameEn = model.NameEn,
                    Code = model.Code,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now,
                    MinQty = model.MinQty,
                    OfficialPrice = model.CategoryId,
                    Qty = model.Qty,
                    CategoryId = model.CategoryId,
                    SupplierId = model.SupplierId,
                    BrandId = model.BrandId,
                    MaterialId = model.MaterialId,
                    ColorId = model.ColorId,

                };
            else
                return new Product();
        }
        public async Task EditProductGroupSearch(EditProductGroupSearchViewModel vm, decimal newValue,
            EditProductColumnEnum column, OperationTypeEnum operation) 
        {
               var query = CreateProductFilter(vm);
                await EditProductPriceGroup(query, column, operation, newValue);
            
        }
        public PagedList<Product> ProductGroupSearch(EditProductGroupSearchViewModel vm)
        {
            var query = CreateProductFilter(vm);

            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                vm.RowCount);

            return result;
        }

        private IQueryable<Product> CreateProductFilter(EditProductGroupSearchViewModel vm)
        {
           var query= this.DbSet.AsQueryable();

            if (vm.Id != default(long))
                query = query.Where(c => c.Id == vm.Id);
            if (vm.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == vm.CategoryId);
            if (vm.SupplierId.HasValue)
                query = query.Where(c => c.SupplierId == vm.SupplierId);
            if (vm.BrandId.HasValue)
                query = query.Where(c => c.BrandId == vm.BrandId);
            if (vm.ColorId.HasValue)
                query = query.Where(c => c.ColorId == vm.ColorId);
            if (vm.ProductIdFrom.HasValue)
                query = query.Where(c => c.Id >= vm.ProductIdFrom);
            if (vm.ProductIdTo.HasValue)
                query = query.Where(c => c.Id <= vm.ProductIdTo);
            return query;
        }

        private decimal AddNewValue(decimal? oldValue,decimal newValue)
        {
            
            return oldValue.HasValue ? (oldValue.Value + newValue) : newValue; ;
        }
        private decimal SubtractNewValue(decimal? oldValue, decimal newValue)
        {
            return oldValue.HasValue ? (oldValue.Value - newValue) : newValue;
        }
        private decimal PlusPrecentageNewValue(decimal? oldValue, decimal newValue)
        {

            return oldValue.HasValue? oldValue.Value + (oldValue??0 * newValue) / 100:0; 
        } 
        private decimal SubtractPrecentageNewValue(decimal? oldValue, decimal newValue)
        {
            return oldValue.HasValue? oldValue.Value - (oldValue??0 * newValue) / 100:0;
        }
        private async Task EditProductPriceGroup(IQueryable<Product> products, EditProductColumnEnum column, OperationTypeEnum operation, decimal newValue)
        {
            if (column == EditProductColumnEnum.price)
            {
                switch (operation)
                {
                    case OperationTypeEnum.Plus:
                        {
                            //foreach (var item in products)
                            //{
                            //    item.OfficialPrice = AddNewValue(item.OfficialPrice, newValue);
                            //    Update(item);
                            //}
                             await products.ForEachAsync(a => a.OfficialPrice = AddNewValue(a.OfficialPrice, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    case OperationTypeEnum.Subtract:
                        {
                            await products.ForEachAsync(a => a.OfficialPrice = SubtractNewValue(a.OfficialPrice, newValue));
                            await this.Context.SaveChangesAsync();


                        }
                        break;
                    case OperationTypeEnum.PlusPercentage:
                        {
                            await products.ForEachAsync(a => a.OfficialPrice = PlusPrecentageNewValue(a.OfficialPrice, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    case OperationTypeEnum.SubtractPercentage:
                        {
                            await products.ForEachAsync(a => a.OfficialPrice = SubtractPrecentageNewValue(a.OfficialPrice, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    default:
                        break;
                }

            }
            else
            {
                switch (operation)
                {
                    case OperationTypeEnum.Plus:
                        {
                            await products.ForEachAsync(a => a.Qty = (int)AddNewValue(a.Qty, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    case OperationTypeEnum.Subtract:
                        {
                            await products.ForEachAsync(a => a.Qty = (int)SubtractNewValue(a.Qty, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    case OperationTypeEnum.PlusPercentage:
                        {
                            await products.ForEachAsync(a => a.Qty = (int)PlusPrecentageNewValue(a.Qty, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    case OperationTypeEnum.SubtractPercentage:
                        {
                            await products.ForEachAsync(a => a.Qty = (int)SubtractPrecentageNewValue(a.Qty, newValue));
                            await this.Context.SaveChangesAsync();

                        }
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
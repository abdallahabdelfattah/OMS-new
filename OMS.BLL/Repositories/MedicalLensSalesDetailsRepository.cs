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
using Commons.Framework.Extensions;

namespace OMS.BLL.Repositories
{
    public partial class MedicalLensSalesDetailsRepository : RepositoryBase<MedicalLensSalesDetail>
    {
        public MedicalLensSalesDetailsRepository(DbContext context)
            : base(context)
        {
        }
    }

    //public partial class MedicalLensSalesDetailsRepository : RepositoryBase<MedicalLensSalesDetail>
    //{
    //    public PagedList<MedicalLensSalesDetail> Search(MedicalLensMasterSearchViewModel vm)
    //    {
    //        var query = this.DbSet.AsQueryable();

    //        if (vm.Id != default(long))
    //            query = query.Where(c => c.Id == vm.Id);
    //        if (!vm.BrandId.IsNullOrDefault<long>())
    //            query = query.Where(c => c.BrandId == vm.BrandId);
    //        if (!vm.ColorId.IsNullOrDefault<long>())
    //            query = query.Where(c => c.ColorId == vm.ColorId);
    //        if (!vm.MaterialId.IsNullOrDefault<long>())
    //            query = query.Where(c => c.MaterialId == vm.MaterialId);
    //        if (!vm.LenseIndexId.IsNullOrDefault<long>())
    //            query = query.Where(c => c.LenseIndexId == vm.LenseIndexId);

    //        var result = query.GetPaged(
    //            l => l.Id,
    //            true,
    //            vm.PageIndex,
    //            vm.RowCount, p => p.Brand);

    //        return result;
    //    }

    //    public string GetDocumentNumber()
    //    {
    //        var lastEntity = this.DbSet.ToList().LastOrDefault();
    //        string docNo = lastEntity?.Code;
    //        if (string.IsNullOrEmpty(docNo))
    //        {
    //            return "000001";
    //        }
    //        else
    //        {
    //            return String.Format("{0:D6}", (int.Parse(docNo) + 1));
    //        }
    //    }

    //    public MedicalLensMaster MapViewModelToEntity(MedicalLensMasterViewModel model)
    //    {
    //        if (model != null)
    //            return new MedicalLensMaster
    //            {
    //                Id = model.Id,
    //                CreatedBy = "system",
    //                CreatedOn = DateTime.Now,
    //                MultifocalDesignId = model.MultifocalDesignId,
    //                CoatingDiagramId = model.CoatingDiagramId,
    //                LenseIndexId = model.LenseIndexId,
    //                MaterialId = model.MaterialId,
                    
    //            };
    //        else
    //            return new MedicalLensMaster();
    //    }
    //}
}
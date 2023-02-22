using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.ViewModel;

namespace OMS.BLL.Repositories
{
    public class PurchaseTransactionDetailsRepository: RepositoryBase<PurchaseTransactionDetail>
    {
    public PurchaseTransactionDetailsRepository(DbContext context)
        : base(context)
    {
    }
    public PagedList<PurchaseTransactionDetail> Search(PurchaseTransactionDetailsSearchViewModel vm)
    {
        var query = this.DbSet.AsQueryable();

        var result = query.GetPaged(
            l => l.Id,
            true,
            vm.PageIndex,
            50);

        return result;
    }

    public PurchaseTransactionDetail MapViewModelToEntity(PurchaseTransactionDetailsSummeryModel model)
    {
        var entity = new PurchaseTransactionDetail
        {
            Id = model.Id,
            ChangeRate = model.ChangeRate,
            OfficialUnitPrice = model.OfficialUnitPrice,
            CurrencyId= model.CurrencyId,
            ProductId = model.ProductId,
            PurchasePrice = model.PurchasePrice,
            PurchaseTransactionMasterID = model.PurchaseTransactionMasterId,
            Qty = model.Qty
        };
        return entity;
    }

    public List<PurchaseTransactionDetail> MapViewModelListToEntityList(List<PurchaseTransactionDetailsSummeryModel> modelDetails)
    {
        var purchaseDetailsList = new List<PurchaseTransactionDetail>();
        foreach (var item in modelDetails)
        {
            purchaseDetailsList.Add(new PurchaseTransactionDetail
            {
                Id = item.Id,
                ChangeRate = item.ChangeRate,
                CurrencyId = item.CurrencyId,
                OfficialUnitPrice = item.OfficialUnitPrice,
                ProductId = item.ProductId,
                PurchasePrice = item.PurchasePrice,
                PurchaseTransactionMasterID = item.PurchaseTransactionMasterId,
                Qty = item.Qty
            });
        }

        return purchaseDetailsList;
    }
    }
}

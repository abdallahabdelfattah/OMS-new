using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OMS.BLL.Data;
using OMS.DAL.DataAccess;
using OMS.Model.SystemEnums;
using OMS.Model.ViewModel;
using OMS.Model.ViewModel.SC;

namespace OMS.BLL.Repositories
{
    public partial class PurchaseTransactionMasterRepository : RepositoryBase<PurchaseTransactionMaster>
    {
        public PurchaseTransactionMasterRepository(DbContext context)
            : base(context)
        {
        }


        public PagedList<PurchaseTransactionMaster> Search(PurchaseTransactionMasterSearchViewModel vm)
            {
            var query = this.DbSet.AsQueryable();
            //if (EqualityComparer<>.Default.Equals(vm.InvoiceNo, default(T)))
            if (!string.IsNullOrEmpty(vm.InvoiceNo))
                query = query.Where(q => q.InvoiceNo == vm.InvoiceNo);
            if (!string.IsNullOrEmpty(vm.SupplierInvoiceNo))
                query = query.Where(q => q.SupplierInvoiceNo == vm.SupplierInvoiceNo);
            if (vm.InvoiceDate.HasValue)
                query = query.Where(q => q.InvoiceDate == vm.InvoiceDate);
            if (vm.SupplierInvoiceDate.HasValue)
                query = query.Where(q => q.SupplierInvoiceDate == vm.SupplierInvoiceDate);
            if (! string.IsNullOrEmpty(vm.SupplierId))
                query = query.Where(q => q.SupplierId == long.Parse(vm.SupplierId));
            if (vm.WarehouseId.HasValue)
                query = query.Where(q => q.WarehouseId == vm.WarehouseId);
            var result = query.GetPaged(
                l => l.Id,
                true,
                vm.PageIndex,
                50);

            return result;
        }
        public void AddNewPurchase(PurchaseTransactionMasterViewModel model)
        {
            //Load Repositories 
            var PurchaseTransactionDetailsRepo = new PurchaseTransactionDetailsRepository(this.Context);
            var ProductRepo = new ProductRepository(this.Context);

            var mappedEntity = MapViewModelToEntity(model);
            try
            {
                foreach (var detail in mappedEntity.PurchaseTransactionDetails)
                {
                    PurchaseTransactionDetailsRepo.AddOrUpdate(detail, d => d.Id == detail.Id);
                }
                //save purchase transaction master
                AddOrUpdate(mappedEntity, e => e.Id == model.Id);

                //add warehouse add and update product stock - إذن إضافة
                WarehouseTransaction warehouseTrans = new WarehouseTransaction
                {
                    CustomerId = model.SupplierId,
                    Date = model.InvoiceDate,
                    DocNo = model.InvoiceNo,
                    EmployeeName = model.WarehouseName,
                    WarehouseId = model.WarehouseId,
                    Notes = "Purchase invoice transaction no {invoice no}",
                    TransactionType = (byte)WarehouseTransactionEnum.Add,
                    WarehouseTransactionDetails = model.Details.Select(e => new WarehouseTransactionDetail
                    {
                        ProductId = e.ProductId,
                        Qty = e.Qty,
                    }).ToList()
                };
                
                foreach (var item in model.Details)
                {
                    ProductRepo.GetById(item.ProductId).Qty += item.Qty;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public PurchaseTransactionMaster MapViewModelToEntity(PurchaseTransactionMasterViewModel model)
        {
            var purchaseDetailsList = new List<PurchaseTransactionDetail>();
            if (model.Details != null)
            {
                var purchaseTransactionDetailsRepo = new PurchaseTransactionDetailsRepository(this.Context);
                purchaseDetailsList = purchaseTransactionDetailsRepo.MapViewModelListToEntityList(model.Details);
            }
            var entity = new PurchaseTransactionMaster
            {
                Id = model.Id,
                FeesAmount = model.OtherFees + model.TaxFees,
                InvoiceDate = model.InvoiceDate,
                InvoiceAmount = model.InvoiceAmount,
                InvoiceNo = model.InvoiceNo,
                Status = model.Status,
                Notes = model.Notes,
                OtherFees = model.OtherFees,
                PurchaseAgentName = model.PurchaseAgentName,
                SupplierId = model.SupplierId,
                TaxFees = model.TaxFees,
                SupplierInvoiceDate = model.SupplierInvoiceDate,
                SupplierInvoiceNo = model.SupplierInvoiceNo,
                WarehouseId = model.WarehouseId,
                PurchaseTransactionDetails = purchaseDetailsList
            };
            return entity;
        }

        public string GetDocumentNumber()
        {
            var lastEntity = this.DbSet.ToList().LastOrDefault();
            string docNo = lastEntity?.InvoiceNo;
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

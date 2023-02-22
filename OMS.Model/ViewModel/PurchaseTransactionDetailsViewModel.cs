using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class PurchaseTransactionDetailsSummeryModel : BaseModel
    {
        public PurchaseTransactionDetailsSummeryModel()
        {

        }
        public PurchaseTransactionDetailsSummeryModel(PurchaseTransactionDetail entity)
        {
            ChangeRate = entity.ChangeRate;
            CurrencyId = entity.CurrencyId;
            OfficialUnitPrice = entity.OfficialUnitPrice;
            ProductId = entity.ProductId;
            ProductName = entity.Product?.NameEn;
            PurchasePrice = entity.PurchasePrice;
            Qty = entity.Qty;
        }

        public string ProductName { get; set; }
        public Product Product { get; set; }
        public long? MedicalLensId { get; set; }

        public int PageIndex { get; set; }
        public int RowCount { get; set; }
        public int? Qty { get; set; }
        public int SPHId { get; set; }
        public int CYLId { get; set; }

        public decimal? PurchasePrice { get; set; }

        public long? PurchaseTransactionMasterId { get; set; }
        public long? ProductId { get; set; }

        public decimal? OfficialUnitPrice { get; set; }

        public long? CurrencyId { get; set; }

        public decimal? ChangeRate { get; set; }
        public double? MedicalLensPrice { get; set; }
    }
    
}

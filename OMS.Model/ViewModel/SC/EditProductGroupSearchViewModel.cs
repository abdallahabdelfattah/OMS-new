using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class EditProductGroupSearchViewModel : BaseSearchViewModel
    {
        public long? CategoryId { get; set; }
        public long? SupplierId { get; set; }
        public long? BrandId { get; set; }
        public long? ColorId { get; set; }
        public long? ModelId { get; set; }
        public long? ProductIdFrom { get; set; }
        public long? ProductIdTo { get; set; }
        public decimal? OfficialPriceFrom { get; set; }
        public decimal? OfficialPriceTo { get; set; }
    }
}

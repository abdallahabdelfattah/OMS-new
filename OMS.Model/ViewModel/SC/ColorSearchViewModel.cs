using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class ColorSearchViewModel : BaseSearchViewModel
    {
        public long? BrandId { get; set; }
        public long? CategoryId { get; set; }
    }
}

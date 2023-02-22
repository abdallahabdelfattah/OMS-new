using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class SystemPageSearchViewModel : BaseSearchViewModel
    {
        public bool? GetAll { get; set; }
        public bool? HasActions { get; set; }
        public byte? Type { get; set; }
        public string NameNotEqual { get; set; }
    }
}

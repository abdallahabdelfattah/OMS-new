using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class BaseSearchViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int PageIndex { get; set; }
        public int RowCount { get; set; }

        public string SortProp { get; set; }
        public string SortDir { get; set; }

        public BaseSearchViewModel()
        {
            Id = 0;
            PageIndex = 1;
            RowCount = 20;
        }

    }
}

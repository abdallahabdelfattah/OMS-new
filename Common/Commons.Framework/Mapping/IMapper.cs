using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.Mapping
{
    public interface IMapper<T>
    {
        List<T> Map(DataTable table);
    }
}

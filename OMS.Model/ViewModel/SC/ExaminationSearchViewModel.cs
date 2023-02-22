using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class ExaminationSearchViewModel : BaseSearchViewModel
    {
        public long? CustomerId { get; set; }
        public long? DoctorId { get; set; }
    }
}

using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Model.ViewModel
{
    public class MedicalLenseReportModel
    {
        public int? SphFrom { get; set; }
        public int? SphTo { get; set; }
        public int? CylFrom { get; set; }
        public int? CylTo { get; set; }
        public List<SPH> SPHList { get; set; }
        public List<CYL> CYLList{ get; set; }
        public List<MedicalLensMaster> MedicalLensList { get; set; }
    }
}

using System.Collections.Generic;
using OMS.DAL.DataAccess;

namespace OMS.Model.ViewModel
{
    public class MedicalLensDetailsListVM
    {
        //TODO Change MedicalLensDetailsQty to MedicalLensDetails to be one global table for purchase and sell and qty
        public List<MedicalLensDetail> MedicalLensDetailsList { get; set; }
       //public int MedicalLensMasterId { get; set; }
       // public int MedicalLensDetailsTypeId { get; set; }
       
    }


}
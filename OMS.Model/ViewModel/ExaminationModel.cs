using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class ExaminationSummaryModel : BaseModel
    {
        public ExaminationSummaryModel()
        {}
        public ExaminationSummaryModel(Examination entity)
        {
            Id = entity.Id;
            DoctorName = entity.Doctor?.NameAr;
            StrDate = entity.Date.HasValue? entity.Date.Value.ToString("dd/MM/yyyy"):"";
            RightSPH = entity.RightSPH;
            RightCYL = entity.RightCYL;
            RightAXIS = entity.RightAXIS;
            RightCL = entity.RightCL;
            LeftSPH = entity.LeftSPH;
            LeftCYL = entity.LeftCYL;
            LeftAXIS = entity.LeftAXIS;
            LeftCL = entity.LeftCL;
            IPD = entity.IPD;
            ADDValue = entity.ADDValue;
        }

        public string DoctorName { get; set; }
        public string StrDate { get; set; }
        public string RightSPH { get; set; }
        public string RightCYL { get; set; }
        public string RightAXIS { get; set; }
        public string RightCL { get; set; }
        public string LeftSPH { get; set; }
        public string LeftCYL { get; set; }
        public string LeftAXIS { get; set; }
        public string LeftCL { get; set; }
        public string IPD { get; set; }
        public string ADDValue { get; set; }
    }

    public class ExaminationModel : ExaminationSummaryModel
    {
        public ExaminationModel() : base()
        {}
        public ExaminationModel(Examination entity) : base(entity)
        {
            CustomerId = entity.CustomerId;
            DoctorId = entity.DoctorId;
            Date = entity.Date;
        }

        public DateTime? Date { get; set; }
        public long? CustomerId { get; set; }
        public long? DoctorId { get; set; }

        public void FillEntity(Examination entity)
        {
            entity.CustomerId = CustomerId;
            entity.DoctorId = DoctorId;
            entity.Date = Date;
            entity.RightSPH = RightSPH;
            entity.RightCYL = RightCYL;
            entity.RightAXIS = RightAXIS;
            entity.RightCL = RightCL;
            entity.LeftSPH = LeftSPH;
            entity.LeftCYL = LeftCYL;
            entity.LeftAXIS = LeftAXIS;
            entity.LeftCL = LeftCL;
            entity.IPD = IPD;
            entity.ADDValue = ADDValue;
        }
    }
}
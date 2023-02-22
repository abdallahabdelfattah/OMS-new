using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class DiseaseSummaryModel : BaseModel
    {
        public DiseaseSummaryModel()
        {}
        public DiseaseSummaryModel(Disease entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public class DiseaseModel : DiseaseSummaryModel
    {
        public DiseaseModel() : base()
        {}
        public DiseaseModel(Disease entity) : base(entity)
        {
        }

        public void FillEntity(Disease entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
        }
    }
}
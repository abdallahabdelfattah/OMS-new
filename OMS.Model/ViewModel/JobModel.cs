using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class JobSummaryModel : BaseModel
    {
        public JobSummaryModel()
        {}
        public JobSummaryModel(Job entity)
        {
            Id = entity.Id;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
        }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

    public class JobModel : JobSummaryModel
    {
        public JobModel() : base()
        {}
        public JobModel(Job entity) : base(entity)
        {
        }

        public void FillEntity(Job entity)
        {
            entity.NameAr = NameAr;
            entity.NameEn = NameEn;
        }
    }
}
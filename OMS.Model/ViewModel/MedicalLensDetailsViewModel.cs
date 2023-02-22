using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class MedicalLensDetailsSummaryModel : BaseModel
    {
        public MedicalLensDetailsSummaryModel()
        { }

        public MedicalLensDetailsSummaryModel(MedicalLensDetail entity)
        {
            Id = entity.Id;
        }
        public string BrandName { get; set; }
        public string Code { get; set; }
        public double? Price { get; set; }
        public string VersionTypeName { get; set; }
        public int Qty { get; set; }
        public long? BrandId { get; set; }
        public string Grade { get; set; }
        public string Power { get; set; }
        public long? Suppliers { get; set; }
    }

    public class MedicalLensDetailsViewModel : MedicalLensDetailsSummaryModel
    {
        public long? SupplierId { get; set; }
        public long? CoatingDiagramId { get; set; }
        public long? LenseIndexId { get; set; }
        public long? VersionTypeId { get; set; }
        public long? MultifocalDesignId { get; set; }
        public long? MaterialId { get; set; }
        public long? ColorId { get; set; }

        public MedicalLensDetailsViewModel() : base()
        { }
        public MedicalLensDetailsViewModel(MedicalLensDetail entity, string language) : base(entity)
        {
        }

        public void FillEntity(MedicalLensMaster entity)
        {
            entity.VersionTypeId = VersionTypeId;
            entity.MultifocalDesignId = MultifocalDesignId;
            entity.LenseIndexId = LenseIndexId;
            entity.CoatingDiagramId = CoatingDiagramId;
            entity.MaterialId = MaterialId;

            entity.BrandId = BrandId;
        }
    }
}
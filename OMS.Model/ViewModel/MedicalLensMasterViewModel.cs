using OMS.DAL.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Model.ViewModel
{
    public class MedicalLensMasterSummaryModel : BaseModel
    {
        public MedicalLensMasterSummaryModel()
        { }

        public MedicalLensMasterSummaryModel(MedicalLensMaster entity)
        {
            Id = entity.Id;
            BrandName = entity.Brand?.BrandName;
            Name = entity.Name;
            CompanyName = entity.CompanyName;
            CoatingDiagramName = entity.CoatingDiagram?.Name;
            VersionTypeName = entity.VersionType?.Name;
            Code = entity.Code;
            LensThisckness = entity.LensThisckness;
            MultifocalDesignName = entity.MultifocalDesign?.Name;
            LensDiameter = entity.LensDiameter;
            AllowedDiscount = entity.AllowedDiscount;
            LensIndexName = entity.LenseIndex?.Index;
            Notes = entity.Notes;
        }

        public string Notes { get; set; }

        public string BrandName { get; set; }
        public string Name { get; set; }
        public double? AllowedDiscount { get; set; }
        public string CompanyName { get; set; }
        public string Code { get; set; }
        public double? Price { get; set; }
        public double? LensDiameter { get; set; }
        public double? LensThisckness { get; set; }
        public double? LensIndexName { get; set; }
        public string CoatingDiagramName { get; set; }
        public string MultifocalDesignName { get; set; }
        public string VersionTypeName { get; set; }
        public int Qty { get; set; }
        public long? BrandId { get; set; }
        public string Grade { get; set; }
        public string Power { get; set; }
        public long? Suppliers { get; set; }
    }

    public class MedicalLensMasterViewModel : MedicalLensMasterSummaryModel
    {
        public long? MedicalLensTypeId { get; set; }
        public bool? IsPositive { get; set; }
        public string Notes { get; set; }
        public long? BrandId { get; set; }
        public long? SupplierId { get; set; }
        public long? CoatingDiagramId { get; set; }
        public long? LenseIndexId { get; set; }
        public long? VersionTypeId { get; set; }
        public long? MultifocalDesignId { get; set; }
        public long? MaterialId { get; set; }
        public long? ColorId { get; set; }
        public long? MedicalLensDetailsTypeId { get; set; }

        public MedicalLensMasterViewModel() : base()
        { }
        public MedicalLensMasterViewModel(MedicalLensMaster entity) : base(entity)
        {
            MedicalLensTypeId = entity.MedicalLensTypeId;
            MaterialId = entity.MaterialId;
            MultifocalDesignId = entity.MultifocalDesignId;
            CoatingDiagramId = entity.CoatingDiagramId;
            LenseIndexId = entity.LenseIndexId;
            BrandId = entity.BrandId;
            VersionTypeId = entity.VersionTypeId;
        }

        public void FillEntity(MedicalLensMaster entity)
        {
            entity.VersionTypeId = VersionTypeId;
            entity.MultifocalDesignId = MultifocalDesignId;
            entity.LenseIndexId = LenseIndexId;
            entity.LensDiameter = LensDiameter;
            entity.CoatingDiagramId = CoatingDiagramId;
            entity.MaterialId = MaterialId;
            entity.BrandId = BrandId;
            entity.Name = Name;
            entity.AllowedDiscount = AllowedDiscount;
            entity.IsPositive = IsPositive;
            entity.CompanyName = CompanyName;
            entity.Code = Code;
            entity.BrandId = BrandId;
            entity.ColorId = ColorId;
            entity.LensThisckness = LensThisckness;
            entity.SupplierId = SupplierId;
            entity.Notes = Notes;
        }
    }
}

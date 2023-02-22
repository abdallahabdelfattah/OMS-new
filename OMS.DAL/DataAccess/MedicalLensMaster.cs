//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OMS.DAL.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class MedicalLensMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MedicalLensMaster()
        {
            this.MedicalLensPurchaseDetails = new HashSet<MedicalLensPurchaseDetail>();
            this.SalesTransactionDetails = new HashSet<SalesTransactionDetail>();
            this.WarehouseTransactionDetails = new HashSet<WarehouseTransactionDetail>();
            this.WarehouseVerificationDetails = new HashSet<WarehouseVerificationDetail>();
            this.MedicalLensSalesDetails = new HashSet<MedicalLensSalesDetail>();
        }
    
        public long Id { get; set; }
        public Nullable<long> MedicalLensTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<double> AllowedDiscount { get; set; }
        public Nullable<double> LensDiameter { get; set; }
        public Nullable<double> LensThisckness { get; set; }
        public Nullable<bool> IsPositive { get; set; }
        public string CompanyName { get; set; }
        public string Notes { get; set; }
        public Nullable<long> SupplierId { get; set; }
        public Nullable<long> ColorId { get; set; }
        public Nullable<long> CoatingDiagramId { get; set; }
        public Nullable<long> LenseIndexId { get; set; }
        public Nullable<long> MultifocalDesignId { get; set; }
        public Nullable<long> VersionTypeId { get; set; }
        public Nullable<long> BrandId { get; set; }
        public Nullable<long> MaterialId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual Material Material { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual CoatingDiagram CoatingDiagram { get; set; }
        public virtual LenseIndex LenseIndex { get; set; }
        public virtual MedicalLensType MedicalLensType { get; set; }
        public virtual MultifocalDesign MultifocalDesign { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual VersionType VersionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalLensPurchaseDetail> MedicalLensPurchaseDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesTransactionDetail> SalesTransactionDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseTransactionDetail> WarehouseTransactionDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WarehouseVerificationDetail> WarehouseVerificationDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MedicalLensSalesDetail> MedicalLensSalesDetails { get; set; }
    }
}
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
    
    public partial class SalesTransactionDetail
    {
        public long Id { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> VATValue { get; set; }
        public Nullable<decimal> SalesPrice { get; set; }
        public Nullable<long> TransactionId { get; set; }
        public Nullable<long> MedicalLensIMasterd { get; set; }
        public Nullable<long> ProductId { get; set; }
    
        public virtual MedicalLensMaster MedicalLensMaster { get; set; }
        public virtual SalesTransaction SalesTransaction { get; set; }
        public virtual Product Product { get; set; }
    }
}

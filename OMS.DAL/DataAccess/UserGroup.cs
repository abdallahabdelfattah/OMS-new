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
    
    public partial class UserGroup
    {
        public long Id { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> GroupId { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}

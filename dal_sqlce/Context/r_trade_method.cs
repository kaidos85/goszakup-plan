//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dal_sqlce.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class r_trade_method
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public r_trade_method()
        {
            this.UserData = new HashSet<UserData>();
        }
    
        public long code { get; set; }
        public string name_kz { get; set; }
        public string name_ru { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserData> UserData { get; set; }
    }
}
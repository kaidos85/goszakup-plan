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
    
    public partial class Orgs
    {
        public long Id { get; set; }
        public string name_kz { get; set; }
        public string name_ru { get; set; }
        public Nullable<long> BIN { get; set; }
        public Nullable<long> KodGU { get; set; }
        public Nullable<long> Budget { get; set; }
        public Nullable<bool> Negu { get; set; }
    
        public virtual r_bud_type r_bud_type { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lab8.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class cust_reservations
    {
        public int res_cust_id { get; set; }
        public int fkcustomerid { get; set; }
        public int fkbookingid { get; set; }
    
        public virtual reservation reservation { get; set; }
        public virtual customer customer { get; set; }
    }
}

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
    
    public partial class boat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public boat()
        {
            this.reservations = new HashSet<reservation>();
        }
    
        public int boatid { get; set; }
        public int fkcategoryid { get; set; }
        public string name { get; set; }
        public int fkcolourid { get; set; }
        public decimal hour_rate { get; set; }
        public decimal daily_rate { get; set; }
    
        public virtual category category { get; set; }
        public virtual boat_colour boat_colour { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reservation> reservations { get; set; }
    }
}

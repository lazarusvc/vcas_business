//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VCAS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VCAS_forms
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VCAS_forms()
        {
            this.VCAS_REF_forms = new HashSet<VCAS_REF_forms>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string form { get; set; }
        public System.DateTime dateModified { get; set; }
        public int FK_location { get; set; }
        public int FK_REF_userRolesId { get; set; }
        public string calendarTitle { get; set; }
        public string calendarStartDate { get; set; }
        public string calendarEndDate { get; set; }
        public string calendarEventColor { get; set; }
    
        public virtual VCAS_council VCAS_council { get; set; }
        public virtual VCAS_REF_userRoles VCAS_REF_userRoles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VCAS_REF_forms> VCAS_REF_forms { get; set; }
    }
}

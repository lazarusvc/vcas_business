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
    
    public partial class VCAS_REF_userRoles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VCAS_REF_userRoles()
        {
            this.VCAS_users = new HashSet<VCAS_users>();
            this.VCAS_REF_reports = new HashSet<VCAS_reports>();
            this.VCAS_supportDocs = new HashSet<VCAS_supportDocs>();
            this.VCAS_forms = new HashSet<VCAS_forms>();
            this.VCAS_links = new HashSet<VCAS_links>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VCAS_users> VCAS_users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VCAS_reports> VCAS_REF_reports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VCAS_supportDocs> VCAS_supportDocs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VCAS_forms> VCAS_forms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VCAS_links> VCAS_links { get; set; }
    }
}

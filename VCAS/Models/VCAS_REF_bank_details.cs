
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
    
public partial class VCAS_REF_bank_details
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public VCAS_REF_bank_details()
    {

        this.VCAS_capture_payments = new HashSet<VCAS_capture_payments>();

    }


    public int Id { get; set; }

    public string name { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<VCAS_capture_payments> VCAS_capture_payments { get; set; }

}

}
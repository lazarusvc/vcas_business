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
    
    public partial class VCAS_undepositedFunds
    {
        public int Id { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public int FK_location { get; set; }
        public Nullable<double> recieved_amount { get; set; }
        public Nullable<int> checkNo { get; set; }
        public string comment { get; set; }
        public string receiptNo { get; set; }
        public Nullable<int> FK_paymentType { get; set; }
        public Nullable<int> FK_bankDetails { get; set; }
        public Nullable<bool> deposited { get; set; }
        public Nullable<int> depositedID { get; set; }
    
        public virtual VCAS_council VCAS_council { get; set; }
    }
}

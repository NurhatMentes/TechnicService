//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechnicService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Expenses = new HashSet<Expenses>();
        }
    
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Purchase { get; set; }
        public decimal SalesPrice { get; set; }
        public int stock { get; set; }
        public bool Status { get; set; }
        public string BarcodeNo { get; set; }
        public Nullable<int> CustomerId { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Expenses> Expenses { get; set; }
        public virtual Customers Customers { get; set; }
    }
}

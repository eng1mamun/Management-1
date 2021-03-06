//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WppAI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EMPName { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> GenderId { get; set; }
        public string Photo { get; set; }
        public string Date { get; set; }
        public string UName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual GenderInfo GenderInfo { get; set; }
    }
}

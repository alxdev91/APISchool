using System;
using System.Collections.Generic;

namespace APISchool.Models
{
    public partial class Payment
    {
        public int IdPayment { get; set; }
        public int? IdStudent { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? SecLastname { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? RegistratedDate { get; set; }
        public string? Document { get; set; }

        public virtual Student? oPayStudent { get; set; }
    }
}

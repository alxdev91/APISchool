using System;
using System.Collections.Generic;

namespace APISchool.Models
{
    public partial class Student
    {
        public Student()
        {
            Contacts = new HashSet<Contact>();
            Payments = new HashSet<Payment>();
        }

        public int IdStudent { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? SecLastname { get; set; }
        public int? Age { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }
        public DateTime? EnrolDate { get; set; }
        public DateTime? UnsubscribeDate { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

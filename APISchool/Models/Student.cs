using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;//se agrego par autilizar el [JsonIgnore]

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

        [JsonIgnore]//se agrega para que no se muestre en el resultado
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

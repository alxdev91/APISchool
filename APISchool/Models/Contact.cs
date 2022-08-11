using System;
using System.Collections.Generic;

namespace APISchool.Models
{
    public partial class Contact
    {
        public int IdContact { get; set; }
        public int? IdStudent { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? SecLastname { get; set; }
        public bool? Gender { get; set; }
        public string? Relationship { get; set; }
        public string? Email { get; set; }
        public string? HomePhone { get; set; }
        public string? CellPhone { get; set; }

        //modifique
        public virtual Student? oCtoStudent { get; set; }
    }
}

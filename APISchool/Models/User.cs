using System;
using System.Collections.Generic;

namespace APISchool.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? SecLastname { get; set; }
        public int? IdRole { get; set; }
        public bool? Status { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

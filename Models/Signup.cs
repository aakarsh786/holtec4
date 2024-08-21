using System;
using System.Collections.Generic;

namespace holtec4.Models
{
    public partial class Signup
    {
        public Signup()
        {
            Alluserdata = new HashSet<Alluserdatum>();
            Applicants = new HashSet<Applicant>();
        }

        public int Userid { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Alluserdatum> Alluserdata { get; set; }
        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace holtec4.Models
{
    public partial class Alluserdatum
    {
        public int Alluserid { get; set; }
        public int? Userid { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? Dateofbirth { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Userrole { get; set; }
        public string? ClassX { get; set; }
        public decimal? XMarks { get; set; }
        public int? XYear { get; set; }
        public string? ClassXii { get; set; }
        public decimal? XiiMarks { get; set; }
        public int? XiiYear { get; set; }
        public string? Bachelors { get; set; }
        public decimal? BachelorsMarks { get; set; }
        public int? BachelorsYear { get; set; }
        public string? Masters { get; set; }
        public decimal? MastersMarks { get; set; }
        public int? MastersYear { get; set; }
        public string? Diploma { get; set; }
        public decimal? DiplomaMarks { get; set; }
        public int? DiplomaYear { get; set; }
        public string? Company1 { get; set; }
        public string? Role1 { get; set; }
        public int? Years1 { get; set; }
        public string? Company2 { get; set; }
        public string? Role2 { get; set; }
        public int? Years2 { get; set; }
        public string? Company3 { get; set; }
        public string? Role3 { get; set; }
        public int? Years3 { get; set; }
        public string? Company4 { get; set; }
        public string? Role4 { get; set; }
        public int? Years4 { get; set; }
        public string? Company5 { get; set; }
        public string? Role5 { get; set; }
        public int? Years5 { get; set; }
        public bool Cplusplus { get; set; }
        public bool Javascript { get; set; }
        public bool C { get; set; }
        public bool Python { get; set; }
        public bool Java { get; set; }
        public bool Html { get; set; }
        public bool Flutter { get; set; }
        public bool Aiml { get; set; }
        public bool React { get; set; }
        public bool Angular { get; set; }
        public bool Autocad { get; set; }
        public bool ComputerGraphics { get; set; }
        public byte[]? Resume { get; set; }
        public byte[]? Profilepic { get; set; }

        public virtual Signup? User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace holtec4.Models
{
    public partial class Department
    {
        public Department()
        {
            Jobs = new HashSet<Job>();
        }

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentManager { get; set; }
        public string? ManagerEmployeeId { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}

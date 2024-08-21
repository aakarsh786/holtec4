using holtec4.Models;

namespace holtec4.ViewModels
{
    public class ApplicantJobViewModel
    {
        public Applicant Applicant { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }
        public int? DepartmentId { get; set; }

    }
}

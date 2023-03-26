using JobPortal.Models;
using JobPortal.WebModels.DepartmentWebModels;
using JobPortal.WebModels.LocationWebModels;

namespace JobPortal.WebModels.JobWebModels
{
    public class GetJobResponseWebModel
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? ClosingDate { get; set; }

        public virtual GetDepartmentResponseWebModel? Department { get; set; }
        public virtual GetLocationResponseWebModel? Location { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace JobPortal.WebModels.JobWebModels
{
    public class UpdateJobRequestWebModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must contain at least 2 characters")]
        public string? Title { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Description must contain at least 2 characters")]
        public string? Description { get; set; }

        [Required]
        public int? LocationId { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        [Required]
        public DateTime? ClosingDate { get; set; }
    }
}

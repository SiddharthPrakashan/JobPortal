using System.ComponentModel.DataAnnotations;

namespace JobPortal.WebModels.DepartmentWebModels
{
    public class CreateDeptRequestWebModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must contain at least 2 characters")]
        public string? Title { get; set; }
    }
}

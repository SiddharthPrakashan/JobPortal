using System.ComponentModel.DataAnnotations;

namespace JobPortal.WebModels.AuthenticationWebModels
{
    public class AuthLoginWebModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}

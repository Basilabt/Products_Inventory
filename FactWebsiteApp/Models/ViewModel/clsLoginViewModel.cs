using System.ComponentModel.DataAnnotations;

namespace FactWebsiteApp.Models.ViewModel
{
    public class clsLoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? username { get; set; }


        [Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }
    }
}

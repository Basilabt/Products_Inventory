using System.ComponentModel.DataAnnotations;

namespace FactWebsiteApp.Models.ViewModel
{
    public class clsRegisterViewModel
    {

        [Required(ErrorMessage = "First Name is required")]
        public required string firstname {  get; set; }




        [Required(ErrorMessage = "Second Name is required")]
        public string? secondname { get; set; }




        [Required(ErrorMessage = "Third Name is required")]
        public string? thirdname { get; set; }




        [Required(ErrorMessage = "Last Name is required")]
        public string? lastname { get; set; }




        [Required(ErrorMessage = "Email is required")]
        public string? email { get; set; }




        [Required(ErrorMessage = "Phone Number is required")]
        public string? phonenumber { get; set; }




        [Required(ErrorMessage = "Gender is required")]
        public bool isMale { get; set; }




        [Required(ErrorMessage = "Username is required")]
        public string? username { get; set; }




        [Required(ErrorMessage = "Password is required")]
        public string? password { get; set; }
              

    }
}

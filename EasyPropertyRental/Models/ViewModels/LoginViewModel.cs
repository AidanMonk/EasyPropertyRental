using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required.")]
        [Key]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

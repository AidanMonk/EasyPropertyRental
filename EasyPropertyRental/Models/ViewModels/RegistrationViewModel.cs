using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters")]
        public string LastName { get; set; } = null!;

        [Key]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please Enter Valid Email.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength =6, ErrorMessage = "Password must be between 6 and 20 characters")]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Confirm password must match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}

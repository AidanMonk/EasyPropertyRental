using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models.ViewModels
{
    public class AssignTenantViewModel
    {
        public int ApartmentId { get; set; }

        [Required(ErrorMessage = "Please select a tenant.")]
        public int? SelectedTenantId { get; set; }

        public string ApartmentInfo { get; set; } = string.Empty;

        public List<SelectListItem> TenantList { get; set; } = new List<SelectListItem>();
    }
}

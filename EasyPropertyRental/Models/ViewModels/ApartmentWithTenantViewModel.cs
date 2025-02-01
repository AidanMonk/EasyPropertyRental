namespace EasyPropertyRental.Models.ViewModels
{
    public class ApartmentWithTenantViewModel
    {
        public int ApartmentId { get; set; }
        public string UnitNumber { get; set; } = null!;
        public int? Floor { get; set; }
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public decimal? Rent { get; set; }
        public string BuildingName { get; set; } = null!;

        // This will hold the tenant information if available
        public List<string> TenantNames { get; set; } = new List<string>();
    }

}

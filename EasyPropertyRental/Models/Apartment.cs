using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models;

public partial class Apartment
{
    public int ApartmentId { get; set; }

    // BuildingId is nullable, so we don't need any validation here, but we can still add the required validation for the related Building property.
    public int? BuildingId { get; set; }

    // UnitNumber is required, cannot be empty
    [Required(ErrorMessage = "Unit number is required.")]
    [StringLength(10, ErrorMessage = "Unit number cannot be longer than 10 characters.")]
    public string UnitNumber { get; set; } = null!;

    // Floor must be a positive integer and required
    [Range(1, int.MaxValue, ErrorMessage = "Floor number must be greater than 0.")]
    public int? Floor { get; set; }

    // Bedrooms and Bathrooms must be positive integers and required
    [Range(1, 10, ErrorMessage = "Bedrooms must be between 1 and 10.")]
    public int? Bedrooms { get; set; }

    [Range(1, 10, ErrorMessage = "Bathrooms must be between 1 and 10.")]
    public int? Bathrooms { get; set; }

    // Rent should be a positive decimal and is required
    [Range(0.01, double.MaxValue, ErrorMessage = "Rent must be a positive value.")]
    public decimal? Rent { get; set; }

    // IsAvailable can be either true or false, but it's optional
    public bool? IsAvailable { get; set; }

    public virtual Building? Building { get; set; }

    public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}

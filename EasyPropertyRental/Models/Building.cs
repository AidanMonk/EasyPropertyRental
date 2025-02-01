using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models;

public partial class Building
{
    public int BuildingId { get; set; }

    // Name of the building is required and has a maximum length of 100 characters
    [Required(ErrorMessage = "Building name is required.")]
    [StringLength(100, ErrorMessage = "Building name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    // Address is required and has a maximum length of 255 characters
    [Required(ErrorMessage = "Address is required.")]
    [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
    public string Address { get; set; } = null!;

    // PmId is nullable, but if present, it must refer to an existing property manager
    public int? PmId { get; set; }

    // Collection of apartments in the building (no specific validation needed for collection)
    public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

    // PropertyManager for the building (optional but can be validated if needed)
    public virtual PropertyManager? Pm { get; set; }
}


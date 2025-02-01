using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models;

public partial class PropertyOwner
{
    [Key]
    public int PoId { get; set; }

    // First Name is required and cannot exceed 50 characters
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = null!;

    // Last Name is required and cannot exceed 50 characters
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = null!;

    // Email is required, must be a valid email format, and cannot exceed 100 characters
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string Email { get; set; } = null!;

    // Password is required and must meet certain criteria (min length of 6 characters)
    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = null!;

    // CreatedAt is optional but should not be set in the future (if a value is provided)
    public DateTime? CreatedAt { get; set; }

    // Virtual Navigation Properties (no validation needed for collections)
    public virtual ICollection<Message> MessageReceiverNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenderNavigations { get; set; } = new List<Message>();

    public virtual ICollection<PropertyManager> PropertyManagers { get; set; } = new List<PropertyManager>();
}

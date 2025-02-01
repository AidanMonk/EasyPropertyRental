using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models;

public partial class PropertyManager
{
    public int PmId { get; set; }

    // First Name is required and cannot exceed 50 characters
    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public string FirstName { get; set; } = null!;

    // Last Name is required and cannot exceed 50 characters
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    public string LastName { get; set; } = null!;

    // Email is required and must be a valid email address
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    public string Email { get; set; } = null!;

    // Phone is optional, but if provided, it must match a specific pattern (valid phone number format)
    [Phone(ErrorMessage = "Invalid phone number.")]
    public string? Phone { get; set; }

    // Password is required and cannot be empty
    [Required(ErrorMessage = "Password is required.")]
    [StringLength(50, ErrorMessage = "Password cannot exceed 50 characters.")]
    public string Password { get; set; } = null!;

    // PoId is nullable, but it can be linked to a PropertyOwner
    public int? PoId { get; set; }

    // Navigation properties (no validation needed here, as they are relational)
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();
    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();

    // Nullable reference to PropertyOwner (optional relationship)
    public virtual PropertyOwner? Po { get; set; }
}

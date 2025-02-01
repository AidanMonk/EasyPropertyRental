using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyPropertyRental.Models;

public partial class Tenant
{
    public int TenantId { get; set; }

    // First Name is required and cannot exceed 50 characters
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = null!;

    // Last Name is required and cannot exceed 50 characters
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = null!;

    // Email is required, must be a valid email address, and cannot exceed 100 characters
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
    public string Email { get; set; } = null!;

    // Phone is optional but must follow a valid phone number format if provided
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? Phone { get; set; }

    // Password is required and must meet certain criteria (min length of 6 characters)
    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = null!;

    // ApartmentId is nullable, indicating the tenant might not be assigned to an apartment yet
    public int? ApartmentId { get; set; }

    // PmId is nullable, but when provided, it refers to a Property Manager
    public int? PmId { get; set; }

    // MoveInDate and MoveOutDate are optional but must follow a valid date format
    public DateOnly? MoveInDate { get; set; }

    public DateOnly? MoveOutDate { get; set; }

    // Navigation properties (no validation needed for these, but they represent relationships)
    public virtual Apartment? Apartment { get; set; }
    public virtual PropertyManager? PropertyManager { get; set; }
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public virtual ICollection<Message> MessageReceiver1s { get; set; } = new List<Message>();
    public virtual ICollection<Message> MessageSender1s { get; set; } = new List<Message>();
}

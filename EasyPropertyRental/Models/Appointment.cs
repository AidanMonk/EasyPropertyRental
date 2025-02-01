using System;
using System.Collections.Generic;

namespace EasyPropertyRental.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? TenantId { get; set; }

    public int? PmId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual PropertyManager? Pm { get; set; }

    public virtual Tenant? Tenant { get; set; }
}

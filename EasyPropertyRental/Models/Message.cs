using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace EasyPropertyRental.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public string? SenderType { get; set; }

    public int SenderId { get; set; }

    public string? ReceiverType { get; set; }

    public int ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    [ValidateNever]
    public virtual PropertyManager Receiver { get; set; } = null!;

    [ValidateNever]
    public virtual Tenant Receiver1 { get; set; } = null!;

    [ValidateNever]
    public virtual PropertyOwner ReceiverNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual PropertyManager Sender { get; set; } = null!;

    [ValidateNever]
    public virtual Tenant Sender1 { get; set; } = null!;

    [ValidateNever]
    public virtual PropertyOwner SenderNavigation { get; set; } = null!;
}

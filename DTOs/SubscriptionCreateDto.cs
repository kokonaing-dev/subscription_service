using subscription_service.Models.Enums;

namespace subscription_service.DTOs;

public class SubscriptionCreateDto
{
    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }

    public Guid? CourseId { get; set; }

    public SubscriptionStatus Status { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public DateTime? StopAutoRenewAt { get; set; }

    public string? StopAutoRenewReason { get; set; }

    public bool? AutoRenew { get; set; }

    public Guid? TransactionId { get; set; }

    public string? StripeSubscriptionId { get; set; }
}

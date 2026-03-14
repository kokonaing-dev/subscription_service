using subscription_service.Models.Enums;

namespace subscription_service.Models;

public class Subscription : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid PlanId { get; set; }

    public Guid? CourseId { get; set; }

    public SubscriptionStatus Status { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public DateTime? StopAutoRenewAt { get; set; }

    public string StopAutoRenewReason { get; set; }

    public bool AutoRenew { get; set; } = true;

    public Guid? TransactionId { get; set; }

    public string StripeSubscriptionId { get; set; }

    public Plan Plan { get; set; }

    public SubscriptionTransaction Transaction { get; set; }
}

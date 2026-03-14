using subscription_service.Models.Enums;

namespace subscription_service.Models;

public class GatewayRawEvent : BaseEntity
{
    public string PaymentProvider { get; set; }

    public string EventType { get; set; }

    public string EventRequestPayload { get; set; }

    public string EventResponsePayload { get; set; }

    public string ProviderEventId { get; set; }

    public GatewayRawEventStatus ProcessedStatus { get; set; } = GatewayRawEventStatus.Pending;

    public DateTime? ProcessedAt { get; set; }

    public string ErrorMessage { get; set; }

    public ICollection<SubscriptionTransaction> SubscriptionTransactions { get; set; }
}

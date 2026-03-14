using subscription_service.Models.Enums;

namespace subscription_service.Models;

public class SubscriptionTransaction : BaseEntity
{
    public float Amount { get; set; }

    public string Currency { get; set; } = "USD";

    public string TransactionNo { get; set; }

    public string ReceiptId { get; set; }

    public SubscriptionTransactionStatus Status { get; set; } = SubscriptionTransactionStatus.Pending;

    public float? GrossAmount { get; set; }

    public float? TaxAmount { get; set; }

    public float? TaxRate { get; set; }

    public float? NetAmount { get; set; }

    public Guid? PaymentGatewayId { get; set; }

    public PaymentGateway PaymentGateway { get; set; }

    public Guid? GatewayRawEventId { get; set; }

    public GatewayRawEvent GatewayRawEvent { get; set; }

    public string FailedReason { get; set; }

    public string Metadata { get; set; }

    public string PaymentMethodType { get; set; }

    public string CardBrand { get; set; }

    public string CardLast4 { get; set; }

    public Subscription Subscription { get; set; }
}

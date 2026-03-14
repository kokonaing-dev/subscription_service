using subscription_service.Models.Enums;

namespace subscription_service.DTOs;

public class SubscriptionTransactionCreateDto
{
    public float Amount { get; set; }

    public string? Currency { get; set; }

    public string? TransactionNo { get; set; }

    public string? ReceiptId { get; set; }

    public SubscriptionTransactionStatus Status { get; set; }

    public float? GrossAmount { get; set; }

    public float? TaxAmount { get; set; }

    public float? TaxRate { get; set; }

    public float? NetAmount { get; set; }

    public Guid? PaymentGatewayId { get; set; }

    public Guid? GatewayRawEventId { get; set; }

    public string? FailedReason { get; set; }

    public string? Metadata { get; set; }

    public string? PaymentMethodType { get; set; }

    public string? CardBrand { get; set; }

    public string? CardLast4 { get; set; }
}

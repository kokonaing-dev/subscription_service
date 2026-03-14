using subscription_service.Models.Enums;

namespace subscription_service.DTOs;

public class AIPointTransactionCreateDto
{
    public float Amount { get; set; }

    public Guid UserAIPointId { get; set; }

    public int? AIPoint { get; set; }

    public Guid? SubscriptionTransactionNo { get; set; }

    public Guid? CourseId { get; set; }

    public Guid? CoursematerialId { get; set; }

    public string? CourseCode { get; set; }

    public string? MaterialName { get; set; }

    public string? Currency { get; set; }

    public AIPointTransactionStatus Status { get; set; }

    public float? GrossAmount { get; set; }

    public float? TaxAmount { get; set; }

    public float? TaxRate { get; set; }

    public float? NetAmount { get; set; }

    public Guid? PaymentGatewayId { get; set; }

    public string? FailedReason { get; set; }

    public string? Metadata { get; set; }

    public AiPointType AiPointType { get; set; }
}

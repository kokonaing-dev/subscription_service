using subscription_service.Models.Enums;

namespace subscription_service.Models;

public class AIPointTransaction : BaseEntity
{
    public float Amount { get; set; }

    public Guid UserAIPointId { get; set; }

    public UserAIPoint UserAIPoint { get; set; }

    public int AIPoint { get; set; } = 0;

    public Guid? SubscriptionTransactionNo { get; set; }

    public SubscriptionTransaction SubscriptionTransaction { get; set; }

    public Guid? CourseId { get; set; }

    public Guid? CoursematerialId { get; set; }

    public string CourseCode { get; set; }

    public string MaterialName { get; set; }

    public string Currency { get; set; }

    public AIPointTransactionStatus Status { get; set; } = AIPointTransactionStatus.Pending;

    public float? GrossAmount { get; set; }

    public float? TaxAmount { get; set; }

    public float? TaxRate { get; set; }

    public float? NetAmount { get; set; }

    public Guid? PaymentGatewayId { get; set; }

    public PaymentGateway PaymentGateway { get; set; }

    public string FailedReason { get; set; }

    public string Metadata { get; set; }

    public AiPointType AiPointType { get; set; }
}

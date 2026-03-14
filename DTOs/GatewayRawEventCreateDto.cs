using subscription_service.Models.Enums;

namespace subscription_service.DTOs;

public class GatewayRawEventCreateDto
{
    public string? PaymentProvider { get; set; }

    public string? EventType { get; set; }

    public string? EventRequestPayload { get; set; }

    public string? EventResponsePayload { get; set; }

    public string? ProviderEventId { get; set; }

    public GatewayRawEventStatus ProcessedStatus { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public string? ErrorMessage { get; set; }
}

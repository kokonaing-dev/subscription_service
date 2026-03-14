namespace subscription_service.DTOs;

public class PaymentGatewayCreateDto
{
    public string Provider { get; set; }

    public string DisplayName { get; set; }

    public Guid? CountryId { get; set; }

    public bool? Active { get; set; }
}

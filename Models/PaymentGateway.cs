namespace subscription_service.Models;

public class PaymentGateway : BaseEntity
{
    public string Provider { get; set; }

    public string DisplayName { get; set; }

    public Guid? CountryId { get; set; }

    public bool Active { get; set; } = true;

    public ICollection<Plan> Plans { get; set; }
}
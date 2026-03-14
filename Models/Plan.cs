using subscription_service.Models.Enums;

namespace subscription_service.Models;

public class Plan : BaseEntity
{
    public ProductType ProductType { get; set; }

    public string Title { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; } = "USD";

    public string PlanType { get; set; }

    public decimal? TaxAmount { get; set; }

    public Guid? PaymentGatewayId { get; set; }

    public PaymentGateway PaymentGateway { get; set; }

    public string TermsAndCondition { get; set; }

    public Guid? CountryId { get; set; }

    public int? AIPoint { get; set; }

    public bool IsActive { get; set; } = true;

    public string StripeProductId { get; set; }

    public string StripePriceId { get; set; }

    public Guid? DiscountId { get; set; }

    public Discount Discount { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; }
}

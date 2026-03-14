using subscription_service.Models.Enums;

namespace subscription_service.DTOs;

public class PlanCreateDto
{
    public ProductType ProductType { get; set; }

    public string? Title { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public float Price { get; set; }

    public string? Currency { get; set; }

    public string? PlanType { get; set; }

    public float? TaxAmount { get; set; }

    public Guid? PaymentGatewayId { get; set; }

    public string? TermsAndCondition { get; set; }

    public Guid? CountryId { get; set; }

    public int? AIPoint { get; set; }

    public bool? IsActive { get; set; }

    public string? StripeProductId { get; set; }

    public string? StripePriceId { get; set; }

    public Guid? DiscountId { get; set; }
}

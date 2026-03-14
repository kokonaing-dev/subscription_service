namespace subscription_service.DTOs;

public class DiscountCreateDto
{
    public string Code { get; set; }

    public float? Amount { get; set; }

    public double? Percentage { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public string Note { get; set; }

    public bool? IsActive { get; set; }
}

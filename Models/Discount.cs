namespace subscription_service.Models;

public class Discount : BaseEntity
{
    public string Code { get; set; }

    public float? Amount { get; set; }

    public double? Percentage { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public string Note { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Plan> Plans { get; set; }
}
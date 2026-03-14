namespace subscription_service.DTOs;

public class UserAIPointCreateDto
{
    public Guid UserId { get; set; }

    public int? TotalAIPoints { get; set; }
}

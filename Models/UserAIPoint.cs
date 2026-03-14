namespace subscription_service.Models;

public class UserAIPoint : BaseEntity
{
    public Guid UserId { get; set; }

    public int TotalAIPoints { get; set; } = 0;

    public ICollection<AIPointTransaction> AIPointTransactions { get; set; }
}

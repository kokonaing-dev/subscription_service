using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface IAIPointTransactionService
{
    IQueryable<AIPointTransaction> Query();

    Task<AIPointTransaction> CreateAsync(AIPointTransactionCreateDto dto);

    Task<AIPointTransaction?> PatchAsync(Guid id, Delta<AIPointTransaction> delta);

    Task<bool> DeleteAsync(Guid id);
}

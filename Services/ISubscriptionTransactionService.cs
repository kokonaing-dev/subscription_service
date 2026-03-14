using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface ISubscriptionTransactionService
{
    IQueryable<SubscriptionTransaction> Query();

    Task<SubscriptionTransaction> CreateAsync(SubscriptionTransactionCreateDto dto);

    Task<SubscriptionTransaction?> PatchAsync(Guid id, Delta<SubscriptionTransaction> delta);

    Task<bool> DeleteAsync(Guid id);
}

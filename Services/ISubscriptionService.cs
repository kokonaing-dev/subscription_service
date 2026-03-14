using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface ISubscriptionService
{
    IQueryable<Subscription> Query();

    Task<Subscription> CreateAsync(SubscriptionCreateDto dto);

    Task<Subscription?> PatchAsync(Guid id, Delta<Subscription> delta);

    Task<bool> DeleteAsync(Guid id);
}

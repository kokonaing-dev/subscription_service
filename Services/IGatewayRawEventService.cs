using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface IGatewayRawEventService
{
    IQueryable<GatewayRawEvent> Query();

    Task<GatewayRawEvent> CreateAsync(GatewayRawEventCreateDto dto);

    Task<GatewayRawEvent?> PatchAsync(Guid id, Delta<GatewayRawEvent> delta);

    Task<bool> DeleteAsync(Guid id);
}

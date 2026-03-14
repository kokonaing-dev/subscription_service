using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface IUserAIPointService
{
    IQueryable<UserAIPoint> Query();

    Task<UserAIPoint> CreateAsync(UserAIPointCreateDto dto);

    Task<UserAIPoint?> PatchAsync(Guid id, Delta<UserAIPoint> delta);

    Task<bool> DeleteAsync(Guid id);
}

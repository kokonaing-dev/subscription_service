using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface IPlanService
{
    IQueryable<Plan> Query();

    Task<Plan> CreateAsync(PlanCreateDto dto);

    Task<Plan?> PatchAsync(Guid id, Delta<Plan> delta);

    Task<bool> DeleteAsync(Guid id);
}

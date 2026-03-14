using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface IDiscountService
{
    IQueryable<Discount> Query();

    Task<Discount> CreateAsync(DiscountCreateDto dto);

    Task<Discount?> PatchAsync(Guid id, Delta<Discount> delta);

    Task<bool> DeleteAsync(Guid id);
}

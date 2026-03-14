using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class PlanService : IPlanService
{
    private readonly AppDbContext _context;

    public PlanService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Plan> Query()
    {
        return _context.Plans.AsQueryable();
    }

    public async Task<Plan> CreateAsync(PlanCreateDto dto)
    {
        var entity = new Plan
        {
            ProductType = dto.ProductType,
            Title = dto.Title,
            Image = dto.Image,
            Description = dto.Description,
            Price = dto.Price,
            Currency = dto.Currency ?? "USD",
            PlanType = dto.PlanType,
            TaxAmount = dto.TaxAmount,
            PaymentGatewayId = dto.PaymentGatewayId,
            TermsAndCondition = dto.TermsAndCondition,
            CountryId = dto.CountryId,
            AIPoint = dto.AIPoint,
            IsActive = dto.IsActive ?? true,
            StripeProductId = dto.StripeProductId,
            StripePriceId = dto.StripePriceId,
            DiscountId = dto.DiscountId
        };

        _context.Plans.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Plan?> PatchAsync(Guid id, Delta<Plan> delta)
    {
        var entity = await _context.Plans.FindAsync(id);
        if (entity == null)
        {
            return null;
        }

        delta.Patch(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Plans.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.Plans.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

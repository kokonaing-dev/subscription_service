using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class DiscountService : IDiscountService
{
    private readonly AppDbContext _context;

    public DiscountService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Discount> Query()
    {
        return _context.Discounts.AsQueryable();
    }

    public async Task<Discount> CreateAsync(DiscountCreateDto dto)
    {
        var entity = new Discount
        {
            Code = dto.Code,
            Amount = dto.Amount,
            Percentage = dto.Percentage,
            ExpiredAt = dto.ExpiredAt,
            Note = dto.Note,
            IsActive = dto.IsActive ?? true
        };

        _context.Discounts.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Discount?> PatchAsync(Guid id, Delta<Discount> delta)
    {
        var entity = await _context.Discounts.FindAsync(id);
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
        var entity = await _context.Discounts.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.Discounts.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

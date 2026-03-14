using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Subscription> Query()
    {
        return _context.Subscriptions.AsQueryable();
    }

    public async Task<Subscription> CreateAsync(SubscriptionCreateDto dto)
    {
        var entity = new Subscription
        {
            UserId = dto.UserId,
            PlanId = dto.PlanId,
            CourseId = dto.CourseId,
            Status = dto.Status,
            StartAt = dto.StartAt,
            ExpiredAt = dto.ExpiredAt,
            StopAutoRenewAt = dto.StopAutoRenewAt,
            StopAutoRenewReason = dto.StopAutoRenewReason,
            AutoRenew = dto.AutoRenew ?? true,
            TransactionId = dto.TransactionId,
            StripeSubscriptionId = dto.StripeSubscriptionId
        };

        _context.Subscriptions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Subscription?> PatchAsync(Guid id, Delta<Subscription> delta)
    {
        var entity = await _context.Subscriptions.FindAsync(id);
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
        var entity = await _context.Subscriptions.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.Subscriptions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

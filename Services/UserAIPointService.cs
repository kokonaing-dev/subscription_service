using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class UserAIPointService : IUserAIPointService
{
    private readonly AppDbContext _context;

    public UserAIPointService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<UserAIPoint> Query()
    {
        return _context.UserAIPoints.AsQueryable();
    }

    public async Task<UserAIPoint> CreateAsync(UserAIPointCreateDto dto)
    {
        var entity = new UserAIPoint
        {
            UserId = dto.UserId,
            TotalAIPoints = dto.TotalAIPoints ?? 0
        };

        _context.UserAIPoints.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<UserAIPoint?> PatchAsync(Guid id, Delta<UserAIPoint> delta)
    {
        var entity = await _context.UserAIPoints.FindAsync(id);
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
        var entity = await _context.UserAIPoints.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.UserAIPoints.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

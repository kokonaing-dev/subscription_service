using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class GatewayRawEventService : IGatewayRawEventService
{
    private readonly AppDbContext _context;

    public GatewayRawEventService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<GatewayRawEvent> Query()
    {
        return _context.GatewayRawEvents.AsQueryable();
    }

    public async Task<GatewayRawEvent> CreateAsync(GatewayRawEventCreateDto dto)
    {
        var entity = new GatewayRawEvent
        {
            PaymentProvider = dto.PaymentProvider,
            EventType = dto.EventType,
            EventRequestPayload = dto.EventRequestPayload,
            EventResponsePayload = dto.EventResponsePayload,
            ProviderEventId = dto.ProviderEventId,
            ProcessedStatus = dto.ProcessedStatus,
            ProcessedAt = dto.ProcessedAt,
            ErrorMessage = dto.ErrorMessage
        };

        _context.GatewayRawEvents.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<GatewayRawEvent?> PatchAsync(Guid id, Delta<GatewayRawEvent> delta)
    {
        var entity = await _context.GatewayRawEvents.FindAsync(id);
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
        var entity = await _context.GatewayRawEvents.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.GatewayRawEvents.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

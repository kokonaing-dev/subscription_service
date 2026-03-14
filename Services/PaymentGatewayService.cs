using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.EntityFrameworkCore;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class PaymentGatewayService : IPaymentGatewayService
{
    private readonly AppDbContext _context;

    public PaymentGatewayService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<PaymentGateway> Query()
    {
        return _context.PaymentGateways.AsQueryable();
    }

    public async Task<PaymentGateway> CreateAsync(PaymentGatewayCreateDto dto)
    {
        var entity = new PaymentGateway
        {
            Provider = dto.Provider,
            DisplayName = dto.DisplayName,
            CountryId = dto.CountryId,
            Active = dto.Active ?? true,
        };

        _context.PaymentGateways.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PaymentGateway?> PatchAsync(Guid id, Delta<PaymentGateway> delta)
    {
        var entity = await _context.PaymentGateways.FindAsync(id);
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
        var entity = await _context.PaymentGateways.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.PaymentGateways.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

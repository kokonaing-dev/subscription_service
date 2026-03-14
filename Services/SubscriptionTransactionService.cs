using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class SubscriptionTransactionService : ISubscriptionTransactionService
{
    private readonly AppDbContext _context;

    public SubscriptionTransactionService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<SubscriptionTransaction> Query()
    {
        return _context.SubscriptionTransactions.AsQueryable();
    }

    public async Task<SubscriptionTransaction> CreateAsync(SubscriptionTransactionCreateDto dto)
    {
        var entity = new SubscriptionTransaction
        {
            Amount = dto.Amount,
            Currency = dto.Currency ?? "USD",
            TransactionNo = dto.TransactionNo,
            ReceiptId = dto.ReceiptId,
            Status = dto.Status,
            GrossAmount = dto.GrossAmount,
            TaxAmount = dto.TaxAmount,
            TaxRate = dto.TaxRate,
            NetAmount = dto.NetAmount,
            PaymentGatewayId = dto.PaymentGatewayId,
            GatewayRawEventId = dto.GatewayRawEventId,
            FailedReason = dto.FailedReason,
            Metadata = dto.Metadata,
            PaymentMethodType = dto.PaymentMethodType,
            CardBrand = dto.CardBrand,
            CardLast4 = dto.CardLast4
        };

        _context.SubscriptionTransactions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<SubscriptionTransaction?> PatchAsync(Guid id, Delta<SubscriptionTransaction> delta)
    {
        var entity = await _context.SubscriptionTransactions.FindAsync(id);
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
        var entity = await _context.SubscriptionTransactions.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.SubscriptionTransactions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.Data;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public class AIPointTransactionService : IAIPointTransactionService
{
    private readonly AppDbContext _context;

    public AIPointTransactionService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<AIPointTransaction> Query()
    {
        return _context.AIPointTransactions.AsQueryable();
    }

    public async Task<AIPointTransaction> CreateAsync(AIPointTransactionCreateDto dto)
    {
        var entity = new AIPointTransaction
        {
            Amount = dto.Amount,
            UserAIPointId = dto.UserAIPointId,
            AIPoint = dto.AIPoint ?? 0,
            SubscriptionTransactionNo = dto.SubscriptionTransactionNo,
            CourseId = dto.CourseId,
            CoursematerialId = dto.CoursematerialId,
            CourseCode = dto.CourseCode,
            MaterialName = dto.MaterialName,
            Currency = dto.Currency,
            Status = dto.Status,
            GrossAmount = dto.GrossAmount,
            TaxAmount = dto.TaxAmount,
            TaxRate = dto.TaxRate,
            NetAmount = dto.NetAmount,
            PaymentGatewayId = dto.PaymentGatewayId,
            FailedReason = dto.FailedReason,
            Metadata = dto.Metadata,
            AiPointType = dto.AiPointType
        };

        _context.AIPointTransactions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<AIPointTransaction?> PatchAsync(Guid id, Delta<AIPointTransaction> delta)
    {
        var entity = await _context.AIPointTransactions.FindAsync(id);
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
        var entity = await _context.AIPointTransactions.FindAsync(id);
        if (entity == null)
        {
            return false;
        }

        _context.AIPointTransactions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}

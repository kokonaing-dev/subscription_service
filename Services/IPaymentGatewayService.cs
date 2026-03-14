using Microsoft.AspNetCore.OData.Deltas;
using subscription_service.DTOs;
using subscription_service.Models;

namespace subscription_service.Services;

public interface IPaymentGatewayService
{
    IQueryable<PaymentGateway> Query();

    Task<PaymentGateway> CreateAsync(PaymentGatewayCreateDto dto);

    Task<PaymentGateway?> PatchAsync(Guid id, Delta<PaymentGateway> delta);

    Task<bool> DeleteAsync(Guid id);
}

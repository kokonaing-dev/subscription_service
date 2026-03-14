using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using subscription_service.Data;
using subscription_service.Models;
using subscription_service.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Run API on port 8000 ---
builder.WebHost.UseUrls("http://0.0.0.0:8000");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionTransactionService, SubscriptionTransactionService>();
builder.Services.AddScoped<IGatewayRawEventService, GatewayRawEventService>();
builder.Services.AddScoped<IUserAIPointService, UserAIPointService>();
builder.Services.AddScoped<IAIPointTransactionService, AIPointTransactionService>();

// --- Configure OData ---
builder.Services.AddControllers()
    .AddOData(opt =>
        opt.Select()
            .Filter()
            .Expand()
            .OrderBy()
            .Count()
            .SetMaxTop(100)
            .AddRouteComponents("odata", GetEdmModel()))
    .AddMvcOptions(options =>
    {
        foreach (var formatter in options.InputFormatters.OfType<ODataInputFormatter>())
        {
            formatter.SupportedMediaTypes.Add("application/json");
            formatter.SupportedMediaTypes.Add("application/json;odata.metadata=minimal");
            formatter.SupportedMediaTypes.Add("application/json;odata.metadata=minimal;odata.streaming=true");
        }
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

// --- EDM Model for OData ---
static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    builder.EntitySet<PaymentGateway>("payment-gateways");
    builder.EntitySet<Discount>("discounts");
    builder.EntitySet<Plan>("plans");
    builder.EntitySet<Subscription>("subscriptions");
    builder.EntitySet<SubscriptionTransaction>("subscription-transactions");
    builder.EntitySet<GatewayRawEvent>("gateway-raw-events");
    builder.EntitySet<UserAIPoint>("user-ai-points");
    builder.EntitySet<AIPointTransaction>("ai-point-transactions");

    return builder.GetEdmModel();
}

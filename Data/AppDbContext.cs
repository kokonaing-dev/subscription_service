using Microsoft.EntityFrameworkCore;
using subscription_service.Models;

namespace subscription_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<PaymentGateway> PaymentGateways { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionTransaction> SubscriptionTransactions { get; set; }
    public DbSet<GatewayRawEvent> GatewayRawEvents { get; set; }
    public DbSet<UserAIPoint> UserAIPoints { get; set; }
    public DbSet<AIPointTransaction> AIPointTransactions { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Plan>()
            .HasOne(p => p.PaymentGateway)
            .WithMany(g => g.Plans)
            .HasForeignKey(p => p.PaymentGatewayId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Plan>()
            .HasOne(p => p.Discount)
            .WithMany(d => d.Plans)
            .HasForeignKey(p => p.DiscountId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Plan)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(s => s.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.Transaction)
            .WithOne(t => t.Subscription)
            .HasForeignKey<Subscription>(s => s.TransactionId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<SubscriptionTransaction>()
            .HasOne(t => t.GatewayRawEvent)
            .WithMany(e => e.SubscriptionTransactions)
            .HasForeignKey(t => t.GatewayRawEventId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<SubscriptionTransaction>()
            .HasOne(t => t.PaymentGateway)
            .WithMany()
            .HasForeignKey(t => t.PaymentGatewayId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<GatewayRawEvent>()
            .HasIndex(e => e.ProviderEventId)
            .IsUnique();

        modelBuilder.Entity<AIPointTransaction>()
            .HasOne(t => t.UserAIPoint)
            .WithMany(u => u.AIPointTransactions)
            .HasForeignKey(t => t.UserAIPointId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AIPointTransaction>()
            .HasOne(t => t.SubscriptionTransaction)
            .WithMany()
            .HasForeignKey(t => t.SubscriptionTransactionNo)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<AIPointTransaction>()
            .HasOne(t => t.PaymentGateway)
            .WithMany()
            .HasForeignKey(t => t.PaymentGatewayId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

using EKYCWebhook.Entity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EKYCWebhook.Entity;
public class EKYCWebhookContext : IdentityDbContext<EKYCWebhookUsercs, Role, string>
{
    public EKYCWebhookContext(DbContextOptions<EKYCWebhookContext> options)
        : base(options)
    {
    }

    public DbSet<kyc_verification> kyc_verification { get; set; }
    public DbSet<OcrBankStatement> OcrBankStatement { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
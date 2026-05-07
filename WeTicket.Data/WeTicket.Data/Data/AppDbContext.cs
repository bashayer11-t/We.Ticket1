using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WeTicket.Data.Models;


namespace WeTicket.Data.Data;

// هذا الكلاس هو المسؤول عن تشغيل الـ Migrations فقط
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        // تأكدي أن هذا هو نفس الـ Connection String اللي في appsettings.json
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=MyDatabaseName;User Id=sa;Password=YourStrong@Password;TrustServerCertificate=True");

        return new AppDbContext(optionsBuilder.Options);
    }
}

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    // احذفي OnConfiguring من هنا لأننا نمرر الـ options عن طريق الـ Constructor 
    // وهذا هو الأسلوب الأصح في ASP.NET Core

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // إعدادات الجداول كما هي...
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

        builder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

public class AppDbContextImpl(DbContextOptions<AppDbContext> options) : AppDbContext(options)
{
}

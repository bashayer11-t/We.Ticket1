using Microsoft.EntityFrameworkCore;
using WeTicket.API.Extensions;
using WeTicket.Data.Configurations;
using WeTicket.Services.Configurations;
using Scalar.AspNetCore;
using WeTicket.Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeTicket.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. إعداد قاعدة البيانات
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. إعداد الطبقات (Layers)
builder.Services.AddProjectDataLayer(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddApiLayer(builder.Configuration);

// 3. إعداد سياسة CORS (تأكدي من إضافة جميع البورتات المحتملة للرياكت)
builder.Services.AddCors(options =>
{
    options.AddPolicy("WeTicket", policy => 
    {
        /* policy.WithOrigins(
                "http://localhost:5173", 
                "http://localhost:5174", 
                "http://localhost:5175", 
                "http://localhost:5176"
            )*/
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// 4. إعداد بيئة التطوير (Swagger/Scalar)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// 5. ترتيب الـ Middleware (الترتيب هنا حاسم جداً)

// أولاً: تفعيل CORS قبل أي شيء آخر
app.UseCors("WeTicket");

app.UseHttpsRedirection();

// ثانياً: التوثيق والصلاحيات
app.UseAuthentication();
app.UseAuthorization();

// ثالثاً: ربط الـ Controllers
app.MapControllers(); 

// ملاحظة: قمت بإزالة RequireAuthorization() من هنا لأنها ستغلق صفحة التسجيل واللوجن.
// بدلاً من ذلك، ضعي [Authorize] فوق الـ Controllers التي تحتاج حماية فقط.

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // 1. إنشاء رتبة Admin إذا لم تكن موجودة
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // 2. إضافة رتبة Admin لمستخدم معين (اكتبي إيميلك هنا)
    var user = await userManager.FindByEmailAsync("adminn@weticket.com");
    if (user != null && !await userManager.IsInRoleAsync(user, "Admin"))
    {
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
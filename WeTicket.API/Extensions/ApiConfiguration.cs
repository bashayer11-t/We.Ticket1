using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeTicket.Data.Data;
using WeTicket.Data.DTOs;
using WeTicket.Data.Models;
using WeTicket.Services.IService;
using WeTicket.Services.Service;

namespace WeTicket.API.Extensions;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        #region Configure Identity & JWT
        // ربط إعدادات JWT من ملف appsettings.json
        // ملاحظة: تأكدي من وجود كلاس اسمه JWT في مجلد DTOs
        services.Configure<JWT>(configuration.GetSection("JWT"));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        #endregion

        #region Register Business Services (DI)
        // إضافة جميع الخدمات التي قمنا بإنشائها سابقاً لعمل Dependency Injection
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ITicketService, TicketService>();
        #endregion

        services.AddControllers();
        services.AddOpenApi();

        #region Configure CORS
        services.AddCors(options =>
        {
            options.AddPolicy("WeTicketPolicy", builder => // تغيير اسم السياسة لـ WeTicket
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        #endregion

        #region JWT Authentication Setup
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {

            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),


                ClockSkew = TimeSpan.Zero
            };
        });
        #endregion

        return services;
    }
}
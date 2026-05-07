using Microsoft.Extensions.DependencyInjection;
using WeTicket.Services.IService;
using WeTicket.Services.Service;

namespace WeTicket.Services.Configurations;
public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }
}
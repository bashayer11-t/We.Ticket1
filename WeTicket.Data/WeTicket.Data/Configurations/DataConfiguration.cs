using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeTicket.Data.Data;
using WeTicket.Data.Data;

namespace WeTicket.Data.Configurations;

public static class DataConfiguration
{
    public static IServiceCollection AddProjectDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}

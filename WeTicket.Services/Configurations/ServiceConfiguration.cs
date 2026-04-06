using Microsoft.Extensions.DependencyInjection;
using WeTicket.Services.IService;
//using WeTicket.Services.Service;

namespace WeTicket.Services.Configurations;
public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<IAuthService, AuthService>();
        //services.AddScoped<IGenreService, GenreService>();
        //services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}
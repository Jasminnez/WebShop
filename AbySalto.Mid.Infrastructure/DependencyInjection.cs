using AbySalto.Mid.Infrastructure.Persistence;
using AbySalto.Mid.Infrastructure.Services.Interfaces;
using AbySalto.Mid.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AbySalto.Mid.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddServices();
            
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
             services.AddScoped<IJwtService, JwtService>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
             services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("AbySalto.Mid.WebApi")));
                
            return services;
        }
    }
}

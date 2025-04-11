using AbySalto.Mid.Application.Interfaces;
using AbySalto.Mid.Application.Services;
using Microsoft.Extensions.DependencyInjection;
namespace AbySalto.Mid.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor(); 
            return services;
        }
    }
}

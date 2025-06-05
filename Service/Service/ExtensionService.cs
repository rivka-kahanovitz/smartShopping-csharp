using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.DTOs;
using Service.Interfaces;
using Service.Service;

namespace Service
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IService<UserLoginDto>, UserloginService>();
            services.AddScoped<IService<UserSignUpDto>, UserSignUpService>();

            // תוכל/י להוסיף כאן שירותים נוספים בהמשך לדוגמה:
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<User>, UserRepository>();

            return services;
        }
    }
}

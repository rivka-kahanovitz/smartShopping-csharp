using Microsoft.Extensions.DependencyInjection;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using common.DTOs;
using common.Interfaces;
using Service.Service;

namespace Service
{
    public static class ExtensionService
    {
        public static IServiceCollection AddServiceExtension(this IServiceCollection services)
        {
            services.AddScoped<IService<UserLoginDto>, UserloginService>();
            services.AddScoped<IService<UserSignUpDto>, UserSignUpService>();
            services.AddScoped<IService<ShoppingListDto>, ShoppingListService>();
            services.AddScoped<IService<ProductDto>, ProductService>();


            // תוכל/י להוסיף כאן שירותים נוספים בהמשך לדוגמה:
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<ShoppingList>, ShoppingListsRepository>();
            services.AddScoped<IRepository<ShoppingListItem>, ShoppingListItemRpository>();

            services.AddScoped<IRepository<Product>, ProductRepository>();

            return services;
        }
    }
}

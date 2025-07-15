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
            services.AddScoped<IService <UserDto>, UserService>();
            services.AddScoped<IService <ShoppingListItemDto>, ShoppingListItemService>();
            services.AddScoped<IService <StoreDto>, StoreService>();
            services.AddScoped<IService <AllProductStoreDto>, AllProductStoreService>();


            // תוכל/י להוסיף כאן שירותים נוספים בהמשך לדוגמה:
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<ShoppingList>, ShoppingListsRepository>();
            services.AddScoped<IRepository<ShoppingListItem>, ShoppingListItemRpository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Stores>, StoresRepository>();
            services.AddScoped<IRepository<AllProductsStores>, AllProductStoresRepository>();

            return services;
        }
    }
}

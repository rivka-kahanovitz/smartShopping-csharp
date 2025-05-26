

using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository.Interfaces
{
    public interface IContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        void Save();
    }
}

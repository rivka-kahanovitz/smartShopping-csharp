using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;



namespace Mock
{
    public class DataBase : DbContext   
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-SSNMLFD;database=SmartShoppingApplication;trusted_connection=true;TrustServerCertificate=True");
        }
    }
}

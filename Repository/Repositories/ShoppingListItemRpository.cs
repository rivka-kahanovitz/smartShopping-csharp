using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ShoppingListItemRpository : IRepository<ShoppingListItem>
    {
        private readonly IContext context;
        public ShoppingListItemRpository(IContext context)
        {
            this.context = context;
        }
        public ShoppingListItem Add(ShoppingListItem item)
        {
            context.ShoppingListItems.Add(item);
            context.Save();
            return item;
        }
        public List<ShoppingListItem> GetAll()
        {
            return context.ShoppingListItems.ToList();
        }
        public ShoppingListItem GetById(int id)
        {
            return context.ShoppingListItems.FirstOrDefault(x => x.Id == id);
        }

        public ShoppingListItem Put(int id, ShoppingListItem item)
        {
            var user = context.ShoppingListItems.FirstOrDefault(x => x.Id == id);
            user.Id = item.Id;
            user.ListId = item.ListId;
            user.ShoppingList = item.ShoppingList;
            user.ProductId = item.ProductId;
            user.Product = item.Product;
            user.Quantity = item.Quantity;
            context.Save();
            return user;
        }
        public void Delete(int id)
        {
            context.ShoppingListItems.Remove(GetById(id));
            context.Save();
        }

    }
}

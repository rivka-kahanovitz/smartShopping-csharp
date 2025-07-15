using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ShoppingListsRepository : IRepository<ShoppingList>
    {
        private readonly IContext context;
        public ShoppingListsRepository(IContext context)
        {
            this.context = context;
        }
        public ShoppingList Add(ShoppingList item)
        {
            context.ShoppingLists.Add(item);
            context.Save();
            return item;
        }
        public List<ShoppingList> GetAll()
        {
            if(context.ShoppingLists == null)
                return new List<ShoppingList>();
            return context.ShoppingLists.ToList();
        }
        public ShoppingList GetById(int id)
        {
            return context.ShoppingLists
           .Include(l => l.ShoppingListItems)
           .ThenInclude(i => i.Product)
           .FirstOrDefault(l => l.Id == id);
        }

        public ShoppingList Put(int id, ShoppingList item)
        {
            var ShoppingList = context.ShoppingLists.FirstOrDefault(x => x.Id == id);
            ShoppingList.Id = item.Id;
            ShoppingList.UserId= item.UserId;
            ShoppingList.Title = item.Title;
            ShoppingList.User = item.User;
            context.Save();
            return ShoppingList;
        }
        public void Delete(int id)
        {
            context.ShoppingLists.Remove(GetById(id));
            context.Save();
        }

    }
}

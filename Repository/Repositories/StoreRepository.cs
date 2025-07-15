using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class StoresRepository : IRepository<Stores>
    {
        private readonly IContext context;

        public StoresRepository(IContext context)
        {
            this.context = context;
        }

        public Stores Add(Stores item)
        {
            context.Stores.Add(item);
            context.Save();
            return item;
        }

        public List<Stores> GetAll()
        {
            return context.Stores.ToList();
        }

        public Stores GetById(int id)
        {
            return context.Stores.FirstOrDefault(s => s.Id == id);
        }

        public Stores Put(int id, Stores item)
        {
            var existing = context.Stores.FirstOrDefault(s => s.Id == id);
            if (existing == null)
                return null;

            existing.StorName = item.StorName;
            existing.BranchName = item.BranchName;
            existing.Address = item.Address;
            // לא נעדכן את AllProductsStores כאן אלא אם כן זה דרוש

            context.Save();
            return existing;
        }

        public void Delete(int id)
        {
            var store = GetById(id);
            if (store != null)
            {
                context.Stores.Remove(store);
                context.Save();
            }
        }
    }
}

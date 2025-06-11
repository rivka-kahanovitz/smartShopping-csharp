using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StoreRepository: IRepository<Stores>
    {
        private readonly IContext context;
        public StoreRepository(IContext context)
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
            return context.Stores.FirstOrDefault(x => x.Id == id);
        }

        public Stores Put(int id, Stores item)
        {
            var store = context.Stores.FirstOrDefault(x => x.Id == id);
            store.Id = item.Id;
            store.Name = item.Name;
            store.Address = item.Chain;
            store.Chain = item.Chain;
            return store;
        }
        public void Delete(int id)
        {
            context.Stores.Remove(GetById(id));
            context.Save();
        }
    }
}


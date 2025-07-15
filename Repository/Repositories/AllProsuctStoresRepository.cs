using Repository.Entities;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class AllProductStoresRepository : IRepository<AllProductsStores>
    {
        private readonly IContext context;

        public AllProductStoresRepository(IContext context)
        {
            this.context = context;
        }

        public AllProductsStores Add(AllProductsStores item)
        {
            context.AllProductsStores.Add(item);
            context.Save();
            return item;
        }

        public List<AllProductsStores> GetAll()
        {
            return context.AllProductsStores.ToList();
        }

        public AllProductsStores GetById(int id)
        {
            return context.AllProductsStores.FirstOrDefault(x => x.Id == id);
        }

        public AllProductsStores Put(int id, AllProductsStores item)
        {
            var entity = context.AllProductsStores.FirstOrDefault(x => x.Id == id);
            if (entity == null)
                return null;

            entity.Barcode = item.Barcode;
            entity.StoreId = item.StoreId;
            entity.Stores = item.Stores;
            context.Save();
            return entity;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                context.AllProductsStores.Remove(entity);
                context.Save();
            }
        }
    }
}

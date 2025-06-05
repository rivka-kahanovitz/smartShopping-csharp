using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Interfaces;
namespace Repository.Repositories
{   
    public class ProductRepository : IRepository<Product>
    {
        private readonly IContext context;
        public ProductRepository(IContext context)
        {
            this.context = context;
        }
        public Product Add(Product item)
        {
            context.Products.Add(item);
            context.Save();
            return item;
        }
        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }
        public Product GetById(int id)
        {
            return context.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product Put(int id, Product item)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == id);
            product.Id = item.Id;
            product.Name = item.Name;
            product.Brand = item.Brand;
            product.Barcode = item.Barcode;
            product.ImageUrl = item.ImageUrl;
            product.Category = item.Category;
            return product;
        }
        public void Delete(int id)
        {
            context.Products.Remove(GetById(id));
            context.Save();
        }
    }
}

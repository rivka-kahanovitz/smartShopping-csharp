using AutoMapper;
using common.DTOs;
using common.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Service
{
    public class ProductService : IService<ProductDto>
    {
        private int userID;

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ShoppingList> _shoppingListRepository;
        private readonly IRepository<ShoppingListItem> _shoppingListItemRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<ShoppingList> shoppingListRepository,
            IRepository<ShoppingListItem> shoppingListItemRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _shoppingListRepository = shoppingListRepository;
            _shoppingListItemRepository = shoppingListItemRepository;
            _mapper = mapper;
        }

        public void UserIdForAddIrem(int userId)
        {
            userID =userId;
        }
        public ProductDto AddItem(ProductDto item)
        {
            // מיפוי ושמירת המוצר
            var productEntity = _mapper.Map<Product>(item);
            var savedProduct = _productRepository.Add(productEntity);

            int userId =this.userID;
            // מביא או יוצר רשימת קניות
            var shoppingList = GetOrCreateUserShoppingList(userId);

            // מוסיף את הפריט לרשימה
            var shoppingItem = new ShoppingListItem
            {
                ProductId = savedProduct.Id,
                ListId = shoppingList.Id,
                Quantity = 1
            };

            _shoppingListItemRepository.Add(shoppingItem);

            return _mapper.Map<ProductDto>(savedProduct);
        }
        public void Delete(int id)
        {
            _productRepository.Delete(id);
        }

        public List<ProductDto> GetAll()
        {
            var products = _productRepository.GetAll();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public ProductDto GetById(int id)
        {
            var product = _productRepository.GetById(id);
            return _mapper.Map<ProductDto>(product);
        }

        public ProductDto Update(int id, ProductDto item)
        {
            var existing = _productRepository.GetById(id);
            if (existing == null)
                throw new Exception("Product not found");

            // עדכון שדות
            existing.Name = item.Name;
            existing.Category = item.Category;
            existing.ImageUrl = item.ImageUrl;
            existing.Barcode = item.Barcode;

            var productEntity = _mapper.Map<Product>(item);
            var updated = _productRepository.Put(existing.Id, productEntity);
            return _mapper.Map<ProductDto>(updated);
        }

        private ShoppingList GetOrCreateUserShoppingList(int userId)
        {
            var list = _shoppingListRepository.GetAll()
                .FirstOrDefault(l => l.UserId == userId);

            if (list == null)
            {
                list = new ShoppingList
                {
                    UserId = userId,
                    Title = "רשימת קניות שלי"
                };
                list = _shoppingListRepository.Add(list);
            }

            return list;
        }
    }
}

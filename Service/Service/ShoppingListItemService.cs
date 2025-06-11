using AutoMapper;
using common.DTOs;
using common.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using System.Collections.Generic;

namespace Service.Service
{
    public class ShoppingListItemService : IService<ShoppingListItemDto>
    {
        private readonly IRepository<ShoppingListItem> _repository;
        private readonly IMapper _mapper;

        public ShoppingListItemService(IRepository<ShoppingListItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ShoppingListItemDto AddItem(ShoppingListItemDto item)
        {
            // ממפה רק את המידע הנדרש, בלי ליצור Product חדש בטעות
            var entity = _mapper.Map<ShoppingListItem>(item);
            entity.Product = null; // חשוב! כדי למנוע שגיאת ברקוד ריק

            var added = _repository.Add(entity);
            return _mapper.Map<ShoppingListItemDto>(added);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<ShoppingListItemDto> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<List<ShoppingListItemDto>>(entities);
        }

        public ShoppingListItemDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<ShoppingListItemDto>(entity);
        }

        public ShoppingListItemDto Update(int id, ShoppingListItemDto item)
        {
            var entity = _mapper.Map<ShoppingListItem>(item);
            entity.Product = null; // שוב – לא ליצור אובייקט חדש
            var updated = _repository.Put(id, entity);
            return _mapper.Map<ShoppingListItemDto>(updated);
        }
    }
}

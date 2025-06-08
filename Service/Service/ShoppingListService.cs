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
    public class ShoppingListService : IService<ShoppingListDto>
    {
        //כשיהיה קונטרולר לשים שם את הפונקציה הזאת
                /*/
        [Authorize]
        [HttpPost]
        public ActionResult<ShoppingListDto> Create([FromForm] ShoppingListDto dto)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);

            _shoppingListService.SetUserId(userId); // בדיוק כמו שעשית עם המוצר

            var newList = _shoppingListService.AddItem(dto);
            return Ok(newList);
        }
        */
        private int userId;

        private readonly IRepository<ShoppingList> _shoppingListRepository;
        private readonly IRepository<ShoppingListItem> _shoppingListItemRepository;
        private readonly IMapper _mapper;

        public ShoppingListService(
            IRepository<ShoppingList> shoppingListRepository,
            IRepository<ShoppingListItem> shoppingListItemRepository,
            IMapper mapper)
        {
            _shoppingListRepository = shoppingListRepository;
            _shoppingListItemRepository = shoppingListItemRepository;
            _mapper = mapper;
        }

        public void SetUserId(int id)
        {
            userId = id;
        }

        public ShoppingListDto AddItem(ShoppingListDto item)
        {
            var entity = _mapper.Map<ShoppingList>(item);
            entity.UserId = userId;

            var added = _shoppingListRepository.Add(entity);
            return _mapper.Map<ShoppingListDto>(added);
        }

        public void Delete(int id)
        {
            var list = _shoppingListRepository.GetById(id);
            if (list == null)
                throw new Exception("Shopping list not found");

            var relatedItems = _shoppingListItemRepository.GetAll()
                .Where(item => item.ListId == id)
                .ToList();

            foreach (var item in relatedItems)
            {
                _shoppingListItemRepository.Delete(item.Id);
            }

            _shoppingListRepository.Delete(id);
        }

        public List<ShoppingListDto> GetAll()
        {
            var allLists = _shoppingListRepository.GetAll()
                .Where(l => l.UserId == userId)
                .ToList();

            return _mapper.Map<List<ShoppingListDto>>(allLists);
        }

        public ShoppingListDto GetById(int id)
        {
            var list = _shoppingListRepository.GetById(id);
            if (list == null || list.UserId != userId)
                throw new Exception("Shopping list not found");

            return _mapper.Map<ShoppingListDto>(list);
        }

        public ShoppingListDto Update(int id, ShoppingListDto item)
        {
            var existing = _shoppingListRepository.GetById(id);
            if (existing == null || existing.UserId != userId)
                throw new Exception("Shopping list not found");

            existing.Title = item.Title;

            var updated = _shoppingListRepository.Put(id, existing);
            return _mapper.Map<ShoppingListDto>(updated);
        }
    }
}

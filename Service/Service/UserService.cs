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
    public class UserService : IService<UserDto>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserDto AddItem(UserDto item)
        {
            // נוודא שלא ננסה למפות Id ידני
            var user = _mapper.Map<User>(item);
            user.Id = 0; // אפס את ה־Id כדי למנוע ניסיון לעדכון במקום Insert
            var added = _repository.Add(user);
            return _mapper.Map<UserDto>(added);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<UserDto> GetAll()
        {
            return _mapper.Map<List<UserDto>>(_repository.GetAll());
        }

        public UserDto GetById(int id)
        {
            var user = _repository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto Update(int id, UserDto item)
        {
            var existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception("User not found");

            existing.Name = item.Name;
            existing.Email = item.Email;
            // existing.Password = ...  ← אם צריך

            var updated = _repository.Put(id, existing);
            return _mapper.Map<UserDto>(updated);
        }

    }
}

using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using common.DTOs;
using common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Service
{
    public class UserSignUpService : IService<UserSignUpDto>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserSignUpService(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public UserSignUpDto AddItem(UserSignUpDto dto)
        {
            if (_repository.GetAll().Any(u => u.Email == dto.Email))
                throw new InvalidOperationException("כתובת האימייל כבר קיימת.");

            dto.Password = PasswordHasher.Hash(dto.Password);

            var user = _mapper.Map<User>(dto);
            _repository.Add(user);

            return dto;
        }

        public void Delete(int id)
        {
            var existingUser = _repository.GetById(id);
            if (existingUser == null)
                throw new KeyNotFoundException("משתמש לא נמצא.");

            _repository.Delete(id);
        }

        public List<UserSignUpDto> GetAll()
        {
            var users = _repository.GetAll();
            return _mapper.Map<List<UserSignUpDto>>(users);
        }

        public UserSignUpDto GetById(int id)
        {
            var user = _repository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("משתמש לא נמצא.");

            return _mapper.Map<UserSignUpDto>(user);
        }

        public UserSignUpDto Update(int id, UserSignUpDto dto)
        {
            var existingUser = _repository.GetById(id);
            if (existingUser == null)
                throw new KeyNotFoundException("משתמש לא נמצא.");

            dto.Password = PasswordHasher.Hash(dto.Password);

            var updatedUser = _mapper.Map<User>(dto);
            updatedUser.Id = id; // חשוב – לא לאבד את המזהה!

            var result = _repository.Put(id, updatedUser);
            return _mapper.Map<UserSignUpDto>(result);
        }
    }
}

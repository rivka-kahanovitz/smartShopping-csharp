using AutoMapper;
using common.DTOs;
using common.Interfaces;
using Humanizer;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public UserService(IUserRepository repository, IMapper mapper, IEmailSender emailSender)
        {
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
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
            var updated = _repository.Put(id, existing);
            return _mapper.Map<UserDto>(updated);
        }

        // איפוס סיסמה בסיסי (שליחת סיסמה חדשה במייל)
        public async Task ResetPasswordAsync(string email)
        {
            var user = _repository.GetByEmail(email);
            if (user is null)
                throw new Exception("המייל לא קיים במערכת");

            var newPwd = GenerateRandomPassword();
            user.Password = PasswordHasher.Hash(newPwd);
            _repository.Put(user.Id, user);

            await _emailSender.SendAsync(
                to: email,
                subject: "סיסמה חדשה",
                body: $"הסיסמה החדשה שלך: {newPwd}"
            );
        }

        private static string GenerateRandomPassword(int len = 8)
        {
            const string valid = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
            var rng = new Random();
            return new string(Enumerable.Range(0, len)
                                        .Select(_ => valid[rng.Next(valid.Length)])
                                        .ToArray());
        }
    }
}

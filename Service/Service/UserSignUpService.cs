using AutoMapper;
using Repository.Entities;
using Repository.Interfaces;
using Service.DTOs;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using Service;
namespace Service.Service
{
    internal class UserSignUpService : IService<UserSignUpDto>
    {
        private readonly IContext _context;

        public UserSignUpService(IContext context)
        {
            _context = context;
        }

        public UserSignUpDto AddItem(UserSignUpDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                throw new InvalidOperationException("כתובת האימייל כבר קיימת.");

            var hashedPassword = PasswordHasher.Hash(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            _context.Save();

            return dto;
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new KeyNotFoundException("משתמש לא נמצא.");

            _context.Users.Remove(user);
            _context.Save();
        }

        public List<UserSignUpDto> GetAll()
        {
            return _context.Users
                .Select(u => new UserSignUpDto
                {
                    Name = u.Name,
                    Email = u.Email,
                    Password = "********"
                }).ToList();
        }

        public UserSignUpDto GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new KeyNotFoundException("משתמש לא נמצא.");

            return new UserSignUpDto
            {
                Name = user.Name,
                Email = user.Email,
                Password = "********"
            };
        }

        public UserSignUpDto Update(int id, UserSignUpDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new KeyNotFoundException("משתמש לא נמצא.");

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = PasswordHasher.Hash(dto.Password);

            _context.Save();

            return dto;
        }
    }
}

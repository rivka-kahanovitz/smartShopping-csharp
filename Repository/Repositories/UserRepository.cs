using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Interfaces;
using System.Diagnostics;
namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IContext context;
        public UserRepository(IContext context)
        {
            this.context = context;
        }
        public User Add(User item)
        {
            context.Users.Add(item);
            context.Save();
            return item;
        }
        public List<User> GetAll()
        {
            return context.Users.ToList();
        }
        public User GetById(int id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            Console.WriteLine(user); // או Console.WriteLine אם את בודקת בקונסול
            return user;

        }
        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User Put(int id, User item)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
                throw new Exception("User not found");

           
            user.Name = item.Name;
            user.Email = item.Email;
            user.Password = item.Password;

            context.Save();
            return user;
        }

        public void Delete(int id)
        {
            context.Users.Remove(GetById(id));
            context.Save();
        }
    }
}

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
    public class UserRepository : IRepository<User>
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
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Put(int id, User item)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);
            user.Id = item.Id;
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

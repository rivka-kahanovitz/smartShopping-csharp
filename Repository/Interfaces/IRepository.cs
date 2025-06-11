using System.Collections.Generic;

namespace Repository.Interfaces
{
        public interface IRepository<T>
        {
            T Add(T item);
            List<T> GetAll();
            T GetById(int id);
            T Put(int id, T item);
            void Delete(int id);
        }
}


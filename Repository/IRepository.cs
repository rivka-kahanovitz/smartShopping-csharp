
using System.Collections.Generic;

namespace Repository
{
        public interface IRepository<T>
        {
            int Create(T item);
            T Read(int id);
            bool Update(int id, T item);
            bool Delete(int id);
        }
}


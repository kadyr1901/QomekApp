using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QomekCore.Repository
{
    public interface IQomekRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void DeleteAsync(int id);
        Task<T?> Get(int id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QomekCore.Repository
{
    public interface IQomekRepository<T> where T : class
    {
        T AddAsync(T entity);
        T Edit(T entity);
        void DeleteAsync(int id);
        T Get(int id);

    }
}

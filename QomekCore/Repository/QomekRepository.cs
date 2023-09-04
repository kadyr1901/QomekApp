using QomekData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QomekCore.Repository
{
    public class QomekRepository<T> : IQomekRepository<T> where T : class
    {
        private readonly BlogDbContext _db;
        public QomekRepository(BlogDbContext db)
        {
            _db = db;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            await _db.RemoveAsync(id);
        }

        public T Edit(T entity)
        {
            _db.Update(entity);
            return entity;
        }

        public T Get(int id)
        {
            
        }
    }
}

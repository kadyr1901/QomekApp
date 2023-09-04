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
        private protected readonly QomekDbContext _db;
        public QomekRepository(QomekDbContext db)
        {
            _db = db;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public void DeleteAsync(int id)
        {
            _db.Remove(id);
        }

        public T Update(T entity)
        {
            _db.Update(entity);
            return entity;
        }

        public async Task<T?> Get(int id)
        {
            return await _db.FindAsync<T>(id);
        }
    }
}

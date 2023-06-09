using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anzas.DAL.Services
{
    public class RockRepository : IRepository<Rock>
    {
        private readonly AnzasContext _dbContext;

        public RockRepository(AnzasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Rock Add(Rock item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.Rocks.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public async Task AddAsync(Rock item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.Rocks.Add(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public IEnumerable<Rock> GetAll()
        {
            return _dbContext.Rocks
                    .Include(item => item.GeologNavigation)
                    .Include(item => item.RockCodeNavigation).AsNoTracking().ToList();
        }

        public Rock GetById(int id)
        {
            return _dbContext.Rocks.SingleOrDefault(i => i.Uid == id);
        }

        public async Task<Rock> GetByIdAsync(int id)
        {
            return await _dbContext.Rocks.FirstOrDefaultAsync(i => i.Uid == id);
        }

        public int Max()
        {
            return _dbContext.Rocks.Max(i => i.Uid);
        }

        public void Remove(int id)
        {
            var item = _dbContext.Rocks.FirstOrDefault(i => i.Uid == id);

            _dbContext.Rocks.Remove(item);
            _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            var item = _dbContext.Rocks.FirstOrDefault(i => i.Uid == id);

            _dbContext.Rocks.Remove(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Update(Rock item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.Rocks.Update(item);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(Rock item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.Rocks.Update(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anzas.DAL.Services
{
    public class InfoRouteRepository : IRepository<InfoRoute>
    {
        private readonly AnzasContext _dbContext;

        public InfoRouteRepository(AnzasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public InfoRoute Add(InfoRoute item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoRoutes.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public async Task AddAsync(InfoRoute item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoRoutes.Add(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public IEnumerable<InfoRoute> GetAll()
        {
            return _dbContext.InfoRoutes
                .Include(p => p.PlaceSiteNavigation)
                .Include(item => item.TypeLcodeNavigation)
                .Include(item => item.GeologNavigation).AsNoTracking().ToList();
        }

        public InfoRoute GetById(int id)
        {
            return _dbContext.InfoRoutes.SingleOrDefault(i => i.Uid == id);
        }

        public async Task<InfoRoute> GetByIdAsync(int id)
        {
            return await _dbContext.InfoRoutes.FirstOrDefaultAsync(i => i.Uid == id);
        }

        public int Max()
        {
            return _dbContext.InfoRoutes.Max(i => i.Uid);
        }

        public void Remove(int id)
        {
            var item = _dbContext.InfoRoutes.FirstOrDefault(i => i.Uid == id);

            _dbContext.InfoRoutes.Remove(item);
            _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            var item = _dbContext.InfoRoutes.FirstOrDefault(i => i.Uid == id);

            _dbContext.InfoRoutes.Remove(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Update(InfoRoute item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoRoutes.Update(item);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(InfoRoute item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoRoutes.Update(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}


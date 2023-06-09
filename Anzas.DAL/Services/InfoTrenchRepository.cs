using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anzas.DAL.Services
{
    public  class InfoTrenchRepository : IRepository<InfoTrench>
    {
        private readonly AnzasContext _dbContext;

        public InfoTrenchRepository(AnzasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public InfoTrench Add(InfoTrench item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoTrenches.Add(item);
            _dbContext.SaveChanges();
            return item;
        }
        
        public async Task AddAsync(InfoTrench item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoTrenches.Add(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public IEnumerable<InfoTrench> GetAll()
        {
            return _dbContext.InfoTrenches
                .Include(p => p.PlaceSiteNavigation)
                .Include(item => item.TypeLcodeNavigation)
                .Include(item => item.GeologNavigation).AsNoTracking().ToList();
        }

        public InfoTrench GetById(int id)
        {
            return _dbContext.InfoTrenches.SingleOrDefault(i => i.Uid == id);
        }

        public async Task<InfoTrench> GetByIdAsync(int id)
        {
            return await _dbContext.InfoTrenches.FirstOrDefaultAsync(i => i.Uid == id);
        }

        public int Max()
        {
            return _dbContext.InfoTrenches.Max(i => i.Uid);
        }

        public void Remove(int id)
        {
            var item = _dbContext.InfoTrenches.FirstOrDefault(i => i.Uid == id);

            _dbContext.InfoTrenches.Remove(item);
            _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            var item = _dbContext.InfoTrenches.FirstOrDefault(i => i.Uid == id);

            _dbContext.InfoTrenches.Remove(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Update(InfoTrench item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoTrenches.Update(item);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(InfoTrench item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoTrenches.Update(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }
    }
}

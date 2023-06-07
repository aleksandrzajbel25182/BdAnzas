using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anzas.DAL.Services
{
    public class InfodrillRepository : IRepository<InfoDrill>
    {
        private readonly AnzasContext _dbContext;

        public InfodrillRepository(AnzasContext dbContext)
        {
            _dbContext = dbContext;
        }

        public InfoDrill Add(InfoDrill item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoDrills.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public async Task AddAsync(InfoDrill item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoDrills.Add(item);
             await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public IEnumerable<InfoDrill> GetAll()
        {
            return _dbContext.InfoDrills
                .Include(p => p.PlaceSiteNavigation)
                .Include(item => item.TypeLcodeNavigation)
                .Include(item => item.GeologNavigation).AsNoTracking().ToList();
        }

        public InfoDrill GetById(int id)
        {
            return  _dbContext.InfoDrills.SingleOrDefault(i=>i.Uid == id);
        }

        public async Task<InfoDrill> GetByIdAsync(int id)
        {
            return await _dbContext.InfoDrills.FirstOrDefaultAsync(i => i.Uid == id);
        }

        public int Max()
        {
            return _dbContext.InfoDrills.Max(i => i.Uid);
        }

        public void Remove(int id)
        {
            var item = _dbContext.InfoDrills.FirstOrDefault(i => i.Uid == id);

            _dbContext.InfoDrills.Remove(item);
            _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            var item = _dbContext.InfoDrills.FirstOrDefault(i => i.Uid == id);

            _dbContext.InfoDrills.Remove(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Update(InfoDrill item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoDrills.Update(item);
            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(InfoDrill item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _dbContext.InfoDrills.Update(item);
            await _dbContext.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        //public InfoDrill Add(InfoDrill item)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task AddAsync(InfoDrill entity)
        //{
        //    await _dbContext.InfoDrills.AddAsync(entity);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(InfoDrill entity)
        //{
        //    _dbContext.InfoDrills.Remove(entity);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public IEnumerable<InfoDrill> GetAll()
        //{
        //    return _dbContext.InfoDrills
        //        .Include(p => p.PlaceSiteNavigation)
        //        .Include(item => item.TypeLcodeNavigation)
        //        .Include(item => item.GeologNavigation).AsNoTracking().ToList();
        //}



        //public async Task<InfoDrill> GetByIdAsync(int id)
        //{
        //    return await _dbContext.InfoDrills.FindAsync(id);
        //}

        //public async Task Maxsync()
        //{
        //    return await _dbContext.InfoDrills.Max(i=>i.Uid);
        //}

        //public void Remove(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveAsync(InfoDrill entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(InfoDrill item)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task UpdateAsync(InfoDrill entity)
        //{
        //    _dbContext.InfoDrills.Update(entity);
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}

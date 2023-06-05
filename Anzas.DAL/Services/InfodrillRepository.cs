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

        public async Task AddAsync(InfoDrill entity)
        {
            await _dbContext.InfoDrills.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(InfoDrill entity)
        {
            _dbContext.InfoDrills.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<InfoDrill> GetAll()
        {
            return _dbContext.InfoDrills
                .Include(p => p.PlaceSiteNavigation)
                .Include(item => item.TypeLcodeNavigation)
                .Include(item => item.GeologNavigation).AsNoTracking().ToList();
        }



        public async Task<InfoDrill> GetByIdAsync(int id)
        {
            return await _dbContext.InfoDrills.FindAsync(id);
        }

        public async Task UpdateAsync(InfoDrill entity)
        {
            _dbContext.InfoDrills.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anzas.DAL.Services
{
    internal class InfoTrenchRepository
    {
        //: IRepository<InfoTrench>
        //private readonly AnzasContext _dbContext;

        //public InfoTrenchRepository(AnzasContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        //public async Task AddAsync(InfoTrench entity)
        //{
        //    await _dbContext.InfoTrenches.AddAsync(entity);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(InfoTrench entity)
        //{
        //    _dbContext.InfoTrenches.Remove(entity);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public IEnumerable<InfoTrench> GetAll()
        //{
        //    return _dbContext.InfoTrenches
        //        .Include(item => item.TypeLcodeNavigation)
        //        .Include(item => item.GeologNavigation).AsNoTracking().ToList();
        //}



        //public async Task<InfoTrench> GetByIdAsync(int id)
        //{
        //    return await _dbContext.InfoTrenches.FindAsync(id);
        //}

        //public async Task UpdateAsync(InfoTrench entity)
        //{
        //    _dbContext.InfoTrenches.Update(entity);
        //    await _dbContext.SaveChangesAsync();
        //}
    }
}

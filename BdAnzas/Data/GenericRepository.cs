using Anzas.DAL;
using BdAnzas.Constants;
using BdAnzas.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BdAnzas.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AnzasContext _db;
        private readonly DbSet<TEntity> _Set;
        private readonly DialogManager _dialogManager;

        public GenericRepository(AnzasContext db)
        {
            _db = db;
            _Set = db.Set<TEntity>();
            _dialogManager = new DialogManager();
        }

        public void Add(TEntity item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Add(item);
            _db.SaveChanges();           
        }

        public async Task AddAsync(TEntity item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Add(item);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);

            _dialogManager.ShowMessage("Новая запись в базу данных успешно добавлена", "Запись в базу данных", InfoMessege.OK, InfoMessege.Information);
        }

        public IEnumerable<TEntity> Get() => _Set.AsNoTracking().ToList();       

        public TEntity FindById(Func<TEntity, bool> predicate)  =>_Set.SingleOrDefault(predicate);     

        public void Remove(Func<TEntity, bool> predicate)
        {
            var item = _Set.Local.FirstOrDefault(predicate);
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Remove(item);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(Func<TEntity, bool> predicate , CancellationToken Cancel = default)
        {
            var item = _Set.FirstOrDefault(predicate);
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Remove(item);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
        }

        public void Update(TEntity item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Update(item);
            _db.SaveChanges();
        }

        public async Task UpdateAsync(TEntity item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _Set.Update(item);
            var result = _dialogManager.ShowMessageResult("Были внесены изминения, вы хотите их сохранить?"
                                                      , "Обновление записи в базу данных"
                                                      , InfoMessege.YesNoCancel, InfoMessege.Information);
            if (result == MessageBoxResult.Yes)
                await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            _dialogManager.ShowMessage($"Данные успешно обновлены", "Обновление записи в базу данных", InfoMessege.OK, InfoMessege.Information);
            
        }


        /*-------*/
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _Set.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

       
    }
}

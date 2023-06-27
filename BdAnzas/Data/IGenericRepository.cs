using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BdAnzas.Data
{
    public interface IGenericRepository<TEntity>
    {
        /// <summary>
        /// Получаем данные
        /// </summary>
        /// <returns>На выходе получаем коллекцию в IEnumerable </returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Поиск по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(Func<TEntity, bool> predicate);      

        /// <summary>
        /// Добавляем данные 
        /// </summary>
        /// <param name="item">В качестве параметра получаем TEntity </param>
        void Add(TEntity item);

        Task AddAsync(TEntity item, CancellationToken Cancel = default);

        /// <summary>
        /// Обновляем данные 
        /// </summary>
        /// <param name="item">В качестве параметра получаем TEntity</param>
        void Update(TEntity item);

        Task UpdateAsync(TEntity item, CancellationToken Cancel = default);

        /// <summary>
        /// Удаляем данные
        /// </summary>
        /// <param name="id"></param>
        void Remove(Func<TEntity, bool> predicate);

        Task RemoveAsync(Func<TEntity, bool> predicate, CancellationToken Cancel = default);

        
    }
}

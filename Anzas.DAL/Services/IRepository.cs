using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anzas.DAL.Services
{
    
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        T GetById(int id);

        T Add(T item);
        Task AddAsync(T item , CancellationToken Cancel = default);

        void Update(T item);
        Task UpdateAsync(T item, CancellationToken Cancel = default);

        void Remove(int id);
        Task RemoveAsync(int id, CancellationToken Cancel = default);

        int Max();
    }
}

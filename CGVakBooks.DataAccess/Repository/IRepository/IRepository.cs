using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CGVakBooks.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null , string? includeproperties = null);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter , string? includeproperties = null);

        void Add(T item);

        void Remove(T item);

        void RemoveChange(IEnumerable<T> items);

    }
}

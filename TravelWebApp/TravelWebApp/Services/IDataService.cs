using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TravelWebApp.Services
{
    public interface IDataService<T>
    {
        IEnumerable<T> GetAll();
        void Create(T entity);
        T GetSingle(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
    }
}

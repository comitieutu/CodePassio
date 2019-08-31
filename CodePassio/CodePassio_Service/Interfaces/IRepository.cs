using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CodePassio_Service.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Entities { get; }

        void Create(T entity);

        void Edit(T entity);

        IQueryable<T> GetAll();

        T Get(params object[] keyValues);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        void Delete(params object[] keyValues);

        int Delete(Expression<Func<T, bool>> predicate);
        EntityEntry GetTracking(T entity);
        bool IsExist(Guid id);
    }
}

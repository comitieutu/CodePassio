using CodePassio_Service.Interfaces;
using CodePassio_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using CodePassio_Core;

namespace CodePassio_Service.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected DbSet<T> EntitySet => _context.Set<T>();
        public virtual IQueryable<T> Entities => EntitySet.AsQueryable();

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Get(params object[] keyValues)
        {
            var entity = EntitySet.Find(keyValues);
            return entity; //?? throw new EntityNotFoundException(keyValues);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return Entities.AsNoTracking();
        }

        public void Create(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }
        public void Delete(params object[] keyValues)
        {
            var entity = Get(keyValues);
            //entity.Deleted = true;
            Edit(entity);
        }

        //public int Delete(Expression<Func<T, bool>> predicate)
        //{
        //    var entities = Get(predicate);
        //    entities.ToList().ForEach(DeleteEntity);
        //    return entities.Count();
        //}

        public void Edit(T entity)
        {
            //entity.ModifiedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEntity(T entity)
        {
            //_context.Entry(entity).State = EntityState.Deleted;
            //entity.Deleted = true;
            Edit(entity);
        }
        public EntityEntry GetTracking(T entity)
        {
            return _context.Entry(entity);
        }
        public bool IsExist(Guid id)
        {
            return Entities.Contains(Get(id));
        }
    }
}

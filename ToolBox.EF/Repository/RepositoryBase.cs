using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ToolBox.EF.Repository
{
    public abstract class RepositoryBase 
    {
        protected readonly DbContext _context;
        protected RepositoryBase(DbContext context)
        {
            _context = context;
        }
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity> 
        where TEntity : class
    {
        protected DbSet<TEntity> _entities => _context.Set<TEntity>();

        public RepositoryBase(DbContext context): base(context) { }

        public virtual IEnumerable<TEntity> Find()
        {
            return _entities;
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual int Count()
        {
            return _entities.Count();
        }

        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return _entities.Where(predicate).Count();
        }

        public virtual TEntity? FindOne(params object[] ids)
        {
            return _entities.Find(ids);
        }

        public virtual TEntity? FindOne(Func<TEntity, bool> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public bool Any(Func<TEntity, bool> predicate)
        {
            return _entities.Any(predicate);
        }

        public virtual TEntity Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Add(entity);
            entry.State = EntityState.Added;
            _context.SaveChanges();
            return entry.Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Update(entity);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
            return entry.Entity;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Remove(entity);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
            return entry.Entity;
        }
    }
}

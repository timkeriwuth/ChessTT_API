
namespace ToolBox.EF.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        int Count();
        int Count(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Find();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
        TEntity? FindOne(Func<TEntity, bool> predicate);
        bool Any(Func<TEntity, bool> predicate);
        TEntity? FindOne(params object[] ids);
        TEntity Remove(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
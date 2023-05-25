namespace ChatApp.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public TEntity? Get(int id);
    public void Delete(int id);
    public int Add(TEntity entity);
    public void Update(int id, TEntity entity);
}

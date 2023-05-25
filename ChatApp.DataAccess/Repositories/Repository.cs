using ChatApp.Domain.Interfaces;
using System.Data.SqlClient;

namespace ChatApp.DataAccess.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private protected readonly SqlConnection _connection;

    public Repository (string connectionString)
    {
        _connection = new SqlConnection (connectionString);
    }

    public abstract TEntity? Get(int id);

    public abstract void Delete(int id);

    public abstract int Add(TEntity entity);

    public abstract void Update(int id,TEntity entity);
}

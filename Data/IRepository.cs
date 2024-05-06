namespace Garage.Data;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}
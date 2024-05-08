namespace Garage.Data;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task<T> SearchByString(string value);
    Task Add(T entity);
    Task Update(T entity);
}
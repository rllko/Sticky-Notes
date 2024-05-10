namespace DapperGenericRepository.Repository;

public interface IGenericRepository<T>
{
    public T GetById(long id);
    public IEnumerable<T> GetAll();
    public bool Add(T entity);
    public Task<bool> AddAsync(T entity);
    public bool Update(T entity);
    public Task<bool> UpdateAsync(T entity);
    public bool Delete(T entity);
}


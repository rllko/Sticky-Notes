using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperGenericRepository.Repository;

public interface IGenericRepository<T>
{
    public T GetById(long id);
    public IEnumerable<T> GetAll();
    public bool Add(T entity);
    public bool Update(T entity);
    public bool Delete(T entity);
}


using System.Linq.Expressions;

namespace ApiCatalogo.Interfaces.Repositories;

public interface IRepository<T>
{
    /*! cuidado para não violar o principio ISP do SOLID, aqui somente deve haver métodos que serão usados em todas as entidades.*/

    IEnumerable<T> GetAll();

    T? Get(Expression<Func<T, bool>> predicate);

    T Create(T entity);

    T Upadte(T entity);

    T Delete(T entity);
}
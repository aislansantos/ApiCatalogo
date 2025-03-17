using System.Linq.Expressions;
using ApiCatalogo.Interfaces.Repositories;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        /* AsNoTraking -> Metodo do EntityFramework que libera genreciamento dos objetos na memoria, ganha-se desempenho de memoria e de processamento.
         * Só pode ser usado se os objetos não forem alterados, em uma busca de todos os dados pode-ser ser feito o uso deste recurso.
         * */
        return _context.Set<T>().AsNoTracking().ToList();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        //_context.SaveChanges(); -> save chances vai ser feito pelo unitOfWork
        return entity;
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity); // -> retorna sql alterando todos os atreibutos da entidade. usado para atualizações completa
        // _context.Entry(entity).State = EntityState.Modified; // retorna um sql que faz a alteração somente dos atributos que tem alterações na entidade
        // _context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        //_context.SaveChanges();
        return entity;
    }
}
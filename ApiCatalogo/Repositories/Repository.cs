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
        return _context.Set<T>().ToList();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public T Upadte(T entity)
    {
        // _context.Set<T>().Update(entity); // -> retorna sql alterando todos os atreibutos da entidade. usado para atualizações completa
        _context.Entry(entity).State = EntityState.Modified; // retorna um sql que faz a alteração somente dos atributos que tem alterações na entidade
        _context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}
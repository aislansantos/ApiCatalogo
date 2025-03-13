using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public Categoria Create(Categoria categoria)
    {
        ArgumentNullException.ThrowIfNull(categoria); // Forma mais moderna de fazer a validação de categoria is null, se for retorna uma exception.

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return categoria;
    }

    public Categoria Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);

        ArgumentNullException.ThrowIfNull(nameof(categoria));

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();

        return categoria;
    }

    public Categoria GetCategoria(int id)
    {
        return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
    }

    public IEnumerable<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }

    public Categoria Update(Categoria categoria)
    {
        ArgumentNullException.ThrowIfNull(nameof(categoria));

        _context.Entry(categoria).State = EntityState.Modified;// o tratamento de update e o entry(reponsabilidade).state é identica no final das contas.
        _context.SaveChanges();

        return categoria;
    }
}
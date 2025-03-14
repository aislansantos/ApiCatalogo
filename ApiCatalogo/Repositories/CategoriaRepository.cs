using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using APICatalogo.Context;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }
}
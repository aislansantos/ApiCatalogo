using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using APICatalogo.Context;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosPorCategorias(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
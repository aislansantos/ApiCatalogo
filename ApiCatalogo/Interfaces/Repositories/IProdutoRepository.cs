using ApiCatalogo.Models;

namespace ApiCatalogo.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategorias(int id);
    }
}
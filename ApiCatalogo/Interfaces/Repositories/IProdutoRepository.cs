using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Interfaces.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams);
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParams);

        IEnumerable<Produto> GetProdutosPorCategorias(int id);
    }
}
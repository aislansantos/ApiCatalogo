using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Interfaces.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams);
}
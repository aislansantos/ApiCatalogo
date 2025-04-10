using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using APICatalogo.Context;

namespace ApiCatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams)
    {
        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();

        var categoriasOrdenadas = PagedList<Categoria>.ToPagedList(categorias,
                        categoriasParams.PageNumber, categoriasParams.PageSize);

        return categoriasOrdenadas;
    }
}
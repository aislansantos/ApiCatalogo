using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Produto Create(Produto produto)
    {
        ArgumentNullException.ThrowIfNull(produto);

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return produto;
    }

    public bool Delete(int id)
    {
        var produto = _context.Produtos.Find(id);

        ArgumentNullException.ThrowIfNull(nameof(produto));

        if (produto is not null)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public Produto GetProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        ArgumentNullException.ThrowIfNull(produto);

        return produto;
    }

    // Usando IQuerable somente para demonstrar, é um tipo de desenvolvimnento chamado lazy loading.
    public IQueryable<Produto> GetProdutos()
    {
        return _context.Produtos;
    }

    public bool Update(Produto produto)
    {
        ArgumentNullException.ThrowIfNull(nameof(produto));

        if (_context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId))
        {
            _context.Produtos.Update(produto); // o tratamento de update e o entry(reponsabilidade).state é identica no final das contas.
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
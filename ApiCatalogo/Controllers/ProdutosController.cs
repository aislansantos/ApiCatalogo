using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    private ILogger<ProdutosController> _logger;

    public ProdutosController(ILogger<ProdutosController> logger,
                              IUnitOfWork uof)
    {
        _logger = logger;
        _uof = uof;
    }

    // Método do repositório especifico
    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategorias(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategorias(id);
        if (produtos is null)
            return NotFound();

        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null)
            return NotFound("Produtos Não encontrados");

        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
        {
            _logger.LogWarning($"Produto com o id= {id} não encontrado ...");
            return NotFound($"Produto com o id= {id} não encontrado...");
        }

        return Ok(produto);
    }

    [HttpPost]
    public ActionResult Post(Produto? produto)
    {
        if (produto is null)
        {
            _logger.LogWarning("Daos inválidos...");
            return BadRequest("Dados inválidos...");
        }

        var produtoCriado = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();//400

        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound("Produtos não encontrado ..");

        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();

        return Ok(produtoDeletado);
    }
}
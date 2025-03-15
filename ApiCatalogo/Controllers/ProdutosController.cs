using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _produtoRepositorio;

    //private readonly IRepository<Produto> _repository;
    ILogger<ProdutosController> _logger;

    public ProdutosController(IProdutoRepository produtoRepositorio,
                               //IRepository<Produto> repository,
                               ILogger<ProdutosController> logger)
    {
        _produtoRepositorio = produtoRepositorio;
        //_repository = repository;
        _logger = logger;
    }

    // Método do repositório especifico
    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<Produto>> GetProdutosCategorias(int id)
    {
        var produtos = _produtoRepositorio.GetProdutosPorCategorias(id);
        if (produtos is null)
            return NotFound();

        return Ok(produtos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _produtoRepositorio.GetAll();
        if (produtos is null)
            return NotFound("Produtos Não encontrados");

        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _produtoRepositorio.Get(p => p.ProdutoId == id);
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

        var produtoCriado = _produtoRepositorio.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();//400

        var produtoAtualizado = _produtoRepositorio.Update(produto);

        return Ok(produtoAtualizado);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _produtoRepositorio.Get(p => p.ProdutoId == id);

        if (produto is null)
            return NotFound("Produtos não encontrado ..");

        var produtoDeletado = _produtoRepositorio.Delete(produto);

        return Ok(produtoDeletado);
    }
}
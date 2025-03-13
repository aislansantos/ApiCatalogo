using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public ProdutosController(IProdutoRepository repository,
                              IConfiguration configuration,
                              ILogger<ProdutosController> logger)
    {
        _repository = repository;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> GetProdutos()
    {
        var produtos = _repository.GetProdutos();
        if (produtos is null)
            return NotFound("Produtos Não encontrados");

        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _repository.GetProduto(id);
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

        var produtoCriado = _repository.Create(produto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
            return BadRequest();

        bool produtoAtualizado = _repository.Update(produto);

        if (produtoAtualizado)
        {
            return Ok(produto);
        }

        return StatusCode(500, $"Falaha ao atualizar o produto de id = {id}");
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        bool produtoDeletado = _repository.Delete(id);

        if (produtoDeletado)
        {
            return Ok($"Produdo de id={id} foi excluido");
        }

        return StatusCode(500, $"Falaha ao excluir o produto de id = {id}");
    }
}
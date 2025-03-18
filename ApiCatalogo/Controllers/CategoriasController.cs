using ApiCatalogo.DTOs;
using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ILogger<CategoriasController> logger,
                                IUnitOfWork uof)
    {
        _logger = logger;
        _uof = uof;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uof.CategoriaRepository.GetAll();

        if (categorias is null)
            return NotFound("Não existem categorias ...");

        var categoriasDTO = new List<CategoriaDTO>();
        foreach (var categoria in categorias)
        {
            var categoriaDTO = new CategoriaDTO()
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl,
            };
            categoriasDTO.Add(categoriaDTO);
        }

        return Ok(categoriasDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id= {id} não encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        //Antes de retornar deve-se fazer o mapeamento entre o DTO e o objeto
        var categoriaDTO = new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl,
        };

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl,
        };

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();

        var novaCategoriaDTO = new CategoriaDTO()
        {
            CategoriaId = categoriaCriada.CategoriaId,
            Nome = categoriaCriada.Nome,
            ImagemUrl = categoriaCriada.ImagemUrl,
        };

        return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDTO.CategoriaId }, novaCategoriaDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.CategoriaId)
        {
            _logger.LogWarning($"Dados inválidos...");
            return BadRequest("Dados inválidos");
        }

        var categoria = new Categoria()
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImagemUrl = categoriaDTO.ImagemUrl,
        };

        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.Commit();

        var categoriaAtualizadaDTO = new CategoriaDTO()
        {
            CategoriaId = categoriaAtualizada.CategoriaId,
            Nome = categoriaAtualizada.Nome,
            ImagemUrl = categoriaAtualizada.ImagemUrl,
        };

        return Ok(categoriaAtualizadaDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria == null)
        {
            _logger.LogWarning($"Categoria com id={id} não encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.Commit();

        var categoriaExcluidaDTO = new CategoriaDTO()
        {
            CategoriaId = categoriaExcluida.CategoriaId,
            Nome = categoriaExcluida.Nome,
            ImagemUrl = categoriaExcluida.ImagemUrl,
        };

        return Ok(categoriaExcluidaDTO);
    }
}
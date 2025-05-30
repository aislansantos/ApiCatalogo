﻿using ApiCatalogo.Interfaces.Repositories;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using APICatalogo.Context;

namespace ApiCatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    //public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams)
    //{
    //    return GetAll()
    //        .OrderBy(p => p.Nome)
    //        .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
    //        .Take(produtosParams.PageSize)
    //        .ToList();
    //}

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParams)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos,
                produtosParams.PageNumber, produtosParams.PageSize);

        return produtosOrdenados;
    }

    public IEnumerable<Produto> GetProdutosPorCategorias(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
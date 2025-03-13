﻿using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ApiCatalogo.Interfaces.Repositories;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> GetCategorias();

    Categoria GetCategoria(int id);

    Categoria Create(Categoria categoria);

    Categoria Update(Categoria categoria);

    Categoria Delete(int id);
}
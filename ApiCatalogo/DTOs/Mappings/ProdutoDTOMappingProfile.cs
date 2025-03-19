using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings
{
    public class ProdutoDTOMappingProfile : Profile
    {
        public ProdutoDTOMappingProfile()
        {
            // Dessa forma usando o ReverseMap ele vai estar fazendo o mapeamento de produto -> produtoDTo e de ProdutoDTO para produto;
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
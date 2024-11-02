using AutoMapper;
using LicoreriaBackend.Dto;
using LicoreriaBackend.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        //aca es donde mapeamos todas las entidades con sus dtos y entidades completas
        public MappingProfiles()
        {
            //aca le decimos qeu cree el mapeo de categoria a dto y viceversa con el ReverseMap sino solo se puede de 
            //categoria a dto eso es importante
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();
            CreateMap<Proveedor, ProveedorDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Bodega, BodegaDto>().ReverseMap();
        }
    }
}

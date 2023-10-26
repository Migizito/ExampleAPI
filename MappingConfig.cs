using AutoMapper;
using ExampleAGAPI.Models;
using ExampleAGAPI.Models.DTO;

namespace ExampleAGAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AGranel, AGranelDTO>();
            CreateMap<AGranelDTO, AGranel>();
            CreateMap<AGranel, AGranelCreateDTO>().ReverseMap();
            CreateMap<AGranel, AGranelUpdateDTO>().ReverseMap();

            CreateMap<NumeroProducto, NumeroProductoDTO>().ReverseMap();
            CreateMap<NumeroProducto, NumeroProductoCreateDTO>().ReverseMap();
            CreateMap<NumeroProducto, NumeroProductoUpdateDTO>().ReverseMap();
        }
    }
}

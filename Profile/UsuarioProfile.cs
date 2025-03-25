using AutoMapper;
using PlataformaAPI.Data.Dtos;
using PlataformaAPI.Models;

namespace PlataformaAPI.Profiles;



public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto, Usuario>();
    }
}


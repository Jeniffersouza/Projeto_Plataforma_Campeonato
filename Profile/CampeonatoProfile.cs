using AutoMapper;
using PlataformaAPI.Data.Dtos;
using PlataformaAPI.Models;

namespace PlataformaAPI.Profiles  
{
    public class CampeonatoProfile : Profile
    {
        public CampeonatoProfile()
        {
            CreateMap<CreateCampeonatoDto, Campeonato>();
            CreateMap<UpdateCampeonatoDto, Campeonato>();
            CreateMap<Campeonato, UpdateCampeonatoDto>();
            CreateMap<Campeonato, ReadCampeonatoDto>();
        }
    }
}

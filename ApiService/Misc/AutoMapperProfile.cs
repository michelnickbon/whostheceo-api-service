using ApiService.Dtos;
using ApiService.Models;
using AutoMapper;

namespace ApiService.Misc
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CompanyPostDto, Company>();
            CreateMap<CompanyPutDto, Company>();
            CreateMap<HistoryPostDto, History>();
            CreateMap<CeoPostDto, Ceo>();
        }
    }
}

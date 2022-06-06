using AutoMapper;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInHouse.EFCoreApi.Application.AutoMapper
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Livro, LivroViewModel>()
                .ForMember(dest => dest.Publicacao, opt => opt.MapFrom(src => src.DataPublicacao));
        }
    }
}

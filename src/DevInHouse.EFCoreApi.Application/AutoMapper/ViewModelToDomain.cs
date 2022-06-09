using AutoMapper;
using DevInHouse.EFCoreApi.Application.ViewModels;
using DevInHouse.EFCoreApi.Core.Entities;

namespace DevInHouse.EFCoreApi.Application.AutoMapper
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            CreateMap<LivroCreateViewModel, Livro>()
                .ConstructUsing((src, res) =>
                {
                    return new Livro(src.Titulo, src.CategoriaId, src.AutorId, src.Publicacao, src.Preco);
                })
                .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.Publicacao));


            CreateMap<LivroEditViewModel, Livro>()
                .ConstructUsing((src, res) =>
                {
                    return new Livro(src.Id, src.Titulo, src.CategoriaId, src.AutorId, src.Publicacao, src.Preco);
                })
                .ForMember(dest => dest.DataPublicacao, opt => opt.MapFrom(src => src.Publicacao));
        }
    }
}

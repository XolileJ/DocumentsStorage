using AutoMapper;
using DocumentsStorage.Infra.Data.Models;
using DocumentsStorage.Service.ViewModels;

namespace DocumentsStorage.Service.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {            
            CreateMap<Document, DocumentViewModel>();
        }
    }
}

using AutoMapper;
using DocumentsStorage.Infra.Data.Models;
using DocumentsStorage.Service.ViewModels;

namespace DocumentsStorage.Service.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<DocumentViewModel, Document>();
        }
    }
}

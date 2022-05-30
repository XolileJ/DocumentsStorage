using AutoMapper;
using DocumentsStorage.Infra.Data.Interfaces;
using DocumentsStorage.Infra.Data.Models;
using DocumentsStorage.Service.Interfaces;
using DocumentsStorage.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DocumentsStorage.Service.Services
{
    public class DocumentService : IDocumentService
    {
        public readonly IDocumentRepository documentRepository;
        public readonly IMapper mapper;

        public DocumentService(IDocumentRepository documentRepository, IMapper mapper)
        {
            this.documentRepository = documentRepository;
            this.mapper = mapper;
        }

        public void Create(FileUploadViewModel file)
        {
            var uploadFile = new Document();

            byte[] bytes;

            using (BinaryReader br = new BinaryReader(file.File.OpenReadStream()))
            {
                bytes = br.ReadBytes((int)file.File.OpenReadStream().Length);

                uploadFile = new Document()
                {
                    Description = file.Description,
                    Name = file.Name,
                    FileContent = bytes,
                    FileType = file.File.ContentType,
                    Size = file.File.Length
                };
            }

            documentRepository.Add(uploadFile);
            documentRepository.SaveChanges();
        }             

        public IEnumerable<DocumentViewModel> GetDocuments()
        {
            var documents = documentRepository.GetAll().Select(x => new Document()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Size = x.Size
            });

            return mapper.Map<IEnumerable<DocumentViewModel>>(documents);
        }

        public void Delete(long id)
        {
            if (documentRepository.GetById(id) != null)
            {
                documentRepository.Remove(id);
                documentRepository.SaveChanges();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

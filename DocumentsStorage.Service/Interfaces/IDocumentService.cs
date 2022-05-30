using DocumentsStorage.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DocumentsStorage.Service.Interfaces
{
    public interface IDocumentService : IDisposable
    {
        void Create(FileUploadViewModel file);
        void Delete(long id);
        IEnumerable<DocumentViewModel> GetDocuments();
        DocumentViewModel Download(long id);
    }
}

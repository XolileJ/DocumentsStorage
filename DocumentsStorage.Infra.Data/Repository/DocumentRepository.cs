using DocumentsStorage.Infra.Data.Context;
using DocumentsStorage.Infra.Data.Interfaces;
using DocumentsStorage.Infra.Data.Models;

namespace DocumentsStorage.Infra.Data.Repository
{
    public class DocumentRepository : DocumentStorageRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DocumentContext context)
            : base(context)
        {

        }
    }
}

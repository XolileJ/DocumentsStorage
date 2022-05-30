using DinkToPdf.Contracts;
using DocumentsStorage.Service.Interfaces;
using DocumentsStorage.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace DocumentsStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        public readonly IDocumentService documentService;
        public DocumentController(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        [HttpPost]
        public IActionResult Post([FromForm] FileUploadViewModel file)
        {
            try
            {
                documentService.Create(file);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var documents = documentService.GetDocuments();

                return Ok(documents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
        
        [HttpGet("Download")]
        public IActionResult Download(long id)
        {
            try
            {
                var document = documentService.Download(id);

                using (var stream = new MemoryStream(document.FileContent))
                {
                    var file = new FormFile(stream, 0, document.FileContent.Length,document.Name, document.Name)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = document.FileType,
                    };

                    System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = file.FileName
                    };
                    file.ContentDisposition = cd.ToString();

                    return Ok(file);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromForm] long id)
        {
            try
            {
                documentService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
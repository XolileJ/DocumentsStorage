using DinkToPdf.Contracts;
using DocumentsStorage.Service.Interfaces;
using DocumentsStorage.Service.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace DocumentsStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                if (file == null)
                {
                    return BadRequest("Please submit a document");
                }

                if (string.IsNullOrEmpty(file.Name) || 
                    string.IsNullOrEmpty(file.Description))
                {
                    return BadRequest("Please provide values for both Name and Description");
                }

                documentService.Create(file);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize(Policy = "AdministratorOnly")]
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

        [Authorize(Policy = "AdministratorOnly")]
        [HttpGet("Download")]
        public IActionResult Download(long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid id value");
                }

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

        [Authorize(Policy = "AdministratorOnly")]
        [HttpDelete]
        public IActionResult Delete([FromForm] long id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please provide a valid id value");
                }

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
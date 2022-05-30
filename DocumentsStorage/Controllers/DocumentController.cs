using DocumentsStorage.Service.Interfaces;
using DocumentsStorage.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DocumentsStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        public readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        public IActionResult Post([FromForm] FileUploadViewModel file)
        {
            try
            {
                _documentService.Create(file);

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
                var documents = _documentService.GetDocuments();

                return Ok(documents);
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
                _documentService.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
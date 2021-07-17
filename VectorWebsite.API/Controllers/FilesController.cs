using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using VectorWebsite.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VectorWebsite.API.Controllers
{
    public class FilesController : ControllerBase
    {
        public async Task<IActionResult> UploadFile(string category, IFormFile file)
        {
            try
            {
                // if (file == null || file.Length == 0)
                // {
                //     return BadRequest("No file selected");
                // }

                var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", category);
                Directory.CreateDirectory(dir);
                var path = Path.Combine(dir, file.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return StatusCode(StatusCodes.Status201Created, path);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [AllowAnonymous]
        [Route("api/files/download")]
        [HttpGet]
        public async Task<ActionResult> DownloadFile(string category, string filename)
        {
            if (filename == null)
                return BadRequest();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", category, filename);
            var memory = new MemoryStream();
            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();

            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace VectorWebsite.Domain
{
    public class UploadedFile
    {
        public string FileName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}

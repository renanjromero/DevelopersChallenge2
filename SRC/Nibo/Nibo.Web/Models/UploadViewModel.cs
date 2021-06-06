using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Nibo.Web.Models
{
    public class UploadViewModel
    {
        public IFormFile[] Files { get; set; }
    }
}

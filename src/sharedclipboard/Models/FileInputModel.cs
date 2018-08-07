using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace sharedclipboard.Models
{
    public class FileInputModel
    {
        public string id {get;set;}
        public IList<IFormFile> files {get;set;}
    }
}
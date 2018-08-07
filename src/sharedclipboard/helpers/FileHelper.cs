using System.IO;
using Microsoft.AspNetCore.Http;

namespace sharedclipboard.helpers
{
    public static class FileHelper
    {
        public static string UploadFile(string pathToCopy, IFormFile file, string newFileName = null )
        {
            string fullpath  = null;
            try
            {
                if(file != null)
                {
                    string extension = Path.GetExtension(file.FileName);

                    newFileName = newFileName ??  file.FileName;
                    fullpath = Path.Combine(pathToCopy, file.FileName);
                    var fileStream = new FileStream(fullpath, FileMode.Create);
                    file.CopyTo(fileStream);
                }
            }
            catch (System.Exception)
            {
                //TODO: log
                fullpath = null;
            }
            
            return fullpath;
        }
    }
}
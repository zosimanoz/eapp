using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [Route("api/v1/file")]
    [AllowAnonymous]
    public class FileHandlerApiController : BaseApiController
    {
        private IHostingEnvironment _env;
        public FileHandlerApiController(IHostingEnvironment env)
        {
            _env = env;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            Console.WriteLine("file upload called");
            var path = Path.Combine(_env.WebRootPath, "attachments");
            string filePath ="";
            string relativeFilePath="";
            Console.WriteLine(path);
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                foreach (var formFile in files)
                {
                    string savedFileName = Guid.NewGuid().ToString();
                    string fileExtension = System.IO.Path.GetExtension(formFile.FileName);
                    filePath = path + "/" + savedFileName + fileExtension;
                    relativeFilePath ="/attachments" + "/" + savedFileName + fileExtension;
                    if (formFile.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
                return Ok(new {relativeFilePath });
            }
            catch (IOException)
            {
                throw;
            }

        }
    }
}
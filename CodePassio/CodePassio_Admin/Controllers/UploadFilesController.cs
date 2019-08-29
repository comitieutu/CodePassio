using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePassio_Admin.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public UploadFilesController(IHostingEnvironment IHostingEnvironment)
        {
            _environment = IHostingEnvironment;
        }
        #region snippet1
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> upload)
        {
            long size = upload.Sum(f => f.Length);

            // full path to file in temp location
            var newFileName = string.Empty;
            var fileName = string.Empty;
            var returnUrl = string.Empty;

            foreach (var formFile in upload)
            {
                if (formFile.Length > 0)
                {
                    fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var FileExtension = Path.GetExtension(fileName);

                    // concating  FileName + FileExtension
                    newFileName = myUniqueFileName + FileExtension;

                    // Combines two strings into a path.
                    fileName = Path.Combine(_environment.WebRootPath, "demoImages") + $@"\{newFileName}";

                    // if you want to store path of folder in database
                    //PathDB = "demoImages/" + newFileName;
                    returnUrl = $"{this.Request.Scheme}://{this.Request.Host.Value.ToString()}{this.Request.PathBase.Value.ToString()}/demoImages/{newFileName}";
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { uploaded = 1, url= returnUrl, fileName });
        }
        #endregion
    }
}
using ERPSYS.Models;
using ERPSYS.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ERPSYS.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly DropboxService _dropboxService;

        public ImageController(DropboxService dropboxService)
        {
            _dropboxService = dropboxService;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest(new { success = false, message = "Invalid file." });
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            using (var stream = imageFile.OpenReadStream())
            {
                string dropboxUrl = await _dropboxService.UploadFileAsync(stream, fileName);
                return Ok(new { success = true, url = dropboxUrl });
            }
        }

    }
}
        //[HttpPost]
        //public async Task<IActionResult> UploadImage(IFormFile imageFile)
        //{
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        return Json(new { success = false, message = "Invalid file." });
        //    }

        //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

        //    using (var stream = imageFile.OpenReadStream())
        //    {
        //        string dropboxPath = await _dropboxService.UploadFileAsync(stream, fileName);
        //        return Json(new { success = true, path = dropboxPath });
        //    }

        //}
        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
        //{
        //    if (imageFile == null || imageFile.Length == 0)
        //    {
        //        return BadRequest(new { success = false, message = "Invalid file." });
        //    }

        //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

        //    using (var stream = imageFile.OpenReadStream())
        //    {
        //        try
        //        {
        //            string dropboxUrl = await _dropboxService.UploadFileAsync(stream, fileName);
        //            return Ok(new { success = true, imageUrl = dropboxUrl });
        //        }
        //        catch (Exception ex)
        //        {
        //            return StatusCode(500, new { success = false, message = "Error uploading file.", error = ex.Message });
        //        }
        //    }
        //}
            //[HttpPost]
            //public async Task<IActionResult> UploadImage(IFormFile imageFile, [FromServices] AppDbContext _db)
            //{
            //    if (imageFile == null || imageFile.Length == 0)
            //    {
            //        return Json(new { success = false, message = "Invalid file." });
            //    }

            //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            //    using (var stream = imageFile.OpenReadStream())
            //    {
            //        string dropboxPath = await _dropboxService.UploadFileAsync(stream, fileName);

            //        // Save to Database
            //        var fileRecord = new FileRecord
            //        {
            //            FileName = fileName,
            //            DropboxPath = dropboxPath,
            //            UploadedAt = DateTime.UtcNow
            //        };
            //        _db.FileRecords.Add(fileRecord);
            //        await _db.SaveChangesAsync();

            //        return Json(new { success = true, path = dropboxPath });
            //    }
            //}



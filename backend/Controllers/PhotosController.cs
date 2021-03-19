using System;
using System.IO;
using System.Threading.Tasks;
using AeDirectory.Models;
using AeDirectory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeDirectory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IStorageService _S3StorageService;

        public PhotosController(IStorageService S3StorageService)
        {
            _S3StorageService = S3StorageService;
        }
        

        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        // todo
        // [Authorize]
        public async Task<bool> AddPhoto([FromForm] PhotoModel photoFile)
        {
            return await _S3StorageService.AddItem(photoFile.FormFile, photoFile.FileName, photoFile.Id);
        }
        
        [EnableCors("AllowAnyOrigin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            try
            {
                var result = await _S3StorageService.GetItem(id);
                return File(result, "image/png");
            }
            catch
            {
                return Ok(id);
            }
        }


    }
}
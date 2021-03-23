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
        [HttpPut]
        // todo
        // [Authorize]
        public async Task<bool> AddPhoto([FromForm] PhotoModel photoFile)
        {
            // id is mandatory
            if (photoFile.Id != null)
            {
                return await _S3StorageService.AddItemWithID(photoFile.FormFile, photoFile.FileName, (int)photoFile.Id);
            }
            else
            {
                // id is not corrected passed in
                return false;
            }

            
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
        
        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        // todo
        // [Authorize]
        public async Task<string> AddPhotoWithoutID([FromForm] PhotoModelWithoutId photoFile)
        {
            return await _S3StorageService.AddItemWithoutID(photoFile.FormFile, photoFile.FileName);
        }
        
        


    }
}
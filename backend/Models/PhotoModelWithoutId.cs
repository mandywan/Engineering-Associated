using Microsoft.AspNetCore.Http;

namespace AeDirectory.Models
{
    public class PhotoModelWithoutId
    {
        public string FileName { get; set; } // photo name
        public IFormFile FormFile { get; set; } // photo file
    }
}
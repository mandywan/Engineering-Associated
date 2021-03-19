using Microsoft.AspNetCore.Http;

namespace AeDirectory.Models
{
    public class PhotoModel
    {
        public string FileName { get; set; } // photo name
        public IFormFile FormFile { get; set; } // photo file
        public int Id { get; set; }  // employee id
    }
}
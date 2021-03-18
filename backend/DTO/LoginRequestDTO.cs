using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AeDirectory.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
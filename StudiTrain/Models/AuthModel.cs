using System.ComponentModel.DataAnnotations;

namespace StudiTrain.Models
{
    public class AuthModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Passhash { get; set; }
    }
}

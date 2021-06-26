using System.ComponentModel.DataAnnotations;

namespace UkrainiansId.Web.Models
{
    public class LoginViewModel
    {
        [Required, MinLength(7), MaxLength(100)]
        public string Login { get; set; }
        [Required, MinLength(8), MaxLength(35)]
        public string Password { get; set; }
    }
}

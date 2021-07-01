using System.ComponentModel.DataAnnotations;
namespace UkrainiansId.Domain.Models
{
    public class AuthenticationData : BaseModel<int>
    {
        [Required, MinLength(2), MaxLength(50)]
        public string AuthType { get; set; }
        [Required]
        public string Data { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
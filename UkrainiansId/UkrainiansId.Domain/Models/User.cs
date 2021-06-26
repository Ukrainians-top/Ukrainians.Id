using System.ComponentModel.DataAnnotations;
namespace UkrainiansId.Domain.Models
{
    public class User : BaseModel<int>
    {
        [Required]
        public string Firstname { get; set; }
        [MinLength(2), MaxLength(70)]
        public string Lastname { get; set; }
        [MinLength(3), MaxLength(25)]
        public string Username { get; set; }
        [MinLength(5), MaxLength(1000)]
        public string PathToPicture { get; set; }
    }
}
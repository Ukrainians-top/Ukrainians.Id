using System.ComponentModel.DataAnnotations;
namespace UkrainiansId.Domain.Models
{
    public class User : BaseModel<int>
    {
        [Required]
        public string FirstName { get; set; }
        [MinLength(2), MaxLength(70)]
        public string LastName { get; set; }
        [MinLength(3), MaxLength(25)]
        public string UserName { get; set; }
        [MinLength(5), MaxLength(1000)]
        public string PathToPicture { get; set; }
    }
}
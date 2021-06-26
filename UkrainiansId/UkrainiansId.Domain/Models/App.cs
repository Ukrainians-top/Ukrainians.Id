using System.ComponentModel.DataAnnotations;

namespace UkrainiansId.Domain.Models
{
    public class App : BaseModel<int>
    {
        [Required, MinLength(2), MaxLength(75)]
        public string Name { get; set; }
        [MinLength(2), MaxLength(10)]
        public string ShortName { get; set; }
        [MinLength(2), MaxLength(10)]
        public string Description { get; set; }
        [Required, MinLength(30), MaxLength(35)]
        public string ClientId { get; set; }
        [Required, MinLength(60), MaxLength(80)]
        public string ClientSecret { get; set; }
        [Required]
        public bool ClientSecretIsRequired { get; set; }
    }
}
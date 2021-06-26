using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UkrainiansId.Domain.Models
{
    public class BaseCreateModel
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFromIP { get; set; }
    }

    public class BaseUpdateModel : BaseCreateModel
    {
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public string LastUpdatedFromIP { get; set; }
    }

    public class BaseModel : BaseUpdateModel
    {

    }

    public class BaseModel<Tid> : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Tid Id { get; set; }
    }
    public class BaseModelWithoutGen<Tid> : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Tid Id { get; set; }
    }
}
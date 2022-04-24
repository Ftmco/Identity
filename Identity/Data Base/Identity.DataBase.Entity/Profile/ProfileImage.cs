using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record ProfileImage
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ProfileId { get; set; }

    [Required]
    public Guid FileId { get; set; }

    [Required]
    public string FileToken { get; set; }

    //Navigation Property
    //Relationships

    public virtual Profile Profile { get; set; }
}

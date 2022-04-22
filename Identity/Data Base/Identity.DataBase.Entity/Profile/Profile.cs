using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record Profile
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string FistName { get; set; }

    [Required]
    public string LastName { get; set; }

    //Navigation Property
    //Relationships

    public virtual ICollection<ProfileImage> ProfileImages { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record PagesRoles
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    [Required]
    public Guid PageId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Page Page { get; set; }

    public virtual Role Role { get; set; }
}

using Identity.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entity.Page;

public record RolePages
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PageId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Page Page { get; set; }

    public virtual Role Role { get; set; }
}
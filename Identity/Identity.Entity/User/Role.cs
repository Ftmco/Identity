using Identity.Entity.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Entity.User;

public record Role
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Name { get; set; }

    //Navigation Property
    //Relationships

    public virtual ICollection<UserRoles> UserRoles { get; set; }

    public virtual ICollection<RolePages> RolePages { get; set; }
}

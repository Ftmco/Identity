using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record PageAction
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public Guid PageId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Page Page { get; set; }

    public virtual ICollection<ActionsRoles> ActionsRoles { get; set; }
}

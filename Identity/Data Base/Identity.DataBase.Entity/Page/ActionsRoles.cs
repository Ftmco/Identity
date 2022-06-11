using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record ActionsRoles
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ActionId { get; set; }

    [Required]
    public Guid RoleId { get; set; }

    //Navigation Property
    //Relationships

    [ForeignKey("ActionId")]
    public virtual PageAction PageAction { get; set; }

    public virtual Role Role { get; set; }
}

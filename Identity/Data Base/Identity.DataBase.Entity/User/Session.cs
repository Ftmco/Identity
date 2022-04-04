using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record Session
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Key { get; set; }

    [Required]
    public string Value { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public DateTime ExpireDate { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public string Os { get; set; }

    //Navigation Proerty
    //Relationships

    public virtual User User { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record Application
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public string Key { get; set; }

    [Required]
    public string ApiKey { get; set; }

    //Navigation Proeprty
    //Relationships

    public virtual ICollection<ApplicationsUsers> ApplicationsUsers { get; set; }
}

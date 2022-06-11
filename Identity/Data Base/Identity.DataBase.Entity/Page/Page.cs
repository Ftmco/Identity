using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DataBase.Entity;

public record Page
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    //Relationships
    //Navigation Property

    public virtual Application Application { get; set; }

    public virtual ICollection<PagesRoles> PagesRoles { get; set; }

    public virtual ICollection<PageAction> PageActions { get; set; }
}

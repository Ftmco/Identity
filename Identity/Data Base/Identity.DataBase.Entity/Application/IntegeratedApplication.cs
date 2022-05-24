using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.DataBase.Entity;

public record IntegeratedApplication
{

    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid BaseApplicationId { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }   
}

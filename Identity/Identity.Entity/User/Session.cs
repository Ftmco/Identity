﻿namespace Identity.Entity.User;

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

    [Required]
    public Guid ApplicationId { get; set; }

    //Navigation Property
    //Relationships

    public virtual User User { get; set; }

}
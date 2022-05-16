namespace Identity.Client.Models;

public record Avatar(Guid FileId, string FileToken, string? Base64);
namespace Identity.Client.Models;

public record LoginResponse(LoginStatus Status, Session? Session);

public record Session(string Key, string Value);
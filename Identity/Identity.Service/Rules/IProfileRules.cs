namespace Identity.Service.Rules;

public interface IProfileRules
{
    Task GetProfileAsync();

    Task UpdateProfileAsync();
}


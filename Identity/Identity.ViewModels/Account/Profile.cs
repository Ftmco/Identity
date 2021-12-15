using Identity.ViewModels.Application;
using Identity.ViewModels.File;

namespace Identity.ViewModels.Account;

public record ProfileViewModel(string Image, string Json, UserViewModel User);

public record UpdateProfileViewModel(ApplicationRequest Application, FileBase64ViewModel Image, string Json, UserViewModel User);
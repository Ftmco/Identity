using Identity.DataBase.Entity;

namespace Identity.Service.Implemention;

public class UserViewModel : IUserViewModel
{
    public Task<DataBase.ViewModel.UserViewModel> CreateUserViewModelAsync(User user)
    {
        DataBase.ViewModel.UserViewModel viewModel = new(
            Id: user.Id,
            FullName: "",
            Email: user.Email,
            MobileNo: user.MobileNo,
            RegisterDate: user.RegisterDate.ToString(),
            IsActive: user.IsActvie);
        return Task.FromResult(viewModel);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}

using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.Entities.User;
using HardwareStore.MVP.Presenters.Base;
using HardwareStore.MVP.Presenters.MainWindow;
using HardwareStore.MVP.ViewModels.UserIdentification;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.UserIdentification;
using HardwareStore.Services.SqlServerService;
using HardwareStore.Services.SqlServerService.DataProcessing.User;
using HardwareStore.Services.Utils.Monitoring.Caching.InMemory;

namespace HardwareStore.MVP.Presenters.UserIdentification;

public sealed class AuthorizationPresenter : Presenter<IAuthorizationView>
{
    private readonly UserService _userService;
    private readonly MemoryCache<string, UserEntity> _memoryCache;

    public AuthorizationPresenter(IApplicationController controller, IAuthorizationView view, SqlServerService service)
        : base(controller, view)
    {
        _userService = service.User;
        _memoryCache = new MemoryCache<string, UserEntity>();

        View.Authorization += LoginAsync;
        View.Registration += RegistrationAsync;
    }

    private async void LoginAsync(AuthorizationViewModel model)
    {
        UserEntity? user = await _userService.GetByAsync(new UserEntity
        {
            Login = model.Login,
            Password = model.Password
        });

        if (user == null)
        {
            View.ShowMessage("Неправильный логин или пароль", "Ошибка", MessageLevel.Error);
            return;
        }

        View.ShowMessage($"Добро Пожаловать, {user}!", "Добро пожаловать", MessageLevel.Info);

        switch (user.Role)
        {
            case Role.User:
                Controller.Run<MainWindowPresenter>();
                await _memoryCache.WriteAsync("User", user);
                View.Close();
                break;
            case Role.Admin:
                await _memoryCache.WriteAsync("Admin", user);
                View.Close();
                break;
        }
    }

    private void RegistrationAsync()
        => Controller.Run<RegistrationPresenter>();
}
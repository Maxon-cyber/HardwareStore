using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.Entities.User;
using HardwareStore.MVP.Presenters.Base;
using HardwareStore.MVP.ViewModels.MainWindow.Sections.UserAccount;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;
using HardwareStore.Services.Abstractions;
using HardwareStore.Services.SqlServerService;
using HardwareStore.Services.SqlServerService.DataProcessing.User;
using HardwareStore.Services.Utils.Monitoring.Caching.InMemory;

namespace HardwareStore.MVP.Presenters.MainWindow.UserAccount;

public sealed class UserAccountPresenter : Presenter<IUserAccountView>
{
    private readonly MemoryCache<string, UserEntity> _cache;
    private readonly UserService _service;

    public UserAccountPresenter(IApplicationController controller, IUserAccountView view, SqlServerService service)
        : base(controller, view)
    {
        _cache = new MemoryCache<string, UserEntity>();
        _service = service.User;

        View.UpdateData += UpdateDataAsync;
        View.LoadUserData += LoadUserDataAsync;
    }

    private async void LoadUserDataAsync()
    {
        UserEntity? user = await _cache.ReadByKeyAsync("User");

        await View.SetUserData(new UserAccountModel()
        {
            Name = user.Name,
            SecondName = user.SecondName,
            Patronymic = user.Patronymic,
            Gender = user.Gender.ToString(),
            Age = user.Age,
            Login = user.Login,
            Password = user.Password,
            HouseNumber = user.Location.HouseNumber,
            Street = user.Location.Street,
            City = user.Location.City,
            Region = user.Location.Region,
            Country = user.Location.Country
        });
    }

    private async void UpdateDataAsync(UserAccountModel model)
    {
        object? result = await _service.ChangeAsync(TypeOfUpdateCommand.Insert, new UserEntity()
        {
            Name = model.Name,
            SecondName = model.SecondName,
            Patronymic = model.Patronymic,
            Gender = Enum.Parse<Gender>(model.Gender),
            Age = model.Age,
            Login = model.Login,
            Password = model.Password,
            Location = new Location()
            {
                HouseNumber = model.HouseNumber,
                Street = model.Street,
                City = model.City,
                Region = model.Region,
                Country = model.Country
            },
        });

        if (!Convert.ToBoolean(result))
            View.ShowMessage("Не удалось обновить данные", "Ошибка", MessageLevel.Error);
        else
            View.ShowMessage("Данные успешно обновлены", "Успех", MessageLevel.Info);
    }
}
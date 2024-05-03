using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.Entities.User;
using HardwareStore.MVP.Presenters.Base;
using HardwareStore.MVP.ViewModels.UserIdentification;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.UserIdentification;
using HardwareStore.Services.Abstractions;
using HardwareStore.Services.SqlServerService;
using HardwareStore.Services.SqlServerService.DataProcessing.User;

namespace HardwareStore.MVP.Presenters.UserIdentification;

public sealed class RegistrationPresenter : Presenter<IRegistrationView>
{
    private readonly UserService _userService;

    public RegistrationPresenter(IApplicationController controller, IRegistrationView view, SqlServerService service)
        : base(controller, view)
    {
        _userService = service.User;

        View.Registration += Registration;
        View.ReturnToAuthorization += ReturnToAuthorization;
    }

    private async void Registration(RegistrationViewModel model)
    {
        object? isAdded = await _userService.ChangeAsync(TypeOfUpdateCommand.Insert, new UserEntity()
        {
            Name = model.Name,
            SecondName = model.SecondName,
            Patronymic = model.Patronymic,
            Gender = Enum.Parse<Gender>(model.Gender),
            Age = model.Age,
            Location = new Location()
            {
                HouseNumber = model.HouseNumber,
                Street = model.Street,
                City = model.City,
                Region = model.Region,
                Country = model.Country
            },
            Login = model.Login,
            Password = model.Password,
            Role = UserParameters.DEFAULT_ROLE
        });

        if (!Convert.ToBoolean(isAdded))
            View.ShowMessage("Пользователь с таким логином уже зарегистрирован", "Предупреждение", MessageLevel.Warning);
        else
            View.ShowMessage("Вы успешно зарегистрированы", "Успех", MessageLevel.Info);
    }

    private void ReturnToAuthorization()
        => View.Close();
}
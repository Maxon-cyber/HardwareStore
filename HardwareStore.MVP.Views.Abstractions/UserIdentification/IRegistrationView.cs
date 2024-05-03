using HardwareStore.MVP.ViewModels.UserIdentification;

namespace HardwareStore.MVP.Views.Abstractions.UserIdentification;

public interface IRegistrationView : IView
{
    event Action<RegistrationViewModel>? Registration;
    event Action? ReturnToAuthorization;
}
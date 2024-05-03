using HardwareStore.MVP.ViewModels.UserIdentification;

namespace HardwareStore.MVP.Views.Abstractions.UserIdentification;

public interface IAuthorizationView : IView
{
    event Action<AuthorizationViewModel> Authorization;
    event Action Registration;
}
using HardwareStore.MVP.ViewModels.MainWindow.Sections.UserAccount;

namespace HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;

public interface IUserAccountView : IView
{
    event Action LoadUserData;
    event Action<UserAccountModel> UpdateData;

    Task SetUserData(UserAccountModel user);
}
using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.MVP.Presenter.MainWindow.Sections.ShoppingCart;
using HardwareStore.MVP.Presenters.Base;
using HardwareStore.MVP.Presenters.MainWindow.ProductShowcase;
using HardwareStore.MVP.Presenters.MainWindow.UserAccount;
using HardwareStore.MVP.Views.Abstractions.MainWindow;

namespace HardwareStore.MVP.Presenters.MainWindow;

public sealed class MainWindowPresenter : Presenter<IMainWindowView>
{
    public MainWindowPresenter(IApplicationController controller, IMainWindowView view)
        : base(controller, view)
    {
        View.UserAccount += OpenUserAccount;
        View.ProductShowcase += OpenProductShowcase;
        View.ShoppingCart += OpenShoppingCart;
    }

    private void OpenUserAccount()
       => Controller.Run<UserAccountPresenter>();

    private void OpenProductShowcase()
       => Controller.Run<ProductShowcasePresenter>();

    private void OpenShoppingCart()
       => Controller.Run<ShoppingCartPresenter>();
}
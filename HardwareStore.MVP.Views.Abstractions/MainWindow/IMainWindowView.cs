namespace HardwareStore.MVP.Views.Abstractions.MainWindow;

public interface IMainWindowView : IView
{
    event Action UserAccount;
    event Action ProductShowcase;
    event Action ShoppingCart;
}
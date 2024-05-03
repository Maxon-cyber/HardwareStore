namespace HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;

public interface IShoppingCartView : IView
{
    event Action Order;
}

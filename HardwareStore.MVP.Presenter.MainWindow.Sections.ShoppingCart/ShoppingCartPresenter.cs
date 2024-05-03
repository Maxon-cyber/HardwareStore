using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.Entities.Order;
using HardwareStore.MVP.Presenters.Base;
using HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;
using HardwareStore.Services.SqlServerService;
using HardwareStore.Services.SqlServerService.DataProcessing.Order;

namespace HardwareStore.MVP.Presenter.MainWindow.Sections.ShoppingCart;

public sealed class ShoppingCartPresenter : Presenter<IShoppingCartView>
{
    private readonly OrderService _service;

    public ShoppingCartPresenter(IApplicationController controller, IShoppingCartView view, SqlServerService service) 
        : base(controller, view)
    {
        _service = service.Order;
    }

    private async Task LoadOrders()
    {
        IEnumerable<OrderEntity> orders = await _service.SelectAsync();
    }
}
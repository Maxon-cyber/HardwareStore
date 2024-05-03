using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer;
using HardwareStore.Services.SqlServerService.DataProcessing.Order;
using HardwareStore.Services.SqlServerService.DataProcessing.Product;
using HardwareStore.Services.SqlServerService.DataProcessing.User;

namespace HardwareStore.Services.SqlServerService;

public sealed class SqlServerService(SqlServerRepository sqlServerRepository) : IDisposable, IAsyncDisposable
{
    private readonly Lazy<UserService> _userService = new Lazy<UserService>(() => new UserService(sqlServerRepository.UserRepository));
    private readonly Lazy<ProductService> _productService = new Lazy<ProductService>(() => new ProductService(sqlServerRepository.ProductRepository));
    private readonly Lazy<OrderService> _orderService = new Lazy<OrderService>(() => new OrderService(sqlServerRepository.Order));

    public UserService User => _userService.Value;

    public ProductService Product => _productService.Value;

    public OrderService Order => _orderService.Value;

    public void Dispose()
    {
        User?.Dispose();

        Product?.Dispose();

        Order.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (User != null)
            await User.DisposeAsync();

        if (Product != null)
            await Product.DisposeAsync();

        if (Order != null)
            await Order.DisposeAsync();
    }
}
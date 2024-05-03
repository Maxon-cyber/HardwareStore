using HardwareStore.DataAccess.Providers.Relational.Abstractions.Common;
using HardwareStore.DataAccess.Providers.Relational.SqlServer;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Order;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Product;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.User;

namespace HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer;

public sealed class SqlServerRepository
{
    private readonly Lazy<UserRepository> _userRepository;
    private readonly Lazy<ProductRepository> _productRepository;
    private readonly Lazy<OrderRepository> _orderRepository;

    public UserRepository UserRepository => _userRepository.Value;

    public ProductRepository ProductRepository => _productRepository.Value;

    public OrderRepository Order => _orderRepository.Value;

    public SqlServerRepository(ConnectionParameters connectionParameters)
    {
        SqlServerProvider sqlServer = new SqlServerProvider(connectionParameters);

        _userRepository = new Lazy<UserRepository>(() => new UserRepository(sqlServer));
        _productRepository = new Lazy<ProductRepository>(() => new ProductRepository(sqlServer));
        _orderRepository = new Lazy<OrderRepository>(() => new OrderRepository(sqlServer));
    }
}
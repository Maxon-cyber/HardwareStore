using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Order;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Queries;
using HardwareStore.Entities.Order;
using HardwareStore.Services.Abstractions;
using HardwareStore.Services.Utils.Monitoring.Caching.File;
using HardwareStore.Services.Utils.Monitoring.Logger.File;
using System.Collections.Immutable;
using System.Data;

namespace HardwareStore.Services.SqlServerService.DataProcessing.Order;

public sealed class OrderService : IService<OrderEntity>
{
    private readonly CachedFileManager<string, OrderEntity> _cache;
    private readonly FileLogger _logger;
    private readonly OrderRepository _orderRepository;
    private readonly CancellationToken _cancellationToken;

    public OrderService(OrderRepository orderRepository)
    {
        _cache = CachedFileManager<string, OrderEntity>.Create("");
        _logger = FileLogger.Create("").SetFile("User");
        _orderRepository = orderRepository;

        CancellationTokenSource tokenSource = new CancellationTokenSource();

        _cancellationToken = tokenSource.Token;
    }

    public async Task<OrderEntity?> GetByAsync(OrderEntity orderCondition)
    {
        if (await _cache.ContainsKeyAsync(""))
            return await _cache.ReadByKeyAsync("");

        DbResponse<OrderEntity> response = await _orderRepository.GetByAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetOrderByCondition,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true,
        },
        orderCondition,
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<OrderEntity?>(null);
        }

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");

        OrderEntity? order = response.QueryResult.Peek();

        if (order == null)
        {
            await _logger.LogInfoAsync("Заказ не найден");
            return await Task.FromResult<OrderEntity?>(null);
        }

        await _logger.LogInfoAsync("Заказ получен:");
        await _logger.LogInfoAsync($"\tId заказа:{order.Id}\n");

        string content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return order;
    }

    public async Task<IEnumerable<OrderEntity>?> SelectAsync()
    {
        DbResponse<OrderEntity> response = await _orderRepository.SelectAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetAllOrders,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true
        },
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<IEnumerable<OrderEntity>?>(null);
        }

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");

        string content = null!;
        if (!response.QueryResult.IsEmpty)
        {
            await _logger.LogInfoAsync("Заказы получены получены:");

            foreach (OrderEntity currentOrder in response.QueryResult)
                content += $"\tId Заказа:{currentOrder.Id}\n";

            await _logger.LogInfoAsync(content);
        }

        content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return response.QueryResult;
    }

    public async Task<IEnumerable<OrderEntity>?> SelectByAsync(OrderEntity orderCondition)
    {
        await _logger.LogInfoAsync("Запрос на получение продуктов\n".ToUpper());

        DbResponse<OrderEntity> response = await _orderRepository.SelectByAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetAllOrdersByCondition,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true
        },
        orderCondition,
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<IEnumerable<OrderEntity>?>(null);
        }

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");

        string content = null!;
        if (!response.QueryResult.IsEmpty)
        {
            await _logger.LogInfoAsync("Продукты получены:");

            foreach (OrderEntity currentOrder in response.QueryResult)
                content += $"\tId заказа:{currentOrder.Id}\n";

            await _logger.LogInfoAsync(content);
        }

        content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return response.QueryResult;
    }

    public async Task<object?> ChangeAsync(TypeOfUpdateCommand typeOfCommand, OrderEntity order)
    {
        await _logger.LogInfoAsync("Запрос на обновление заказа\n".ToUpper()
            + "\tКоличество заказов на обновление: 1");

        string command = typeOfCommand switch
        {
            TypeOfUpdateCommand.Insert => SqlServerStoredProcedureList.AddOrder,
            TypeOfUpdateCommand.Update => SqlServerStoredProcedureList.UpadateOrder,
            TypeOfUpdateCommand.Delete => SqlServerStoredProcedureList.DropOrder,
            _ => throw new NotImplementedException(),
        };

        DbResponse<OrderEntity> response = await _orderRepository.ChangeAsync(new QueryParameters()
        {
            CommandText = command,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true,
            OutputParameter = new Parameter()
            {
                Name = "@result",
                DbType = DbType.String,
                Size = -1,
                ParameterDirection = ParameterDirection.Output
            },
            ReturnedValue = new Parameter()
            {
                Name = "@returned_value",
                DbType = DbType.Int32,
                ParameterDirection = ParameterDirection.ReturnValue
            }
        },
        order,
        _cancellationToken);

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");
        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<object?>(null);
        }

        await _logger.LogInfoAsync("Дополнительные данные:");
        string content = null!;
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        await _logger.LogInfoAsync($"Выходное значение: {response.OutputValue}");
        await _logger.LogInfoAsync($"Возвращаемое значение: {response.ReturnedValue}");

        return response.ReturnedValue;
    }

    public async Task<ImmutableDictionary<string, object?>> ChangeAsync(TypeOfUpdateCommand typeOfCommand, IEnumerable<OrderEntity> orders)
    {
        await _logger.LogInfoAsync("Запрос на обновление пользователей\n".ToUpper() +
                               $"\tКоличество пользователей на обновление: {orders.Count()}");

        string command = typeOfCommand switch
        {
            TypeOfUpdateCommand.Insert => SqlServerStoredProcedureList.AddOrder,
            TypeOfUpdateCommand.Update => SqlServerStoredProcedureList.UpadateOrder,
            TypeOfUpdateCommand.Delete => SqlServerStoredProcedureList.DropOrder,
            _ => throw new NotImplementedException(),
        };

        IEnumerable<DbResponse<OrderEntity>> responses = await _orderRepository.ChangeAsync(new QueryParameters()
        {
            CommandText = command,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true,
            OutputParameter = new Parameter()
            {
                Name = "@result",
                Size = -1,
                DbType = DbType.Int32,
                ParameterDirection = ParameterDirection.Output
            },
            ReturnedValue = new Parameter()
            {
                Name = "@returned_value",
                DbType = DbType.Int32,
                ParameterDirection = ParameterDirection.ReturnValue
            }
        },
        orders,
        _cancellationToken);

        ImmutableDictionary<string, object?> result = ImmutableDictionary.Create<string, object?>();

        DbResponse<OrderEntity>[] responsesArray = responses.ToArray();

        for (int index = 0; index < responsesArray.Length; index++)
        {
            result.Add($"Заказ: {index}", null);

            DbResponse<OrderEntity> response = responsesArray[index];

            await _logger.LogInfoAsync($"Сообщение: {response.Message}");
            if (response.Error != null)
            {
                _logger.LogError(response.Error, response.Message);
                result.Add("Ошибка", response.OutputValue);
                continue;
            }

            await _logger.LogInfoAsync("Запрос выполнен");

            await _logger.LogInfoAsync("Дополнительные данные:");
            string content = null!;
            foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
                content += $"\t{additionallData.Key} - {additionallData.Value}\n";

            await _logger.LogInfoAsync(content);

            await _logger.LogInfoAsync($"Выходное значение: {response.OutputValue}");
            await _logger.LogInfoAsync($"Возвращаемое значение: {response.ReturnedValue}");

            result.Add("Выходное значение", response.OutputValue);
            result.Add("Возвращаемое значение", response.ReturnedValue);
        }
        return result;
    }

    public void Dispose()
    {
        GC.Collect();
        GC.SuppressFinalize(this);

        _orderRepository.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        GC.Collect();
        GC.SuppressFinalize(this);

        await _orderRepository.DisposeAsync();
    }
}
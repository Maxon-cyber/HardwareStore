using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Product;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Queries;
using HardwareStore.Entities.Product;
using HardwareStore.Services.Abstractions;
using HardwareStore.Services.Utils.Monitoring.Caching.File;
using HardwareStore.Services.Utils.Monitoring.Logger.File;
using System.Collections.Immutable;
using System.Data;

namespace HardwareStore.Services.SqlServerService.DataProcessing.Product;

public sealed class ProductService : IService<ProductEntity>
{
    private readonly CachedFileManager<string, ProductEntity> _cache;
    private readonly FileLogger _logger;
    private readonly ProductRepository _productRepository;
    private readonly CancellationToken _cancellationToken;

    public ProductService(ProductRepository productRepository)
    {
        _cache = CachedFileManager<string, ProductEntity>.Create("");
        _logger = FileLogger.Create("").SetFile("User");
        _productRepository = productRepository;

        CancellationTokenSource tokenSource = new CancellationTokenSource();

        _cancellationToken = tokenSource.Token;
    }

    public async Task<ProductEntity?> GetByAsync(ProductEntity productCondition)
    {
        DbResponse<ProductEntity> response = await _productRepository.GetByAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetProductByCondition,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true,
        },
        productCondition,
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<ProductEntity?>(null);
        }

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");

        ProductEntity? product = response.QueryResult.Peek();

        if (product == null)
        {
            await _logger.LogInfoAsync("Продукт не найден");
            return await Task.FromResult<ProductEntity?>(null);
        }

        await _logger.LogInfoAsync("Продукт получен:");
        await _logger.LogInfoAsync($"\tId продукта:{product.Id}\n");

        string content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return product;
    }

    public async Task<IEnumerable<ProductEntity>?> SelectAsync()
    {
        DbResponse<ProductEntity> response = await _productRepository.SelectAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetAllProducts,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true
        },
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<IEnumerable<ProductEntity>?>(null);
        }

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");

        string content = null!;
        if (!response.QueryResult.IsEmpty)
        {
            await _logger.LogInfoAsync("Продукты получены получены:");

            foreach (ProductEntity currentProduct in response.QueryResult)
                content += $"\tId продукта:{currentProduct.Id}\n";

            await _logger.LogInfoAsync(content);
        }

        content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return response.QueryResult;
    }

    public async Task<IEnumerable<ProductEntity>?> SelectByAsync(ProductEntity productCondition)
    {
        await _logger.LogInfoAsync("Запрос на получение продуктов\n".ToUpper());

        DbResponse<ProductEntity> response = await _productRepository.SelectByAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetAllProductsByCondition,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true
        },
        productCondition,
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<IEnumerable<ProductEntity>?>(null);
        }

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");

        string content = null!;
        if (!response.QueryResult.IsEmpty)
        {
            await _logger.LogInfoAsync("Продукты получены:");

            foreach (ProductEntity currentProduct in response.QueryResult)
                content += $"\tId продукта:{currentProduct.Id}\n";

            await _logger.LogInfoAsync(content);
        }

        content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return response.QueryResult;
    }

    public async Task<object?> ChangeAsync(TypeOfUpdateCommand typeOfCommand, ProductEntity product)
    {
        await _logger.LogInfoAsync("Запрос на обновление продукта\n".ToUpper()
            + "\tКоличество продуктов на обновление: 1");

        string command = typeOfCommand switch
        {
            TypeOfUpdateCommand.Insert => SqlServerStoredProcedureList.AddProduct,
            TypeOfUpdateCommand.Update => SqlServerStoredProcedureList.UpadateProduct,
            TypeOfUpdateCommand.Delete => SqlServerStoredProcedureList.DropProduct,
            _ => throw new NotImplementedException(),
        };

        DbResponse<ProductEntity> response = await _productRepository.ChangeAsync(new QueryParameters()
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
        product,
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

    public async Task<ImmutableDictionary<string, object?>> ChangeAsync(TypeOfUpdateCommand typeOfCommand, IEnumerable<ProductEntity> products)
    {
        await _logger.LogInfoAsync("Запрос на обновление продуктов\n".ToUpper() +
                               $"\tКоличество продуктов на обновление: {products.Count()}");

        string command = typeOfCommand switch
        {
            TypeOfUpdateCommand.Insert => SqlServerStoredProcedureList.AddProduct,
            TypeOfUpdateCommand.Update => SqlServerStoredProcedureList.UpadateProduct,
            TypeOfUpdateCommand.Delete => SqlServerStoredProcedureList.DropProduct,
            _ => throw new NotImplementedException(),
        };

        IEnumerable<DbResponse<ProductEntity>> responses = await _productRepository.ChangeAsync(new QueryParameters()
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
        products,
        _cancellationToken);

        ImmutableDictionary<string, object?> result = ImmutableDictionary.Create<string, object?>();

        DbResponse<ProductEntity>[] responsesArray = responses.ToArray();

        for (int index = 0; index < responsesArray.Length; index++)
        {
            result.Add($"Продукт: {index}", null);

            DbResponse<ProductEntity> response = responsesArray[index];

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

        _productRepository.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        GC.Collect();
        GC.SuppressFinalize(this);

        await _productRepository.DisposeAsync();
    }
}
using HardwareStore.DataAccess.Providers.Relational.Models;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.Queries;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer.User;
using HardwareStore.Entities.User;
using HardwareStore.Services.Abstractions;
using HardwareStore.Services.Utils.Monitoring.Caching.InMemory;
using HardwareStore.Services.Utils.Monitoring.Logger.File;
using System.Collections.Immutable;
using System.Data;

namespace HardwareStore.Services.SqlServerService.DataProcessing.User;

public sealed class UserService : IService<UserEntity>
{
    private readonly MemoryCache<string, UserEntity> _cache;
    private readonly FileLogger _logger;
    private readonly UserRepository _userRepository;
    private readonly CancellationToken _cancellationToken;

    public UserService(UserRepository userRepository)
    {
        _cache = new MemoryCache<string, UserEntity>();
        _logger = FileLogger.Create("").SetFile("User");
        _userRepository = userRepository;

        CancellationTokenSource tokenSource = new CancellationTokenSource();

        _cancellationToken = tokenSource.Token;
    }

    public async Task<UserEntity?> GetByAsync(UserEntity userCondition)
    {
        if (await _cache.ContainsKeyAsync("User"))
            return await _cache.ReadByKeyAsync("User");

        DbResponse<UserEntity> response = await _userRepository.GetByAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetUserByCondition,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true,
        },
        userCondition,
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<UserEntity?>(null);
        }

        await _logger.LogInfoAsync("Запрос выполнен");

        UserEntity? user = response.QueryResult.Peek();

        if (user == null)
        {
            await _logger.LogInfoAsync("Пользователь не найден");
            return await Task.FromResult<UserEntity?>(null);
        }

        await _logger.LogInfoAsync("Пользователь получен:");
        await _logger.LogInfoAsync($"\tId пользователя:{user.Id}\n");

        await _cache.WriteAsync("User", user);

        string content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return user;
    }

    public async Task<IEnumerable<UserEntity>?> SelectAsync()
    {
        DbResponse<UserEntity> response = await _userRepository.SelectAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetAllUsers,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true
        },
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<IEnumerable<UserEntity>?>(null);
        }

        await _logger.LogInfoAsync("Запрос выполнен");

        string content = null!;
        if (!response.QueryResult.IsEmpty)
        {
            await _logger.LogInfoAsync("Пользователи получены:");

            foreach (UserEntity currentUser in response.QueryResult)
                content += $"\tId пользователя:{currentUser.Id}\n";

            await _logger.LogInfoAsync(content);
        }

        content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return response.QueryResult;
    }

    public async Task<IEnumerable<UserEntity>?> SelectByAsync(UserEntity userCondition)
    {
        await _logger.LogInfoAsync("Запрос на получение пользователя\n".ToUpper());

        DbResponse<UserEntity> response = await _userRepository.SelectByAsync(new QueryParameters()
        {
            CommandText = SqlServerStoredProcedureList.GetUsersByCondition,
            CommandType = CommandType.StoredProcedure,
            TransactionManagementOnDbServer = true
        },
        userCondition,
        _cancellationToken);

        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<IEnumerable<UserEntity>?>(null);
        }

        await _logger.LogInfoAsync("Запрос выполнен");

        string content = null!;
        if (!response.QueryResult.IsEmpty)
        {
            await _logger.LogInfoAsync("Пользователи получены:");

            foreach (UserEntity currentUser in response.QueryResult)
                content += $"\tId пользователя:{currentUser.Id}\n";

            await _logger.LogInfoAsync(content);
        }

        content = null!;

        await _logger.LogInfoAsync("Дополнительные данные:");
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        return response.QueryResult;
    }

    public async Task<object?> ChangeAsync(TypeOfUpdateCommand typeOfCommand, UserEntity user)
    {
        await _logger.LogInfoAsync("Запрос на обновление пользователя\n".ToUpper()
            + "\tКоличество пользователей на обновление: 1");

        string command = typeOfCommand switch
        {
            TypeOfUpdateCommand.Insert => SqlServerStoredProcedureList.AddUser,
            TypeOfUpdateCommand.Update => SqlServerStoredProcedureList.UpadateUser,
            TypeOfUpdateCommand.Delete => SqlServerStoredProcedureList.DropUser,
            _ => throw new NotImplementedException(),
        };

        DbResponse<UserEntity> response = await _userRepository.ChangeAsync(new QueryParameters()
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
                DbType = DbType.Boolean,
                ParameterDirection = ParameterDirection.ReturnValue
            }
        },
        user,
        _cancellationToken);

        await _logger.LogInfoAsync($"Сообщение: {response.Message}");
        if (response.Error != null)
        {
            _logger.LogError(response.Error, response.Message);
            return await Task.FromResult<object?>(null);
        }

        await _logger.LogInfoAsync("Запрос выполнен");

        await _logger.LogInfoAsync("Дополнительные данные:");
        string content = null!;
        foreach (KeyValuePair<object, object> additionallData in response.AdditionalData)
            content += $"\t{additionallData.Key} - {additionallData.Value}\n";

        await _logger.LogInfoAsync(content);

        await _logger.LogInfoAsync($"Выходное значение: {response.OutputValue}");
        await _logger.LogInfoAsync($"Возвращаемое значение: {response.ReturnedValue}");

        return response.ReturnedValue;
    }

    public async Task<ImmutableDictionary<string, object?>> ChangeAsync(TypeOfUpdateCommand typeOfCommand, IEnumerable<UserEntity> users)
    {
        await _logger.LogInfoAsync("Запрос на обновление пользователей\n".ToUpper() +
                               $"\tКоличество пользователей на обновление: {users.Count()}");

        string command = typeOfCommand switch
        {
            TypeOfUpdateCommand.Insert => SqlServerStoredProcedureList.AddUser,
            TypeOfUpdateCommand.Update => SqlServerStoredProcedureList.AddUser,
            TypeOfUpdateCommand.Delete => SqlServerStoredProcedureList.DropUser,
            _ => throw new NotImplementedException(),
        };

        IEnumerable<DbResponse<UserEntity>> responses = await _userRepository.ChangeAsync(new QueryParameters()
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
                DbType = DbType.Boolean,
                ParameterDirection = ParameterDirection.ReturnValue
            }
        },
        users,
        _cancellationToken);

        ImmutableDictionary<string, object?> result = ImmutableDictionary.Create<string, object?>();

        DbResponse<UserEntity>[] responsesArray = responses.ToArray();

        for (int index = 0; index < responsesArray.Length; index++)
        {
            result.Add($"Пользователь: {index}", null);

            DbResponse<UserEntity> response = responsesArray[index];

            await _logger.LogInfoAsync($"Сообщение: {response.Message}");
            if (response.Error != null)
            {
                _logger.LogError(response.Error, response.Message);
                result.Add($"Ошибка {index}", response.OutputValue);
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

        _userRepository.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        GC.Collect();
        GC.SuppressFinalize(this);

        await _userRepository.DisposeAsync();
    }
}
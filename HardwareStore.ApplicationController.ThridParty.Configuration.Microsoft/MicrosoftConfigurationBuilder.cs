using HardwareStore.ApplicationController.ThridParty.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;

namespace HardwareStore.ApplicationController.ThridParty.Configuration.Microsoft;

public sealed class MicrosoftConfigurationBuilder() : IApplicationConfigurationBuilder
{
    private static bool _isBuilded = false;
    private readonly IConfigurationBuilder _configurationBuilder = new ConfigurationManager();

    public IConfigurationRoot Root { get; private set; }

    public IApplicationConfigurationBuilder SetBasePath(string basePath)
    {
        _configurationBuilder.SetBasePath(basePath);
        return this;
    }

    public IApplicationConfigurationBuilder AddFile(string fileName, bool optional, bool reloadOnChange)
    {
        switch (Path.GetExtension(fileName))
        {
            case ".yml":
                _configurationBuilder.AddYamlFile(fileName, optional, reloadOnChange);
                break;
            default:
                throw new ArgumentException($"Провайдер конфигурации для файла {fileName} не найден");
        }

        return this;
    }

    public IApplicationConfigurationBuilder AddEnviromentVariables()
    {
        _configurationBuilder.AddEnvironmentVariables();
        return this;
    }

    public void Build()
    {
        if (_isBuilded)
            return;

        Root = _configurationBuilder.Build();
        _isBuilded = true;
    }
}
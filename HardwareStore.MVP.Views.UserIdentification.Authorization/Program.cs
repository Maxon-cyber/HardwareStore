using HardwareStore.ApplicationController;
using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.ApplicationController.ThridParty.Configuration.Microsoft;
using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions.Common;
using HardwareStore.ApplicationController.ThridParty.IoC.Autofac;
using HardwareStore.DataAccess.Providers.Relational.Abstractions.Common;
using HardwareStore.DataAccess.Providers.Repositories.Relational.SqlServer;
using HardwareStore.MVP.Presenter.MainWindow.Sections.ShoppingCart;
using HardwareStore.MVP.Presenters.MainWindow;
using HardwareStore.MVP.Presenters.MainWindow.ProductShowcase;
using HardwareStore.MVP.Presenters.MainWindow.UserAccount;
using HardwareStore.MVP.Presenters.UserIdentification;
using HardwareStore.MVP.Views.Abstractions.MainWindow;
using HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;
using HardwareStore.MVP.Views.Abstractions.UserIdentification;
using HardwareStore.MVP.Views.MainWindow;
using HardwareStore.MVP.Views.MainWindow.Sections.ProductShowcase;
using HardwareStore.MVP.Views.UserIdentification.Registration;
using Microsoft.Extensions.Configuration;

namespace HardwareStore.MVP.Views.UserIdentification.Authorization;

internal static class Program
{
    private static readonly ApplicationContext _context = new ApplicationContext();

    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        Application.SetCompatibleTextRenderingDefault(false);

        Controller<AutofacBuilder, MicrosoftConfigurationBuilder> applicationController = new Controller<AutofacBuilder, MicrosoftConfigurationBuilder>();

        applicationController.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                                           .AddFile("configuration.yml", true, false)
                                           .AddEnviromentVariables()
                                           .Build();

        IConfigurationSection sqlServer = applicationController.Configuration.Root.GetSection("Databases:SqlServer");

        IDictionary<string, ConnectionParameters> parametersOfAllDatabases = new Dictionary<string, ConnectionParameters>
        {
            {
                sqlServer.Key,
                new ConnectionParameters()
                {
                    Provider = sqlServer.Key,
                    Server = sqlServer.GetSection("Server").Value!,
                    Port = Convert.ToInt32(sqlServer.GetSection("Port").Value),
                    Database = sqlServer.GetSection("Database").Value!,
                    IntegratedSecurity = Convert.ToBoolean(sqlServer.GetSection("IntegratedSecurity").Value),
                    Username = sqlServer.GetSection("Username").Value,
                    Password = sqlServer.GetSection("Password").Value,
                    TrustedConnection = Convert.ToBoolean(sqlServer.GetSection("TrustedConnection").Value),
                    TrustServerCertificate = Convert.ToBoolean(sqlServer.GetSection("TrustServerCertificate").Value),
                    ConnectionTimeout = TimeSpan.Parse(sqlServer.GetSection("ConnectionTimeout").Value!),
                    MaxPoolSize = Convert.ToInt32(sqlServer.GetSection("MaxPoolSize").Value)
                }
            },
        };

        applicationController.Container.Register<AuthorizationPresenter>(asSelf: true)
                                       .Register<RegistrationPresenter>(asSelf: true)
                                       .Register<MainWindowPresenter>(asSelf: true)
                                       .Register<ShoppingCartPresenter>(asSelf: true)
                                       .Register<ProductShowcasePresenter>(asSelf: true)
                                       .Register<UserAccountPresenter>(asSelf: true)
                                       .RegisterView<IAuthorizationView, AuthorizationForm>(Lifetime.Singleton)
                                       .RegisterView<IRegistrationView, RegistrationControl>(Lifetime.Singleton)
                                       .RegisterView<IMainWindowView, MainWindowControl>(Lifetime.Singleton)
                                       .RegisterView<IUserAccountView, UserAccountControl>(Lifetime.Singleton)
                                       .RegisterView<IProductShowcaseView, ProductShowcaseControl>(Lifetime.Singleton)
                                       .RegisterView<IShoppingCartView, ShoppngCartControl>(Lifetime.Singleton)
                                       .RegisterWithConstructor<SqlServerRepository>("connectionParameters", parametersOfAllDatabases["SqlServer"])
                                       .RegisterInstance(_context, Lifetime.Singleton)
                                       .RegisterInstance<IApplicationController>(applicationController)
                                       .Build();

        applicationController.Run<AuthorizationPresenter>();
    }
}
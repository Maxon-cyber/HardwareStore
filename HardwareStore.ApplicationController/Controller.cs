using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.ApplicationController.ThridParty.Configuration.Abstractions;
using HardwareStore.ApplicationController.ThridParty.IoC.Abstractions;
using HardwareStore.MVP.Presenters.Abstractions;

namespace HardwareStore.ApplicationController;

public sealed class Controller<TIoCContainer, TApplicationConfiguration>() : IApplicationController
    where TIoCContainer : IIoCContainerBuilder, new()
    where TApplicationConfiguration : IApplicationConfigurationBuilder, new()
{
    public TIoCContainer Container { get; } = new TIoCContainer();

    public TApplicationConfiguration Configuration { get; } = new TApplicationConfiguration();

    public void Run<TPresenter>()
        where TPresenter : class, IPresenter
    {
        TPresenter presenter = Container.Resolve<TPresenter>();
        presenter.Run();
    }

    public void Run<TPresenter>(Action<TPresenter> action)
         where TPresenter : class, IPresenter
    {
        TPresenter presenter = Container.Resolve<TPresenter>();
        presenter.Run(action, presenter);
    }

    public void Run<TPresenter>(Action action)
        where TPresenter : class, IPresenter
    {
        TPresenter presenter = Container.Resolve<TPresenter>();
        presenter.Run(action);
    }
}
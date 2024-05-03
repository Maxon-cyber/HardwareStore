using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.MVP.Presenters.Abstractions;
using HardwareStore.MVP.Views.Abstractions;

namespace HardwareStore.MVP.Presenters.Base;

public abstract class Presenter<TView> : IPresenter
    where TView : IView
{
    protected TView View { get; }

    protected IApplicationController Controller { get; }

    protected Presenter(IApplicationController controller, TView view)
        => (Controller, View) = (controller, view);

    public void Run()
        => View.Show();

    public void Run<TPresenter>(Action<TPresenter> action, TPresenter presenter)
    {
        action(presenter);
        View.Show();
    }

    public void Run(Action action)
    {
        action();
        View.Show();
    }
}
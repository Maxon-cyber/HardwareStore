using HardwareStore.MVP.Presenters.Abstractions;

namespace HardwareStore.ApplicationController.Abstractions;

public interface IApplicationController
{
    void Run<TPresenter>()
        where TPresenter : class, IPresenter;

    void Run<TPresenter>(Action<TPresenter> action)
        where TPresenter : class, IPresenter;

    void Run<TPresenter>(Action action)
        where TPresenter : class, IPresenter;
}
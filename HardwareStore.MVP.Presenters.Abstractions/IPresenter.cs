namespace HardwareStore.MVP.Presenters.Abstractions;

public interface IPresenter
{
    void Run();

    void Run<TPresenter>(Action<TPresenter> action, TPresenter presenter);

    void Run(Action action);
}
using HardwareStore.MVP.Views.Abstractions.Common;

namespace HardwareStore.MVP.Views.Abstractions;

public interface IView
{
    void Show();

    void ShowMessage(string message, string caption, MessageLevel level);

    void Close();
}
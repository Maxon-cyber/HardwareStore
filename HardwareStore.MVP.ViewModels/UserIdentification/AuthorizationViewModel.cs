namespace HardwareStore.MVP.ViewModels.UserIdentification;

public sealed class AuthorizationViewModel()
{
    public required string Login { get; init; }

    public required byte[] Password { get; init; }
}
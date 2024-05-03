namespace HardwareStore.MVP.ViewModels.MainWindow.Sections.UserAccount;

public sealed class UserAccountModel()
{
    public required string Name { get; init; }

    public required string SecondName { get; init; }

    public required string Patronymic { get; init; }

    public required string Gender { get; init; }

    public required uint Age { get; init; }

    public required string Login { get; init; }

    public required byte[] Password { get; init; }

    public required string HouseNumber { get; set; }

    public required string Street { get; set; }

    public required string City { get; init; }

    public required string Region { get; init; }

    public required string Country { get; init; }
}
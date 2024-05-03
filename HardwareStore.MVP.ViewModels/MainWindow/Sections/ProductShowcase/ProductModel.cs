namespace HardwareStore.MVP.ViewModels.MainWindow.Sections.ProductShowcase;

public sealed class ProductModel()
{
    public required string Name { get; init; }

    public required byte[] Image { get; init; }

    public required uint Quantity { get; init; }

    public required string Category { get; init; }

    public required decimal Price { get; init; }
}
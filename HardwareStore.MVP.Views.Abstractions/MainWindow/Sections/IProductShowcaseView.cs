using HardwareStore.MVP.ViewModels.MainWindow.Sections.ProductShowcase;

namespace HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;

public interface IProductShowcaseView : IView
{
    event Func<Task> LoadProducts;

    void DisplayProductsAsync(IList<ProductModel> products);
}
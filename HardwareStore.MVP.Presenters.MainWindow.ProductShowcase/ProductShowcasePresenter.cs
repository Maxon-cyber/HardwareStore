using HardwareStore.ApplicationController.Abstractions;
using HardwareStore.Entities.Product;
using HardwareStore.MVP.Presenters.Base;
using HardwareStore.MVP.ViewModels.MainWindow.Sections.ProductShowcase;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;
using HardwareStore.Services.SqlServerService;
using HardwareStore.Services.SqlServerService.DataProcessing.Product;

namespace HardwareStore.MVP.Presenters.MainWindow.ProductShowcase;

public sealed class ProductShowcasePresenter : Presenter<IProductShowcaseView>
{
    private readonly ProductService _service;

    public ProductShowcasePresenter(IApplicationController controller, IProductShowcaseView view, SqlServerService service)
        : base(controller, view)
    {
        _service = service.Product;
        View.LoadProducts += LoadProductsAsync;
    }

    private async Task LoadProductsAsync()
    {
        try
        {
            await Task.Run(async () =>
            {
                IEnumerable<ProductEntity>? products = await _service.SelectAsync();

                ProductEntity[] arrayProducts = products.ToArray();
                List<ProductModel> productModels = [];

                for (int index = 0; index < products.Count(); index++)
                {
                    ProductEntity currentProduct = arrayProducts[index];
                    productModels.Add(new ProductModel()
                    {
                        Name = currentProduct.Name,
                        Image = currentProduct.Image,
                        Quantity = currentProduct.Quantity,
                        Category = currentProduct.Category,
                        Price = currentProduct.Price
                    });
                }

                View.DisplayProductsAsync(productModels);
            });
        }
        catch (Exception ex)
        {
            View.ShowMessage(ex.Message, "Ошибка", MessageLevel.Error);
        }
    }
}
using HardwareStore.MVP.ViewModels.MainWindow.Sections.ProductShowcase;

namespace HardwareStore.MVP.Views.MainWindow.Sections.Product;

public sealed partial class ProductControl : UserControl
{
    public event Action Add;
    public event Action Remove;

    public ProductControl()
    {
        InitializeComponent();
    }

    public void CreateProductView(ProductModel product)
    {

    }
}
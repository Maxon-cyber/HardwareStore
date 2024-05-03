using HardwareStore.MVP.ViewModels.MainWindow.Sections.ProductShowcase;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.MainWindow.Sections;
using HardwareStore.MVP.Views.MainWindow.Sections.Product;

namespace HardwareStore.MVP.Views.MainWindow.Sections.ProductShowcase;

public sealed partial class ProductShowcaseControl : UserControl, IProductShowcaseView
{
    public event Func<Task> LoadProducts;

    public ProductShowcaseControl()
    {
        InitializeComponent();

        LoadProducts.Invoke();
    }

    public new void Show()
        => base.Show();

    public void DisplayProductsAsync(IList<ProductModel> products)
    {
        int countOfProducts = products.Count;

        int columnTPL = viewProductsTLP.ColumnCount;
        int rowTPL = countOfProducts / columnTPL;
        viewProductsTLP.RowCount = rowTPL;

        viewProductsTLP.SuspendLayout();
        for (int index = 0; index < countOfProducts; index++)
        {
            int column = index % columnTPL;
            int row = index / columnTPL;

            ProductControl productControl = new ProductControl();
            productControl.CreateProductView(products[index]);
            viewProductsTLP.Controls.Add(productControl, column, row);
        }
        viewProductsTLP.ResumeLayout();

        UpdateRowAndColumnStyles(rowTPL, columnTPL);

        viewProductsTLP.Invalidate();
    }

    private void UpdateRowAndColumnStyles(int rowTPL, int columnTPL)
    {
        TableLayoutRowStyleCollection rowStyles = viewProductsTLP.RowStyles;
        TableLayoutColumnStyleCollection columnStyles = viewProductsTLP.ColumnStyles;

        rowStyles.Clear();
        columnStyles.Clear();

        for (int rowIndex = 0; rowIndex < rowTPL; rowIndex++)
            rowStyles.Add(new RowStyle() { Height = 100F, SizeType = SizeType.Percent });
        for (int columnIndex = 0; columnIndex < columnTPL; columnIndex++)
            columnStyles.Add(new ColumnStyle() { Width = 50F, SizeType = SizeType.Percent });
    }
    public void ShowMessage(string message, string caption, MessageLevel level)
    {
        MessageBoxIcon messageBoxIcon = level switch
        {
            MessageLevel.Info => MessageBoxIcon.Information,
            MessageLevel.Warning => MessageBoxIcon.Warning,
            MessageLevel.Error => MessageBoxIcon.Error,
            _ => MessageBoxIcon.None,
        };

        MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, messageBoxIcon);
    }

    public void Close()
        => Parent.Controls.Remove(this);
}
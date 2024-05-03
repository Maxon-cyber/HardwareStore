namespace HardwareStore.MVP.Views.MainWindow.Sections.Product;

public sealed partial class ProductControl
{
    private System.ComponentModel.IContainer _components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (_components != null))
            _components.Dispose();

        base.Dispose(disposing);
    }

    #region Component Designer generated code
    private void InitializeComponent()
    {
        _components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
    }
    #endregion
}
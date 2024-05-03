using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.MainWindow;

namespace HardwareStore.MVP.Views.MainWindow;

public sealed partial class MainWindowControl : UserControl, IMainWindowView
{
    private readonly ApplicationContext _context;

    public MainWindowControl(ApplicationContext context)
    {
        _context = context;
        InitializeComponent();
    }


    public event Action UserAccount;
    public event Action ProductShowcase;
    public event Action ShoppingCart;

    public new void Show()
    {
        _context.MainForm = (Form)this;
        base.Show();
    }

    private void BtnOpenUserAccount_Click(object sender, EventArgs e)
        => UserAccount?.Invoke();

    private void BtnOpenProductShowcase_Click(object sender, EventArgs e)
        => ProductShowcase?.Invoke();

    private void BtnOpenShoppingCart_Click(object sender, EventArgs e)
        => ShoppingCart?.Invoke();

    public void Close()
    {
        throw new NotImplementedException();
    }

    public void ShowMessage(string message, string caption, MessageLevel level)
    {
        throw new NotImplementedException();
    }
}
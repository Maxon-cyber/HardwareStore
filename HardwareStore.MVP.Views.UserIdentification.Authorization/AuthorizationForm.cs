using HardwareStore.MVP.ViewModels.UserIdentification;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.UserIdentification;
using System.Text;

namespace HardwareStore.MVP.Views.UserIdentification.Authorization;

public sealed partial class AuthorizationForm : Form, IAuthorizationView
{
    private bool _isRunable = false;
    private readonly ApplicationContext _context;

    public event Action<AuthorizationViewModel>? Authorization;
    public event Action? Registration;

    public AuthorizationForm(ApplicationContext context)
    {
        InitializeComponent();

        _context = context;
    }

    public new void Show()
    {
        if (!_isRunable)
        {
            _isRunable = true;

            _context.MainForm = this;
            Application.Run(_context);
        }
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        IEnumerable<TextBox> emptyTextBoxes = Controls.OfType<TextBox>().Where(tb => string.IsNullOrWhiteSpace(tb.Text));

        if (emptyTextBoxes.Any())
        {
            foreach (TextBox textBox in emptyTextBoxes)
                errorProvider.SetError(textBox, $"¬ведите значение {textBox.PlaceholderText}");

            return;
        }

        Authorization?.Invoke(new AuthorizationViewModel()
        {
            Login = loginTextBox.Text,
            Password = Encoding.UTF8.GetBytes(passwordTextBox.Text)
        });
    }

    private void BtnRegistration_Click(object sender, EventArgs e)
        => Registration?.Invoke();

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

    public new void Close()
        => base.Close();
}
using HardwareStore.MVP.ViewModels.UserIdentification;
using HardwareStore.MVP.Views.Abstractions.Common;
using HardwareStore.MVP.Views.Abstractions.UserIdentification;
using System.Text;

namespace HardwareStore.MVP.Views.UserIdentification.Registration;

public sealed partial class RegistrationControl : UserControl, IRegistrationView
{
    public RegistrationViewModel Model { get; private set; }

    public event Action Registration;
    public event Action ReturnToAuthorization;

    public RegistrationControl()
       => InitializeComponent();

    public new void Show()
        => base.Show();

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

    private void BtnRegistration_Click(object sender, EventArgs e)
    {
        IEnumerable<TextBox> emptyTextBoxes = Controls.OfType<TextBox>().Where(tb => string.IsNullOrWhiteSpace(tb.Text));

        if (emptyTextBoxes.Any())
        {
            foreach (TextBox textBox in emptyTextBoxes)
                errorProvider.SetError(textBox, $"Введите значение {textBox.PlaceholderText}");

            return;
        }

        IEnumerable<RadioButton> nonChecked = genderPanel.Controls.OfType<RadioButton>().Where(ch => ch.Checked == false);

        if (nonChecked.Any())
        {
            foreach (TextBox textBox in emptyTextBoxes)
                errorProvider.SetError(textBox, $"Выберите пол");

            return;
        }

        Model = new RegistrationViewModel()
        {
            Name = nameTextBox.Text,
            SecondName = secondNameTextBox.Text,
            Patronymic = patronymicTextBox.Text,
            Gender = genderPanel.Controls.OfType<RadioButton>().FirstOrDefault(rb => rb.Checked == true).Text.ToString().Replace(".", string.Empty),
            Age = Convert.ToUInt32(ageTextBox.Text),
            HouseNumber = houseNumberTextBox.Text,
            Street = streetTextBox.Text,
            City = cityTextBox.Text,
            Region = regionTextBox.Text,
            Country = countryTextBox.Text,
            Login = nameTextBox.Text,
            Password = Encoding.UTF8.GetBytes(passwordTextBox.Text.ToCharArray()),
        };

        Registration.Invoke();
    }

    private void BtnReturnToAuthorization_Click(object sender, EventArgs e)
        => ReturnToAuthorization.Invoke();

    public void Close()
        => Parent.Controls.Remove(this);
}
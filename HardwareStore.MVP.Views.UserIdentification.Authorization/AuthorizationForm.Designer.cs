namespace HardwareStore.MVP.Views.UserIdentification.Authorization;

public sealed partial class AuthorizationForm
{
    private System.ComponentModel.IContainer _components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (_components != null))
            _components.Dispose();

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        loginButton = new Button();
        loginTextBox = new TextBox();
        passwordTextBox = new TextBox();
        errorProvider = new ErrorProvider(components);
        registrationButton = new Button();
        ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
        SuspendLayout();
        // 
        // loginButton
        // 
        loginButton.Location = new Point(97, 349);
        loginButton.Name = "loginButton";
        loginButton.Size = new Size(75, 23);
        loginButton.TabIndex = 0;
        loginButton.Text = "Войти";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += BtnLogin_Click;
        // 
        // loginTextBox
        // 
        loginTextBox.Location = new Point(85, 212);
        loginTextBox.Name = "loginTextBox";
        loginTextBox.PlaceholderText = "Логин";
        loginTextBox.Size = new Size(100, 23);
        loginTextBox.TabIndex = 1;
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new Point(85, 271);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PasswordChar = '*';
        passwordTextBox.PlaceholderText = "Пароль";
        passwordTextBox.Size = new Size(100, 23);
        passwordTextBox.TabIndex = 2;
        // 
        // errorProvider
        // 
        errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        errorProvider.ContainerControl = this;
        // 
        // registrationButton
        // 
        registrationButton.Location = new Point(97, 392);
        registrationButton.Name = "registrationButton";
        registrationButton.Size = new Size(75, 23);
        registrationButton.TabIndex = 3;
        registrationButton.Text = "Регистрация";
        registrationButton.UseVisualStyleBackColor = true;
        registrationButton.Click += BtnRegistration_Click;
        // 
        // AuthorizationForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(274, 510);
        Controls.Add(registrationButton);
        Controls.Add(passwordTextBox);
        Controls.Add(loginTextBox);
        Controls.Add(loginButton);
        Name = "AuthorizationForm";
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
    #endregion

    private Button loginButton;
    private TextBox loginTextBox;
    private TextBox passwordTextBox;
    private ErrorProvider errorProvider;
    private System.ComponentModel.IContainer components;
    private Button registrationButton;
}
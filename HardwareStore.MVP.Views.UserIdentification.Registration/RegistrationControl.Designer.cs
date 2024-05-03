namespace HardwareStore.MVP.Views.UserIdentification.Registration;

public sealed partial class RegistrationControl
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
        components = new System.ComponentModel.Container();
        button1 = new Button();
        button2 = new Button();
        errorProvider = new ErrorProvider(components);
        nameTextBox = new TextBox();
        secondNameTextBox = new TextBox();
        patronymicTextBox = new TextBox();
        ageTextBox = new TextBox();
        houseNumberTextBox = new TextBox();
        streetTextBox = new TextBox();
        cityTextBox = new TextBox();
        regionTextBox = new TextBox();
        countryTextBox = new TextBox();
        loginTextBox = new TextBox();
        passwordTextBox = new TextBox();
        genderPanel = new Panel();
        manCheckBox = new CheckBox();
        womanCheckBox = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
        genderPanel.SuspendLayout();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new Point(92, 397);
        button1.Name = "button1";
        button1.Size = new Size(75, 23);
        button1.TabIndex = 0;
        button1.Text = "button1";
        button1.UseVisualStyleBackColor = true;
        // 
        // button2
        // 
        button2.Location = new Point(92, 444);
        button2.Name = "button2";
        button2.Size = new Size(75, 23);
        button2.TabIndex = 1;
        button2.Text = "button2";
        button2.UseVisualStyleBackColor = true;
        // 
        // errorProvider
        // 
        errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        errorProvider.ContainerControl = this;
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new Point(18, 64);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.PlaceholderText = "Имя";
        nameTextBox.Size = new Size(100, 23);
        nameTextBox.TabIndex = 2;
        // 
        // secondNameTextBox
        // 
        secondNameTextBox.Location = new Point(18, 104);
        secondNameTextBox.Name = "secondNameTextBox";
        secondNameTextBox.PlaceholderText = "Фамилия";
        secondNameTextBox.Size = new Size(100, 23);
        secondNameTextBox.TabIndex = 3;
        // 
        // patronymicTextBox
        // 
        patronymicTextBox.Location = new Point(18, 149);
        patronymicTextBox.Name = "patronymicTextBox";
        patronymicTextBox.PlaceholderText = "Отчество";
        patronymicTextBox.Size = new Size(100, 23);
        patronymicTextBox.TabIndex = 4;
        // 
        // ageTextBox
        // 
        ageTextBox.Location = new Point(18, 193);
        ageTextBox.Name = "ageTextBox";
        ageTextBox.PlaceholderText = "Возраст";
        ageTextBox.Size = new Size(100, 23);
        ageTextBox.TabIndex = 5;
        // 
        // houseNumberTextBox
        // 
        houseNumberTextBox.Location = new Point(143, 64);
        houseNumberTextBox.Name = "houseNumberTextBox";
        houseNumberTextBox.PlaceholderText = "Номер дома";
        houseNumberTextBox.Size = new Size(100, 23);
        houseNumberTextBox.TabIndex = 6;
        // 
        // streetTextBox
        // 
        streetTextBox.Location = new Point(143, 104);
        streetTextBox.Name = "streetTextBox";
        streetTextBox.PlaceholderText = "Улица";
        streetTextBox.Size = new Size(100, 23);
        streetTextBox.TabIndex = 7;
        // 
        // cityTextBox
        // 
        cityTextBox.Location = new Point(143, 149);
        cityTextBox.Name = "cityTextBox";
        cityTextBox.PlaceholderText = "Город";
        cityTextBox.Size = new Size(100, 23);
        cityTextBox.TabIndex = 8;
        // 
        // regionTextBox
        // 
        regionTextBox.Location = new Point(143, 193);
        regionTextBox.Name = "regionTextBox";
        regionTextBox.PlaceholderText = "Регион";
        regionTextBox.Size = new Size(100, 23);
        regionTextBox.TabIndex = 9;
        // 
        // countryTextBox
        // 
        countryTextBox.Location = new Point(143, 242);
        countryTextBox.Name = "countryTextBox";
        countryTextBox.PlaceholderText = "Страна";
        countryTextBox.Size = new Size(100, 23);
        countryTextBox.TabIndex = 10;
        // 
        // loginTextBox
        // 
        loginTextBox.Location = new Point(18, 321);
        loginTextBox.Name = "loginTextBox";
        loginTextBox.PlaceholderText = "Логин";
        loginTextBox.Size = new Size(100, 23);
        loginTextBox.TabIndex = 11;
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new Point(143, 321);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PlaceholderText = "Пароль";
        passwordTextBox.Size = new Size(100, 23);
        passwordTextBox.TabIndex = 12;
        // 
        // genderPanel
        // 
        genderPanel.Controls.Add(womanCheckBox);
        genderPanel.Controls.Add(manCheckBox);
        genderPanel.Location = new Point(18, 242);
        genderPanel.Name = "genderPanel";
        genderPanel.Size = new Size(113, 54);
        genderPanel.TabIndex = 13;
        // 
        // manCheckBox
        // 
        manCheckBox.AutoSize = true;
        manCheckBox.Location = new Point(3, 4);
        manCheckBox.Name = "manCheckBox";
        manCheckBox.Size = new Size(55, 19);
        manCheckBox.TabIndex = 0;
        manCheckBox.Text = "Муж.";
        manCheckBox.UseVisualStyleBackColor = true;
        // 
        // womanCheckBox
        // 
        womanCheckBox.AutoSize = true;
        womanCheckBox.Location = new Point(3, 29);
        womanCheckBox.Name = "womanCheckBox";
        womanCheckBox.Size = new Size(53, 19);
        womanCheckBox.TabIndex = 1;
        womanCheckBox.Text = "Жен.";
        womanCheckBox.UseVisualStyleBackColor = true;
        // 
        // RegistrationControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(genderPanel);
        Controls.Add(passwordTextBox);
        Controls.Add(loginTextBox);
        Controls.Add(countryTextBox);
        Controls.Add(regionTextBox);
        Controls.Add(cityTextBox);
        Controls.Add(streetTextBox);
        Controls.Add(houseNumberTextBox);
        Controls.Add(ageTextBox);
        Controls.Add(patronymicTextBox);
        Controls.Add(secondNameTextBox);
        Controls.Add(nameTextBox);
        Controls.Add(button2);
        Controls.Add(button1);
        Name = "RegistrationControl";
        Size = new Size(275, 500);
        ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
        genderPanel.ResumeLayout(false);
        genderPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
    #endregion

    private Button button1;
    private Button button2;
    private ErrorProvider errorProvider;
    private System.ComponentModel.IContainer components;
    private TextBox countryTextBox;
    private TextBox regionTextBox;
    private TextBox cityTextBox;
    private TextBox streetTextBox;
    private TextBox houseNumberTextBox;
    private TextBox ageTextBox;
    private TextBox patronymicTextBox;
    private TextBox secondNameTextBox;
    private TextBox nameTextBox;
    private TextBox passwordTextBox;
    private TextBox loginTextBox;
    private Panel genderPanel;
    private CheckBox womanCheckBox;
    private CheckBox manCheckBox;
}
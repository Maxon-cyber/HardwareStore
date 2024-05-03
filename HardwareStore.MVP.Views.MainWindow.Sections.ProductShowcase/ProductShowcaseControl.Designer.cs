namespace HardwareStore.MVP.Views.MainWindow.Sections.ProductShowcase;

public sealed partial class ProductShowcaseControl
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
        viewProductsTLP = new TableLayoutPanel();
        searchTextBox = new TextBox();
        searchButton = new Button();
        SuspendLayout();
        // 
        // viewProductsTLP
        // 
        viewProductsTLP.ColumnCount = 2;
        viewProductsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        viewProductsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        viewProductsTLP.Location = new Point(0, 168);
        viewProductsTLP.Name = "viewProductsTLP";
        viewProductsTLP.RowCount = 2;
        viewProductsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        viewProductsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        viewProductsTLP.Size = new Size(697, 211);
        viewProductsTLP.TabIndex = 0;
        // 
        // searchTextBox
        // 
        searchTextBox.Location = new Point(17, 38);
        searchTextBox.Name = "searchTextBox";
        searchTextBox.PlaceholderText = "Введите название товара";
        searchTextBox.Size = new Size(309, 23);
        searchTextBox.TabIndex = 1;
        // 
        // searchButton
        // 
        searchButton.Location = new Point(392, 38);
        searchButton.Name = "searchButton";
        searchButton.Size = new Size(75, 23);
        searchButton.TabIndex = 2;
        searchButton.Text = "Поиск";
        searchButton.UseVisualStyleBackColor = true;
        // 
        // ProductShowcaseControl
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(searchButton);
        Controls.Add(searchTextBox);
        Controls.Add(viewProductsTLP);
        Name = "ProductShowcaseControl";
        Size = new Size(697, 382);
        ResumeLayout(false);
        PerformLayout();
    }
    #endregion

    private TableLayoutPanel viewProductsTLP;
    private TextBox searchTextBox;
    private Button searchButton;
}
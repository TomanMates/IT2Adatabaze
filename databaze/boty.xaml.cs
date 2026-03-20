using System;
using System.Globalization;
using System.Windows;

namespace databaze
{
    public partial class ShoeWindow : Window
    {
        public bezeckeboty Shoe { get; private set; } = new();

        public ShoeWindow()
        {
            InitializeComponent();
        }

        public ShoeWindow(bezeckeboty existing) : this()
        {
            if (existing is not null)
            {
                Shoe = existing.Clone();
                txtModel.Text = Shoe.Model;
                txtBrand.Text = Shoe.Brand;
                txtYear.Text = Shoe.YearReleased.ToString();
                txtType.Text = Shoe.Type;
                txtSize.Text = Shoe.Size.ToString(CultureInfo.InvariantCulture);
                txtQuantity.Text = Shoe.Quantity.ToString();
                chkAvailable.IsChecked = Shoe.IsAvailable;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // validace
            var model = txtModel.Text.Trim();
            var brand = txtBrand.Text.Trim();
            if (string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Název modelu nesmí být prázdný.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(brand))
            {
                MessageBox.Show("Značka nesmí být prázdná.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtYear.Text.Trim(), out var year) || year < 1900 || year > DateTime.Now.Year + 1)
            {
                MessageBox.Show("Zadejte platný rok vydání.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtSize.Text.Trim().Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var size) || size <= 0)
            {
                MessageBox.Show("Zadejte platnou velikost (číslo větší než 0).", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), out var qty) || qty < 0)
            {
                MessageBox.Show("Počet kusů musí být číslo >= 0.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Shoe.Model = model;
            Shoe.Brand = brand;
            Shoe.YearReleased = year;
            Shoe.Type = txtType.Text.Trim();
            Shoe.Size = size;
            Shoe.Quantity = qty;
            Shoe.IsAvailable = chkAvailable.IsChecked == true;

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace databaze
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<bezeckeboty> Shoes { get; } = new();
        private ICollectionView ShoesView => CollectionViewSource.GetDefaultView(Shoes);

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            dgShoes.ItemsSource = Shoes;
            ShoesView.Filter = FilterShoes;
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            // populární modely jako ukázková data
            Shoes.Add(new bezeckeboty { ID = 1, Brand = "Nike", Model = "Pegasus 39", YearReleased = 2022, Type = "Road", Size = 42.5, Quantity = 5, IsAvailable = true });
            Shoes.Add(new bezeckeboty { ID = 2, Brand = "Adidas", Model = "Ultraboost 21", YearReleased = 2021, Type = "Road", Size = 43, Quantity = 3, IsAvailable = true });
            Shoes.Add(new bezeckeboty { ID = 3, Brand = "Brooks", Model = "Ghost 14", YearReleased = 2022, Type = "Road", Size = 42, Quantity = 2, IsAvailable = true });
            Shoes.Add(new bezeckeboty { ID = 4, Brand = "ASICS", Model = "Gel‑Nimbus 25", YearReleased = 2023, Type = "Road", Size = 44, Quantity = 1, IsAvailable = true });
            Shoes.Add(new bezeckeboty { ID = 5, Brand = "Hoka", Model = "Clifton 8", YearReleased = 2022, Type = "Road", Size = 43, Quantity = 4, IsAvailable = true });
        }

        private bool FilterShoes(object obj)
        {
            if (obj is not bezeckeboty s) return false;
            var q = txtSearch.Text?.Trim();
            if (string.IsNullOrEmpty(q)) return true;
            q = q.ToLowerInvariant();
            return (s.Model?.ToLowerInvariant().Contains(q) ?? false)
                || (s.Brand?.ToLowerInvariant().Contains(q) ?? false)
                || s.YearReleased.ToString().Contains(q)
                || (s.Type?.ToLowerInvariant().Contains(q) ?? false);
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ShoesView.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new ShoeWindow();
            if (win.ShowDialog() == true)
            {
                var newShoe = win.Shoe;
                newShoe.ID = GetNextId();
                Shoes.Add(newShoe);
            }
        }

        private int GetNextId()
        {
            if (Shoes.Count == 0) return 1;
            int max = 0;
            foreach (var s in Shoes) if (s.ID > max) max = s.ID;
            return max + 1;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgShoes.SelectedItem is not bezeckeboty sel)
            {
                MessageBox.Show("Vyberte botu pro úpravu.", "Upravit", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // pracujeme s kopií, při potvrzení zkopírujeme změny
            var copy = sel.Clone();
            var win = new ShoeWindow(copy);
            if (win.ShowDialog() == true)
            {
                var updated = win.Shoe;
                sel.Model = updated.Model;
                sel.Brand = updated.Brand;
                sel.YearReleased = updated.YearReleased;
                sel.Type = updated.Type;
                sel.Size = updated.Size;
                sel.Quantity = updated.Quantity;
                sel.IsAvailable = updated.IsAvailable;
                ShoesView.Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgShoes.SelectedItem is not bezeckeboty sel)
            {
                MessageBox.Show("Vyberte botu ke smazání.", "Smazat", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var res = MessageBox.Show($"Opravdu smazat '{sel.Brand} {sel.Model}'?", "Potvrdit smazání", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                Shoes.Remove(sel);
            }
        }
    }
}
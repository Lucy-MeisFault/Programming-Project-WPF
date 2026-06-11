using MahApps.Metro.Controls;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Diagnostics;

namespace Project
{
    /// <summary>
    /// Interakční logika pro Window3.xaml
    /// </summary>
    public partial class CreateWindow : MetroWindow
    {
        public Game FilterGame;
        private MainWindow mainWindow;
        public CreateWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;
            cmbGenre.ItemsSource = MainWindow.genreList;
            cmbGenre.DisplayMemberPath = "Name";
            cmbGenre.SelectedValuePath = "Id";

            cmbPlatform.ItemsSource = MainWindow.platformList;
            cmbPlatform.DisplayMemberPath = "Name";
            cmbPlatform.SelectedValuePath = "Id";

            cmbStatus.ItemsSource = MainWindow.statusList;
            cmbStatus.DisplayMemberPath = "Name";
            cmbStatus.SelectedValuePath = "Id";
        }
        public CreateWindow(MainWindow main, bool Filter)
        {
            InitializeComponent();
            btnSave.Click -= Save;
            btnSave.Click += CreateFilter;
            mainWindow = main;
            cmbGenre.ItemsSource = MainWindow.genreList;
            cmbGenre.DisplayMemberPath = "Name";
            cmbGenre.SelectedValuePath = "Id";

            cmbPlatform.ItemsSource = MainWindow.platformList;
            cmbPlatform.DisplayMemberPath = "Name";
            cmbPlatform.SelectedValuePath = "Id";

            cmbStatus.ItemsSource = MainWindow.statusList;
            cmbStatus.DisplayMemberPath = "Name";
            cmbStatus.SelectedValuePath = "Id";
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
            mainWindow.Show();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            int rating;
            int.TryParse(txtRating.Text, out rating);

            MainWindow.gameList.Add(
                new Game(
                    MainWindow.gameList.Count + 1,
                    txtTitle.Text,
                    ((Genre)cmbGenre.SelectedItem).Id,
                    ((Platform)cmbPlatform.SelectedItem).Id,
                    ((Status)cmbStatus.SelectedItem).Id,
                    rating,
                    0,
                    txtNotes.Text,
                    DateTime.Now,
                    false
                )
            );

            Close();
            mainWindow.Library(sender, e);
        }
        private void CreateFilter(object sender, RoutedEventArgs e)
        {
            int rating;
            int.TryParse(txtRating.Text, out rating);
            FilterGame = new Game(
                0,
                txtTitle.Text ?? "",
                (cmbGenre.SelectedItem as Genre)?.Id ?? 0,
                (cmbPlatform.SelectedItem as Platform)?.Id ?? 0,
                (cmbStatus.SelectedItem as Status)?.Id ?? 0,
                rating,
                0,
                txtNotes.Text ?? "",
                DateTime.Now, false);
            DialogResult = true;
        }

        private void AddGame(object sender, RoutedEventArgs e) => mainWindow.AddGame(sender, e);
        private void Library(object sender, RoutedEventArgs e) => mainWindow.Library(sender, e);
        private void Stats(object sender, RoutedEventArgs e) => mainWindow.Stats(sender, e);
        private void Search(object sender, RoutedEventArgs e) => mainWindow.Search(sender, e);
        private void Settings(object sender, RoutedEventArgs e) => mainWindow.Settings(sender, e);
        private void Exit(object sender, RoutedEventArgs e) => mainWindow.Exit(sender, e);
        private void BtnOptions_Click(object sender, RoutedEventArgs e) => mainWindow.Options(sender, e);
    }
}

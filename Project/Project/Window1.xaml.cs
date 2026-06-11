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
    /// Interakční logika pro Window1.xaml
    /// </summary>
    public partial class LibraryWindow : MetroWindow 
    {
        private MainWindow mainWindow;
        public LibraryWindow(MainWindow main) 
        {
            InitializeComponent();
            mainWindow = main;
            dgGames.ItemsSource = MainWindow.gameList;
            btnLibrary.IsEnabled = false;
        }
        private void AddGame(object sender, RoutedEventArgs e) => mainWindow.AddGame(sender, e);
        private void Library(object sender, RoutedEventArgs e) => mainWindow.Library(sender, e);
        private void Stats(object sender, RoutedEventArgs e) => mainWindow.Stats(sender, e);
        private void Search(object sender, RoutedEventArgs e) => mainWindow.Search(sender, e);
        private void Settings(object sender, RoutedEventArgs e) => mainWindow.Settings(sender, e);
        private void Exit(object sender, RoutedEventArgs e) => mainWindow.Exit(sender, e);
        private void Options(object sender, RoutedEventArgs e) => mainWindow.Options(sender, e);
        public LibraryWindow(MainWindow main, Game g)
        {
            InitializeComponent();
            mainWindow = main;

            var searchedGames = MainWindow.gameList.Where(gg =>
                (string.IsNullOrEmpty(g.Title) || gg.Title.ToLower().Contains(g.Title.ToLower())) &&
                (g.IdGenre == 0 || gg.IdGenre == g.IdGenre) &&
                (g.IdPlatform == 0 || gg.IdPlatform == g.IdPlatform) &&
                (g.IdStatus == 0 || gg.IdStatus == g.IdStatus) &&
                (g.Rating == 0 || gg.Rating == g.Rating)
            ).ToList();

            dgGames.ItemsSource = searchedGames;
            btnLibrary.IsEnabled = false;
        }
    }
}

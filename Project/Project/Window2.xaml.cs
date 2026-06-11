using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interakční logika pro Window2.xaml
    /// </summary>
    public partial class StatisticsWindow : MetroWindow
    {
        private MainWindow mainWindow;
        public StatisticsWindow(MainWindow main)
        {
            InitializeComponent();
            mainWindow = main;

            LoadHoursChart(MainWindow.gameList);
            LoadStatusChart();
            LoadLists();
        }
        private void AddGame(object sender, RoutedEventArgs e) => mainWindow.AddGame(sender, e);
        private void Library(object sender, RoutedEventArgs e) => mainWindow.Library(sender, e);
        private void Stats(object sender, RoutedEventArgs e) => mainWindow.Stats(sender, e);
        private void Search(object sender, RoutedEventArgs e) => mainWindow.Search(sender, e);
        private void Settings(object sender, RoutedEventArgs e) => mainWindow.Settings(sender, e);
        private void Exit(object sender, RoutedEventArgs e) => mainWindow.Exit(sender, e);
        private void Options(object sender, RoutedEventArgs e) => mainWindow.Options(sender, e);

        private void LoadHoursChart(List<Game> games)
        {

            var top4 = games
                .OrderByDescending(g => g.HoursPlayed)
                .Take(8)
                .ToList();

            double otherHours = games
                .OrderByDescending(g => g.HoursPlayed)
                .Skip(8)
                .Sum(g => g.HoursPlayed);

            double total = games.Sum(g => g.HoursPlayed);

            var seriesCollection = new SeriesCollection();

            foreach (var game in top4)
            {
                seriesCollection.Add(new PieSeries
                {
                    Title = game.Title, 
                    Values = new ChartValues<double> { game.HoursPlayed },
                    DataLabels = true,
                    LabelPoint = p => $"{p.Participation:P0}"
                });
            }

            if (otherHours > 0)
            {
                seriesCollection.Add(new PieSeries
                {
                    Title = "Other",
                    Values = new ChartValues<double> { otherHours },
                    DataLabels = true,
                    LabelPoint = p => $"{p.Participation:P0}"
                });
            }

            hoursChart.Series = seriesCollection;
            hoursChart.InnerRadius = 60; 
        }
        private void LoadStatusChart()
        {
            statusChart.LegendLocation = LegendLocation.None;
            var statusNames = new Dictionary<int, string>
            {
        { 1, "Want to play" },
        { 2, "Playing" },
        { 3, "Completed" },
        { 4, "Dropped" }
            };

            var counts = statusNames.Keys.ToDictionary(
                k => k,
                k => MainWindow.gameList.Count(g => g.IdStatus == k)
            );

            statusChart.Series = new SeriesCollection
    {
        new ColumnSeries
        {
            Title = "Games",
            Values = new ChartValues<int>(counts.Values),
            DataLabels = true,
            Fill = new SolidColorBrush(Color.FromRgb(41, 98, 163))
        }
    };

            statusChart.AxisX = new AxesCollection
    {
        new Axis
        {
            Labels = counts.Keys.Select(k => statusNames[k]).ToList(),
            Foreground = Brushes.White
        }
    };

            statusChart.AxisY = new AxesCollection
    {
        new Axis
        {
            Foreground = Brushes.White,
            MinValue = 0
        }

    };

        }
        private void LoadLists()
        {
            lstOne.ItemsSource = MainWindow.gameList
                .OrderByDescending(g => g.HoursPlayed)
                .Take(5)
                .Select(g => g.Title + ", " + g.HoursPlayed + " hours");

            lstTwo.ItemsSource = MainWindow.gameList
                .OrderByDescending(g => g.Rating)
                .Take(5)
                .Select(g => g.Title + ", " + "rated: "  + g.Rating);

            lstThree.ItemsSource = MainWindow.gameList
                .OrderByDescending(g => g.DateAdded)
                .Take(5)
                .Select(g => g.Title + ", added: " + g.DateAdded.ToShortDateString());

            lstFour.ItemsSource = MainWindow.genreList
                .Select(genre => new ////////////////////fix
                {
                    Name = genre.Name,
                    Count = MainWindow.gameList.Count(g => g.IdGenre == genre.Id)
                })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .Select(g => $"{g.Name} {g.Count}");
        }
    }
}

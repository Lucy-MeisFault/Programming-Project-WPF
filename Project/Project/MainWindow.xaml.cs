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
using System.Threading.Tasks;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static List<Game> gameList = new List<Game>();
        public static List<Genre> genreList = new List<Genre>();
        public static List<Status> statusList = new List<Status>();
        public static List<Platform> platformList = new List<Platform>();
        private string connectionString = "server=localhost;port=3306;user id=root;password=;" + "database=gamedatabase;";
        public MainWindow()
        {

            InitializeComponent();
            DatabaseConnected();
            Databases();

        }


        private void DatabaseConnected()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }
        private void Databases()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM game";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Game g;
                    if (reader["rating"] != DBNull.Value)
                    {
                        g = new Game(
                            (int)reader["id"],
                            (string)reader["title"],
                            (int)reader["id_genre"],
                            (int)reader["id_platform"],
                            (int)reader["id_status"],
                            (int)reader["rating"],
                            (int)reader["hours_played"],
                            (string)reader["notes"],
                            (DateTime)reader["date_added"],
                            false
                        );
                    }
                    else
                    {
                        g = new Game(
                            (int)reader["id"],
                            (string)reader["title"],
                            (int)reader["id_genre"],
                            (int)reader["id_platform"],
                            (int)reader["id_status"],
                            0,
                            (int)reader["hours_played"],
                            (string)reader["notes"],
                            (DateTime)reader["date_added"],
                            false
                        );
                    }
                    gameList.Add(g);
                }
                reader.Close();

                cmd = new MySqlCommand("SELECT * FROM genre", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    genreList.Add(new Genre((int)reader["id"], (string)reader["name"]));
                reader.Close();

                cmd = new MySqlCommand("SELECT * FROM platform", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    platformList.Add(new Platform((int)reader["id"], (string)reader["name"]));
                reader.Close();

                cmd = new MySqlCommand("SELECT * FROM status", conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    statusList.Add(new Status((int)reader["id"], (string)reader["name"]));
                reader.Close();
            }
        }
        public void AddGame(object sender, RoutedEventArgs e)
        {
            CreateWindow createWindow = new CreateWindow(this);
            foreach (Window w in Application.Current.Windows)
            w.Hide();
            createWindow.Show();
        }

        public void Search(object sender, RoutedEventArgs e)
        {

            CreateWindow filterWindow = new CreateWindow(this, true);
            filterWindow.ShowDialog();
            if (filterWindow.DialogResult == true)
            {
                Game g = filterWindow.FilterGame;
                LibraryWindow searchWindowActually = new LibraryWindow(this, g);
                searchWindowActually.Show();
            }
            
        }

        public void Settings(object sender, RoutedEventArgs e)
        {

        }

        public void Stats(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
                w.Hide();
            StatisticsWindow statisticsWindow = new StatisticsWindow(this);
            statisticsWindow.Left = this.Left;
            statisticsWindow.Top = this.Top;
            statisticsWindow.Width = this.Width;
            statisticsWindow.Height = this.Height;
            statisticsWindow.Show();
        }

        public void Library(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
                w.Hide();
            LibraryWindow libraryWindow = new LibraryWindow(this);
            libraryWindow.Left = this.Left;
            libraryWindow.Top = this.Top;
            libraryWindow.Width = this.Width;
            libraryWindow.Height = this.Height;
            libraryWindow.Show();
        }

        public void Exit(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                new MySqlCommand("DELETE FROM game", conn).ExecuteNonQuery();

                foreach (Game game in gameList)
                {
                    if (!game.ToDelete)
                    {
                        string query =
                        @"INSERT INTO game (title, id_genre, id_platform, id_status, rating, hours_played, notes, date_added)
                  VALUES (@title, @genre, @platform, @status, @rating, @hours, @notes, @dateAdded)";


                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@title", game.Title);
                        cmd.Parameters.AddWithValue("@genre", game.IdGenre);
                        cmd.Parameters.AddWithValue("@platform", game.IdPlatform);
                        cmd.Parameters.AddWithValue("@status", game.IdStatus);
                        cmd.Parameters.AddWithValue("@rating", game.Rating);
                        cmd.Parameters.AddWithValue("@hours", game.HoursPlayed);
                        cmd.Parameters.AddWithValue("@notes", game.Notes);
                        cmd.Parameters.AddWithValue("@dateAdded", game.DateAdded);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Environment.Exit(0);
        }

        public void Options(object sender, RoutedEventArgs e)
        {

        }
    }
}
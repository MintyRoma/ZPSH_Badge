using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using System.IO;
using ZPSH_Badge.Models;

namespace ZPSH_Badge.Views
{
    /// <summary>
    /// Логика взаимодействия для UsersDataPage.xaml
    /// </summary>
    public partial class UsersDataPage : Page
    {
        private string connectionString = "Data Source=Badges.sqlite;Version=3;";
        private List<UserModel> users = new List<UserModel>();
        public UsersDataPage()
        {
            InitializeComponent();
            CheckDbAvailability();
            CheckDbTopology();
            ImportDbData();
            
        }

        private void ImportDbData()
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            string request = "SELECT * FROM users";
            SQLiteCommand coma = new SQLiteCommand(request, conn);
            SQLiteDataReader info = coma.ExecuteReader();
            if (info.HasRows)
            {
                while (info.Read())
                {
                    UserModel user = new UserModel();
                    user.ID = info.GetInt32(info.GetOrdinal("ID"));
                    user.Surname = info.GetString(info.GetOrdinal("Surname"));
                    user.Name = info.GetString(info.GetOrdinal("Name"));
                    //user.Image = info.GetString(info.GetOrdinal("Image"));
                    users.Add(user);
                }

                UsersGridView.AutoGenerateColumns = true;
                UsersGridView.ItemsSource = users;
            }
            
        }

        private void CheckDbTopology()
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            string request = "SELECT name FROM sqlite_schema WHERE type ='table' AND name NOT LIKE 'sqlite_%';";
            SQLiteCommand comand = new SQLiteCommand(request,conn);
            SQLiteDataReader info = comand.ExecuteReader();
            if (!info.HasRows)
            {
                request = "CREATE TABLE users(" +
                          "ID INTEGER PRIMARY KEY," +
                          "Name varchar(100)," +
                          "Surname varchar(100)," +
                          "Image varchar(100))";
                comand = new SQLiteCommand(request,conn);
                comand.ExecuteNonQuery();
            }
        }

        private void CheckDbAvailability()
        {
            if (!File.Exists("Badges.sqlite"))
            {
                SQLiteConnection.CreateFile("Badges.sqlite");
            }
        }
    }
}

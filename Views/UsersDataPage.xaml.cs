using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Data.SQLite;
using System.IO;
using System.Windows;
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
            users.Clear();
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            conn.Open();
            string request = "SELECT users.* ,styles.Name as StyleName FROM users Inner JOIN styles ON users.Style = styles.ID ;";
            SQLiteCommand coma = new SQLiteCommand(request, conn);
            SQLiteDataReader info = coma.ExecuteReader();
            if (info.HasRows)
            {
                while (info.Read())
                {
                    UserModel user = new UserModel();
                    user.ID = info.GetInt32(info.GetOrdinal("ID"));
                    user.Surname = info.GetValue(info.GetOrdinal("Surname")).GetType()== typeof(DBNull)
                        ?""
                        :info.GetString(info.GetOrdinal("Surname"));
                    
                    user.Name = info.GetValue(info.GetOrdinal("Name")).GetType()== typeof(DBNull)
                        ?""
                        :info.GetString(info.GetOrdinal("Name"));
                    
                    user.Image = info.GetValue(info.GetOrdinal("Image")).GetType()== typeof(DBNull)
                        ?""
                        :info.GetString(info.GetOrdinal("Image"));
                    
                    user.Style = info.GetValue(info.GetOrdinal("StyleName")).GetType() == typeof(DBNull)
                        ?""
                        :info.GetString(info.GetOrdinal("StyleName"));
                    
                    if (user.Image!="") user.HasImage = true;
                    users.Add(user);
                }

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
                          "Image varchar," +
                          "Style INTEGER)";
                comand = new SQLiteCommand(request,conn);
                comand.ExecuteNonQuery();

                request = "CREATE TABLE styles(" +
                          "ID INTEGER PRIMARY KEY," +
                          "Name varchar(100)," +
                          "Path varchar)";
                comand = new SQLiteCommand(request, conn);
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

        private void AddUserBtnClick(object sender, RoutedEventArgs e)
        {
            UserInfo ui = new UserInfo();
        }
    }
}

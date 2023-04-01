using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZPSH_Badge.Models;

namespace ZPSH_Badge.Views;

public partial class Userinformation : Page
{
    private UserModel user;
    private string connectionString = "Data Source=Badges.sqlite;Version=3;";
    private List<StyleModel> styles = new List<StyleModel>();

    public delegate void closedpage();

    public event closedpage ClosedPage;

    public Userinformation()
    {
        InitializeComponent();
        GetStyles();
        user = new UserModel();
        user.Style = new StyleModel();
    }

    private void GetStyles()
    {
        SQLiteConnection conn = new SQLiteConnection(connectionString);
        conn.Open();
        string request = "SELECT * FROM styles";
        SQLiteCommand coma = new SQLiteCommand(request, conn);
        SQLiteDataReader reader = coma.ExecuteReader();
        while (reader.Read())
        {
            StyleModel model = new StyleModel();
            model.ID = reader.GetValue(reader.GetOrdinal("ID")).GetType()== typeof(DBNull)
                ?-1
                :reader.GetInt32(reader.GetOrdinal("ID"));
            model.Stylename = reader.GetValue(reader.GetOrdinal("Stylename")).GetType()== typeof(DBNull)
                ?""
                :reader.GetString(reader.GetOrdinal("Stylename"));
            model.Path = reader.GetValue(reader.GetOrdinal("Path")).GetType()== typeof(DBNull)
                ?""
                :reader.GetString(reader.GetOrdinal("Path"));
        }

        if (styles.Count != 0)
        {
            StyleSelector.ItemsSource = styles;
            StyleSelector.IsEditable = true;
        }
        else
        {
            StyleSelector.Items.Add("Нет ни одного стиля");
            StyleSelector.IsEditable = false;
            StyleSelector.SelectedIndex = 0;
        }
    }

    public Userinformation(UserModel user)
    {
        this.user = user;
        InitializeComponent();
        GetStyles();
        LoadUser();
    }

    private void LoadUser()
    {
        NameBox.Text = user.Name;
        SurnameBox.Text = user.Surname;
        if (user.Image != "")
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(user.Image, UriKind.Absolute);
            img.EndInit();
            UserPhoto.Source = img;
        }
        StyleSelector.SelectedValue = user.Style;
    }

    private void ClosePage(object sender, RoutedEventArgs e)
    {
        ClosedPage?.Invoke();
        NavigationService.GoBack();
    }

    private void SaveUser(object sender, RoutedEventArgs e)
    {
        user.Name = NameBox.Text;
        user.Surname = SurnameBox.Text;
        if (StyleSelector.IsEditable) user.Style = styles[StyleSelector.SelectedIndex];
        
        if (user.ID == -1) CreateNewUser();
        else
        {
            ModifyUser();
        }
        ClosedPage?.Invoke();
        NavigationService.GoBack();
    }

    private void ModifyUser()
    {
        SQLiteConnection conn = new SQLiteConnection(connectionString);
        conn.Open();
        string request = $"UPDATE users SET Name=\"{user.Name}\", Surname=\"{user.Surname}\", Image = \"{user.Image}\", Style={(user.Style.ID==0? "NULL" :user.Style.ID)} WHERE ID = {user.ID}";
        SQLiteCommand coma = new SQLiteCommand(request, conn);
        coma.ExecuteNonQuery();
    }

    private void CreateNewUser()
    {
        SQLiteConnection conn = new SQLiteConnection(connectionString);
        conn.Open();
        string request = $"INSERT INTO users(Name,Surname,Image,Style) VALUES (\"{user.Name}\",\"{user.Surname}\",\"{user.Image}\",{(user.Style.ID==0? "NULL" : user.Style.ID)} )";
        SQLiteCommand coma = new SQLiteCommand(request, conn);
        coma.ExecuteNonQuery();
    }

    private void DeleteUserBtnClick(object sender, RoutedEventArgs e)
    {
        SQLiteConnection conn = new SQLiteConnection(connectionString);
        conn.Open();
        string request = $"DELETE FROM users WHERE  ID={user.ID}";
        SQLiteCommand coma = new SQLiteCommand(request, conn);
        coma.ExecuteNonQuery();
        conn.Close();
        ClosedPage?.Invoke();
        NavigationService.GoBack();
    }
}
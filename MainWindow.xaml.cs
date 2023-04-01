using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZPSH_Badge.Models;
using ZPSH_Badge.Views;

namespace ZPSH_Badge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppStatus.StatusChangedEvent += ChangeMainPage;
            AppStatus.Status = GlobalMenu.UsersView;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppStatus.Status = GlobalMenu.UsersView;
        }

        private void ChangeMainPage()
        {
            MainFrame.Content = null;
            switch (AppStatus.Status)
            {
                case GlobalMenu.UsersView:
                    MainFrame.Content = new UsersDataPage();
                    break;
            }
        }
    }
}
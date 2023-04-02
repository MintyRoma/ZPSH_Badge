using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ZPSH_Badge.Views.Controls;

public partial class Badge : UserControl
{
    public Badge(string descr, BitmapImage image)
    {
        InitializeComponent();
        ImageView.Source = image;
        DescriptionBlock.Text = descr;
    }
}
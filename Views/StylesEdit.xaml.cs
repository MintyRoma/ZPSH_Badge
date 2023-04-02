using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows.Documents;
using ZPSH_Badge.Models;

namespace ZPSH_Badge.Views;

public partial class StylesEdit : Page
{
    private List<StyleModel> styles = new List<StyleModel>();
    
    public StylesEdit()
    {
        InitializeComponent();
        ImportStyles();
    }
    
    

    private void ImportStyles()
    {
        SQLiteConnection conn = new SQLiteConnection(DatabaseLinkModel.ConnectionString);
        conn.Open();
        string request = "SELECT * FROM styles";
        SQLiteCommand coma = new SQLiteCommand(request, conn);
        SQLiteDataReader info = coma.ExecuteReader();

        while (info.Read())
        {
            StyleModel style = new StyleModel();
            style.ID = info.GetInt32(info.GetOrdinal("ID"));
            style.Stylename = info.IsDBNull(info.GetOrdinal("Name")) ? "" : info.GetString(info.GetOrdinal("Name"));
            style.Stylename = info.IsDBNull(info.GetOrdinal("Path")) ? "" : info.GetString(info.GetOrdinal("Path"));
        }
        
        
    }
}
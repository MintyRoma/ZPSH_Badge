namespace ZPSH_Badge.Models;

public class UserModel
{
    public int ID { get; set; } = -1;
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";

    public string Image { get; set; } = "";
    public bool HasImage { get; set; } = false;
    
    public StyleModel Style { get; set; }
}
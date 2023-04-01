namespace ZPSH_Badge.Models;

public class UserModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public string Image { get; set; }
    public bool HasImage { get; set; }
    public string Style { get; set; }
}
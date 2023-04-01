namespace ZPSH_Badge.Models;

public class StyleModel
{
    public int ID { get; set; } = -1;
    public string Stylename { get; set; } = "";
    public string Path { get; set; } = "";

    public override string ToString()
    {
        return $"{ID}.{Stylename}";
    }
}
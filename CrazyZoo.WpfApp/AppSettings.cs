using System.Text.Json;

namespace CrazyZoo.WpfApp;

public class AppSettings
{
    public string Storage { get; set; } = "File";

    public string ConnectionString { get; set; } =
        @"Server=(localdb)\MSSQLLocalDB;Database=CrazyZooDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

    public static AppSettings Load()
    {
        string path = System.IO.Path.Combine(AppContext.BaseDirectory, "appsettings.json");

        if (!System.IO.File.Exists(path))
        {
            return new AppSettings();
        }

        string json = System.IO.File.ReadAllText(path);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }
}




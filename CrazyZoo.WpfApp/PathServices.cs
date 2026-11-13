using Microsoft.Win32;

namespace CrazyZoo.WpfApp;

public interface IPathService
{
    string? GetSavePath();

    string? GetLoadPath();
}

public class FileDialogPathService : IPathService
{
    public string? GetSavePath()
    {
        SaveFileDialog dialog = new()
        {
            Title = "Save world",
            Filter = "JSON file (*.json)|*.json|XML file (*.xml)|*.xml",
            FileName = "crazyzoo-world"
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }

    public string? GetLoadPath()
    {
        OpenFileDialog dialog = new()
        {
            Title = "Load world",
            Filter = "World files (*.json;*.xml)|*.json;*.xml|All files (*.*)|*.*"
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}

public class DatabasePathService : IPathService
{
    public string? GetSavePath() => "LocalDB";

    public string? GetLoadPath() => "LocalDB";
}
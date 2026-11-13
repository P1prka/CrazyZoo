using System.Windows;
using CrazyZoo.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CrazyZoo.WpfApp;

public partial class App : Application
{
    private ServiceProvider? provider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ConfigureServices();

        MainWindow = provider!.GetRequiredService<MainWindow>();
        MainWindow.Show();
    }

    private void ConfigureServices()
    {
        AppSettings settings = AppSettings.Load();

        ServiceCollection services = new();
        services.AddSingleton(settings);

        if (settings.Storage == "LocalDb")
        {
            services.AddSingleton<IWorldStorage>(
                _ => new SqlWorldStorage(settings.ConnectionString));
            services.AddSingleton<IPathService, DatabasePathService>();
        }
        else
        {
            services.AddSingleton<JsonWorldStorage>();
            services.AddSingleton<XmlWorldStorage>();
            services.AddSingleton<IWorldStorage, FileWorldStorage>();
            services.AddSingleton<IPathService, FileDialogPathService>();
        }

        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>();

        provider = services.BuildServiceProvider();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        provider?.Dispose();
        base.OnExit(e);
    }
}







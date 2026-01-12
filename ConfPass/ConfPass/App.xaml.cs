using Uno.Resizetizer;
using ConfPass.ViewModels;

namespace ConfPass;

public partial class App : Application
{
    public IHost? Host { get; private set; }

    public App()
    {
        this.InitializeComponent();
    }

    protected Window? MainWindow { get; private set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var appBuilder = this.CreateBuilder(args)
            .Configure(host =>
            {
                host.ConfigureServices((context, services) =>
                {
                    services.AddTransient<MainViewModel>();
                });
            });

        Host = appBuilder.Build();
        MainWindow = new Window();

#if DEBUG
        MainWindow.UseStudio();
#endif

        if (MainWindow.Content is not Frame rootFrame)
        {
            rootFrame = new Frame();
            MainWindow.Content = rootFrame;
            rootFrame.NavigationFailed += OnNavigationFailed;
        }

        if (rootFrame.Content == null)
        {
            rootFrame.Navigate(typeof(MainPage), args.Arguments);
        }

        MainWindow.SetWindowIcon();
        MainWindow.Activate();
    }

    void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
    {
        throw new System.InvalidOperationException($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
    }
}

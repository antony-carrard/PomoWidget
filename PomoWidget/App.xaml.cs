using System;
using System.Windows;

namespace PomoWidget;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly TimeManager _timeManager;
    private readonly NavigationStore _navigationStore;

    public App()
    {
        _timeManager = new TimeManager(
            new TimeSpan(0, 25, 0),
            new TimeSpan(0, 5, 0),
            new TimeSpan(0, 15, 0),
            4);   // TODO : maybe apply the parameters in the OnStartup method, to get the default parameters from the settings view.
			_navigationStore = new NavigationStore();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _navigationStore.CurrentViewModel = CreateHomeViewModel();

        MainWindow = new MainWindow()
        {
            DataContext = new MainViewModel(_navigationStore)
        };
        MainWindow.Show();

        base.OnStartup(e);
    }

    private SettingsViewModel CreateSettingsViewModel()
    {
        return new SettingsViewModel(_timeManager, new NavigationService(_navigationStore, CreateHomeViewModel));
    }

    private HomeViewModel CreateHomeViewModel()
    {
        return new HomeViewModel(_timeManager, new NavigationService(_navigationStore, CreateSettingsViewModel));
    }
}

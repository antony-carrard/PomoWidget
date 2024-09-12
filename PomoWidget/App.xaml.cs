using System;
using System.Windows;
using PomoWidget.Properties;

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
			Settings.Default.FocusTime,
			Settings.Default.ShortBreakTime,
			Settings.Default.LongBreakTime,
			Settings.Default.RoundsNumber);
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

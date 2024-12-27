using System.Windows.Input;

namespace PomoWidget;

public class HomeViewModel : ViewModelBase
{
	public TimeManager TimeManager { get; }
	public ICommand FocusCommand { get; }
	public ICommand ShortBreakCommand { get; }
	public ICommand LongBreakCommand { get; }
	public ICommand StartStopCommand { get; }
	public ICommand ResetCommand { get; }
	public ICommand SkipCommand { get; }
	public ICommand NavigateCommand { get; }

	public HomeViewModel(TimeManager timeManager, NavigationService navigationService)
	{
		TimeManager = timeManager;
		FocusCommand = new RelayCommand(() => TimeManager.TimeState = TimeState.Focus);
		ShortBreakCommand = new RelayCommand(() => TimeManager.TimeState = TimeState.ShortBreak);
		LongBreakCommand = new RelayCommand(() => TimeManager.TimeState = TimeState.LongBreak);
		ResetCommand = new RelayCommand(() => TimeManager.Reset());
		SkipCommand = new RelayCommand(() => TimeManager.NextState(false));
		StartStopCommand = new RelayCommand(() =>
		{
			if (TimeManager.IsTimeRunning)
				TimeManager.Stop();
			else
				TimeManager.Start();
		});
		NavigateCommand = new NavigateCommand(navigationService);

		//TimeManager.StateChange += ModelStateChange;
	}

	//public void ModelStateChange(object sender, EventArgs e)
	//{
	//	ActualizeButtons();
	//}
}

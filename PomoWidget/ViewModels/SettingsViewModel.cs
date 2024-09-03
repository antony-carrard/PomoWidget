using System;
using System.Windows.Input;

namespace PomoWidget;

public class SettingsViewModel : ViewModelBase
{
	public TimeManager TimeManager { get; }

	public int FocusTime
	{
		get => _focusTime;
		set
		{
			if (_focusTime != value)
			{
				_focusTime = value;
				TimeManager.Focus = new TimeSpan(0, value, 0);
				OnPropertyChanged(nameof(FocusTime));
			}
		}
	}
	private int _focusTime;

	public int ShortBreakTime
	{
		get => _shortBreakTime;
		set
		{
			if (_shortBreakTime != value)
			{
				_shortBreakTime = value;
				TimeManager.ShortBreak = new TimeSpan(0, value, 0);
				OnPropertyChanged(nameof(ShortBreakTime));
			}
		}
	}
	private int _shortBreakTime;

	public int LongBreakTime
	{
		get => _longBreakTime;
		set
		{
			if (_longBreakTime != value)
			{
				_longBreakTime = value;
				TimeManager.LongBreak = new TimeSpan(0, value, 0);
				OnPropertyChanged(nameof(LongBreakTime));
			}
		}
	}
	private int _longBreakTime;

	public int TotalRounds
	{
		get => _totalRounds;
		set
		{
			if (_totalRounds != value)
			{
				_totalRounds = value;
				TimeManager.TotalRounds = value;
				OnPropertyChanged(nameof(TotalRounds));
			}
		}
	}
	private int _totalRounds;

	public ICommand NavigateCommand { get; }
	public SettingsViewModel(TimeManager timeManager, NavigationService navigationService)
        {
		TimeManager = timeManager;
		NavigateCommand = new NavigateCommand(navigationService);

		FocusTime = timeManager.Focus.Minutes;
		ShortBreakTime = timeManager.ShortBreak.Minutes;
		LongBreakTime = timeManager.LongBreak.Minutes;
		TotalRounds = timeManager.TotalRounds;
	}

	~SettingsViewModel()
	{

	}
}

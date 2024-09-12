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
				TimeManager.Focus = TimeSpan.FromMinutes(value);
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
				TimeManager.ShortBreak = TimeSpan.FromMinutes(value);
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
				TimeManager.LongBreak = TimeSpan.FromMinutes(value);
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
	public ICommand ResetDefaultCommand { get; }
	public SettingsViewModel(TimeManager timeManager, NavigationService navigationService)
	{
		TimeManager = timeManager;
		NavigateCommand = new NavigateCommand(navigationService);
		ResetDefaultCommand = new RelayCommand(() => ResetDefault());

		FocusTime = (int)timeManager.Focus.TotalMinutes;
		ShortBreakTime = (int)timeManager.ShortBreak.TotalMinutes;
		LongBreakTime = (int)timeManager.LongBreak.TotalMinutes;
		TotalRounds = timeManager.TotalRounds;
	}

	private void ResetDefault()
	{
		FocusTime = (int)Constants.DefaultFocusTime.TotalMinutes;
		ShortBreakTime = (int)Constants.DefaultShortBreakTime.TotalMinutes;
		LongBreakTime = (int)Constants.DefaultLongBreakTime.TotalMinutes;
		TotalRounds = Constants.DefaultRoundsNumber;
	}

	~SettingsViewModel()
	{
		// TODO implémenter IDisposable...
	}
}

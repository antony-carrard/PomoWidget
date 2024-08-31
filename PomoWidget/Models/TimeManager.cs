using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace PomoWidget;

public enum TimeState
{
	Focus,
	ShortBreak,
	LongBreak
}
public class TimeManager : INotifyPropertyChanged
{
	#region Constructor

	public TimeManager(TimeSpan focusTime, TimeSpan shortBreakTime, TimeSpan longBreakTime, int totalRounds)
	{
		Focus = focusTime;
		ShortBreak = shortBreakTime;
		LongBreak = longBreakTime;
		TotalRounds = totalRounds;
		CurrentRound = 1;
		CurrentInterval = focusTime;
		StopWatch = new Stopwatch();
		StateChange += OnStateChange;
		Timer = new DispatcherTimer
		{
			Interval = TimeSpan.FromSeconds(1)
		};
		Timer.Tick += Timer_Tick;
	}

	#endregion Constructor

	#region Properties

	public TimeSpan Focus
	{
		get => _focusBreak;
		set
		{
			if (value != _focusBreak)
			{
				if (TimeState == TimeState.Focus)
				{
					Reset();
					CurrentInterval = value;
				}
				_focusBreak = value;
			}
		}
	}
	private TimeSpan _focusBreak;

	public TimeSpan ShortBreak
	{
		get => _shortBreak;
		set
		{
			if (value != _shortBreak)
			{
				if (TimeState == TimeState.ShortBreak)
				{
					Reset();
					CurrentInterval = value;
				}
				_shortBreak = value;
			}
		}
	}
	private TimeSpan _shortBreak;

	public TimeSpan LongBreak
	{
		get => _longBreak;
		set
		{
			if (value != _longBreak)
			{
				if (TimeState == TimeState.LongBreak)
				{
					Reset();
					CurrentInterval = value;
				}
				_longBreak = value;
			}
		}
	}
	private TimeSpan _longBreak;

	public int TotalRounds { get; set; }

	public int CurrentRound { get; set; }

	public TimeSpan RemainingTime => CurrentInterval - StopWatch?.Elapsed ?? TimeSpan.Zero;

	public bool Complete => RemainingTime <= TimeSpan.Zero;
	public bool IsTimeRunning => StopWatch?.IsRunning ?? false;

	public Stopwatch? StopWatch { get; }
	public DispatcherTimer? Timer { get; }

	public TimeSpan CurrentInterval { get; set; }

	public TimeState TimeState
	{
		get => _timeState;
		set
		{
			switch (value)
			{
				case TimeState.Focus:
					CurrentInterval = Focus;
					break;

				case TimeState.ShortBreak:
					if (TimeState == TimeState.Focus)
						CurrentRound++;
					CurrentInterval = ShortBreak;
					break;

				case TimeState.LongBreak:
					CurrentRound = 1;
					CurrentInterval = LongBreak;
					break;
			}
			_timeState = value;
			OnPropertyChanged(nameof(TimeState));
			Reset();
		}
	}
	private TimeState _timeState = TimeState.Focus;

	public event EventHandler StateChange;
	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion

	#region Methods

	public void NextState()
	{
		switch (TimeState)
		{
			case TimeState.Focus:
				if (CurrentRound < TotalRounds)
					TimeState = TimeState.ShortBreak;
				else
					TimeState = TimeState.LongBreak;
				break;

			case TimeState.ShortBreak or TimeState.LongBreak:
				TimeState = TimeState.Focus;
				break;
		}
	}

	public void Start()
	{
		Timer?.Start();
		StopWatch?.Start();
		OnPropertyChanged(nameof(IsTimeRunning));
	}
	public void Stop()
	{
		Timer?.Stop();
		StopWatch?.Stop();
		OnPropertyChanged(nameof(IsTimeRunning));
	}

	public void Reset()
	{
		Timer?.Stop();
		StopWatch?.Reset();
		OnPropertyChanged(nameof(IsTimeRunning));
		OnPropertyChanged(nameof(RemainingTime));
	}

	#endregion

	#region Events

	protected virtual void OnStateChange(object? sender, EventArgs e)
	{
		StateChange?.Invoke(this, e);
	}

	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
	private void Timer_Tick(object? sender, EventArgs e)
	{
		if (Complete)
			NextState();
		OnPropertyChanged(nameof(RemainingTime));
		Start(); // TODO rajouter une option pour paramétrer le démarrage automatique
	}

	#endregion
}

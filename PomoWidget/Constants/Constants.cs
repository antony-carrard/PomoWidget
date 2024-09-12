using System;

namespace PomoWidget;

public static class Constants
{
	public static readonly TimeSpan DefaultFocusTime = new(0, 25, 0);
	public static readonly TimeSpan DefaultShortBreakTime = new(0, 5, 0);
	public static readonly TimeSpan DefaultLongBreakTime = new(0, 15, 0);
	public const int DefaultRoundsNumber = 4;
}

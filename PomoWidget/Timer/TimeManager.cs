using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace PomoWidget.Timer
{
    public enum TimeState
    {
        Pomodoro,
        ShortBreak,
        LongBreak
    }
    internal class TimeManager
    {
        private readonly TimeSpan pomodoro;
        private readonly TimeSpan shortBreak;
        private readonly TimeSpan longBreak;
        private readonly int rounds;
        private int currentRound;
        public Stopwatch StopWatch { get; private set; }
        public TimeSpan CurrentTimer { get; private set; }

        public TimeState TimeState { get; private set; }
        public event EventHandler StateChange;

        public TimeManager()
        {
            pomodoro = new(0, 25, 0);
            shortBreak = new(0, 5, 0);
            longBreak = new(0, 15, 0);
            StopWatch = new Stopwatch();
            rounds = 4;
            currentRound = 1;
            TimeState = TimeState.Pomodoro;
        }

        public void PomodoroPressed()
        {
            StopWatch.Reset();
            CurrentTimer = pomodoro;
            TimeState = TimeState.Pomodoro;
            OnStateChange(EventArgs.Empty);
        }

        public void ShortBreakPressed()
        {
            StopWatch.Reset();
            if (TimeState == TimeState.Pomodoro)
            {
                currentRound++;
            }
            CurrentTimer = shortBreak;
            TimeState = TimeState.ShortBreak;
            OnStateChange(EventArgs.Empty);
        }

        public void LongBreakPressed()
        {
            StopWatch.Reset();
            CurrentTimer = longBreak;
            TimeState = TimeState.LongBreak;
            currentRound = 1;
            OnStateChange(EventArgs.Empty);
        }

        public void ResetPressed()
        {
            StopWatch.Reset();
            OnStateChange(EventArgs.Empty);
        }

        public void SkipPressed()
        {
            StopWatch.Reset();
            RequestStateChange();
        }

        public void RequestStateChange()
        {
            switch (TimeState)
            {
                case TimeState.Pomodoro:
                    {
                        if (currentRound < rounds)
                        {
                            TimeState = TimeState.ShortBreak;
                            currentRound++;
                        }
                        else
                        {
                            TimeState = TimeState.LongBreak;
                            currentRound = 1;
                        }
                        break;
                    }
                case TimeState.ShortBreak or TimeState.LongBreak:
                    {
                        TimeState = TimeState.Pomodoro;
                        break;
                    }
            }
            OnStateChange(EventArgs.Empty); //No event data
        }

        protected virtual void OnStateChange(EventArgs e)
        {
            StateChange?.Invoke(this, e);
        }
    }
}

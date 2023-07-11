using Microsoft.Win32;
using PomoWidget.Timer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PomoWidget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        DispatcherTimer dt = new DispatcherTimer();
        TimeManager timeManager = new TimeManager();
        TimeSpan ts = TimeSpan.Zero;
        private string _CurrentTime = string.Empty;
        public string CurrentTime
        {
            get { return _CurrentTime; }
            set
            {
                _CurrentTime = value;
                OnPropertyChanged();
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            dt.Tick += new EventHandler(Dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            PomodoroToggleButton.IsChecked = true;
            ConvertTimeFormat();
            timeManager.StateChange += StateChange; // register with an event
        }

        void ConvertTimeFormat()
        {
            ts = timeManager.StopWatch.Elapsed;
            ts = timeManager.CurrentTimer - ts;
            CurrentTime = string.Format("{0:00}:{1:00}",
                ts.Minutes, ts.Seconds);
        }

        void Dt_Tick(object sender, EventArgs e)
        {
            if (timeManager.StopWatch.IsRunning)
            {
                ConvertTimeFormat();
                if (ts.TotalSeconds <= 0)
                {
                    timeManager.RequestStateChange();
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timeManager.ResetPressed();
        }

        private void StartToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            timeManager.StopWatch.Start();
            dt.Start();
            StartToggleButton.Content = "PAUSE";
        }

        private void StartToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (timeManager.StopWatch.IsRunning)
                timeManager.StopWatch.Stop();
            StartToggleButton.Content = "START";
        }

        private void PomodoroToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            timeManager.PomodoroPressed();
        }

        private void ShortBreakToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            timeManager.ShortBreakPressed();
        }

        private void LongBreakToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            timeManager.LongBreakPressed();
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            timeManager.SkipPressed();
        }

        // event handler
        public void StateChange(object sender, EventArgs e)
        {
            StartToggleButton.IsChecked = false;
            ConvertTimeFormat();
            switch(timeManager.TimeState)
            {
                case TimeState.Pomodoro:
                    {
                        PomodoroToggleButton.IsChecked = true;
                        break;
                    }
                case TimeState.ShortBreak:
                    {
                        ShortBreakToggleButton.IsChecked = true;
                        break;
                    }
                case TimeState.LongBreak:
                    {
                        LongBreakToggleButton.IsChecked = true;
                        break;
                    }
            }
        }
    }
}

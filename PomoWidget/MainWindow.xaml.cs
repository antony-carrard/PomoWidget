using System.Windows;

namespace PomoWidget;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
public partial class MainWindow : Window
{

    //DispatcherTimer dt = new DispatcherTimer();
    //TimeManager timeManager = new TimeManager(25, 5, 15, 4);
    //TimeSpan ts = TimeSpan.Zero;
    //private string _CurrentTime = string.Empty;
    //public string CurrentTime
    //{
    //    get { return _CurrentTime; }
    //    set
    //    {
    //        _CurrentTime = value;
    //        OnPropertyChanged();
    //    }
    //}
    //#region INotifyPropertyChanged Members

    //public event PropertyChangedEventHandler? PropertyChanged;

    ///// <summary>
    ///// Raises this object's PropertyChanged event.
    ///// </summary>
    ///// <param name="propertyName">The property that has a new value.</param>
    //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //{
    //    PropertyChangedEventHandler handler = this.PropertyChanged;
    //    if (handler != null)
    //    {
    //        var e = new PropertyChangedEventArgs(propertyName);
    //        handler(this, e);
    //    }
    //}
    //#endregion

    public MainWindow()
    {
        InitializeComponent();
        //DataContext = new MainViewModel();
        //dt.Tick += new EventHandler(Dt_Tick);
        //dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        ////FocusToggleButton.IsChecked = true;
        //ConvertTimeFormat();
        //timeManager.StateChange += StateChange; // register with an event
    }

    //void ConvertTimeFormat()
    //{
    //    ts = timeManager.StopWatch.Elapsed;
    //    ts = timeManager.CurrentInterval - ts;
    //    CurrentTime = string.Format("{0:00}:{1:00}",
    //        ts.Minutes, ts.Seconds);
    //}

    //void Dt_Tick(object sender, EventArgs e)
    //{
    //    if (timeManager.StopWatch.IsRunning)
    //    {
    //        ConvertTimeFormat();
    //        if (ts.TotalSeconds <= 0)
    //        {
    //            timeManager.RequestStateChange();
    //        }
    //    }
    //}

    //private void ResetButton_Click(object sender, RoutedEventArgs e)
    //{
    //    timeManager.ResetPressed();
    //}

    //private void StartToggleButton_Checked(object sender, RoutedEventArgs e)
    //{
    //    timeManager.StopWatch.Start();
    //    dt.Start();
    //    //StartToggleButton.Content = "PAUSE";
    //}

    //private void StartToggleButton_Unchecked(object sender, RoutedEventArgs e)
    //{
    //    if (timeManager.StopWatch.IsRunning)
    //        timeManager.StopWatch.Stop();
    //    //StartToggleButton.Content = "START";
    //}

    //private void FocusToggleButton_Checked(object sender, RoutedEventArgs e)
    //{
    //    timeManager.FocusPressed();
    //}

    //private void ShortBreakToggleButton_Checked(object sender, RoutedEventArgs e)
    //{
    //    timeManager.ShortBreakPressed();
    //}

    //private void LongBreakToggleButton_Checked(object sender, RoutedEventArgs e)
    //{
    //    timeManager.LongBreakPressed();
    //}

    //private void SkipButton_Click(object sender, RoutedEventArgs e)
    //{
    //    timeManager.SkipPressed();
    //}

    //// event handler
    //public void StateChange(object sender, EventArgs e)
    //{
    //    //StartToggleButton.IsChecked = false;
    //    ConvertTimeFormat();
    //    switch(timeManager.TimeState)
    //    {
    //        case TimeState.Focus:
    //            {
    //                //FocusToggleButton.IsChecked = true;
    //                break;
    //            }
    //        case TimeState.ShortBreak:
    //            {
    //                //ShortBreakToggleButton.IsChecked = true;
    //                break;
    //            }
    //        case TimeState.LongBreak:
    //            {
    //                //LongBreakToggleButton.IsChecked = true;
    //                break;
    //            }
    //    }
    //}

    //private void SettingsButton_Click(object sender, RoutedEventArgs e)
    //{
    //    Window window = new Window
    //    {
    //        Title = "My User Control Dialog",
    //        Content = new Settings(),
    //        Width = this.Width,
    //        Height = this.Height,
    //        Top = this.Top,
    //        Left = this.Left,
    //        Owner = this
    //    };

    //    this.Hide();
    //    window.ShowDialog();
    //}
}

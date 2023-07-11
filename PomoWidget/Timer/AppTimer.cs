using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PomoWidget.Timer
{
    public class AppTimer
    {
        public DispatcherTimer timer;
        public AppTimer()
        {
            timer = new();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            TextBlockTimer.Content = DateTime.Now.ToLongTimeString();
        }
    }
}

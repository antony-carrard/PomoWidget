using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PomoWidget.Resources
{
    /// <summary>
    /// Interaction logic for SliderSettings.xaml
    /// </summary>
    public partial class SliderSettings : UserControl
    {
        public SliderSettings()
        {
            InitializeComponent();
        }

		public static readonly DependencyProperty SelectedValueProperty =
		 DependencyProperty.Register("SelectedValue", typeof(int), typeof(SliderSettings), new
			PropertyMetadata(0, new PropertyChangedCallback(OnSelectedValueChanged)));

		public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
			typeof(RoutedPropertyChangedEventHandler<int>), typeof(SliderSettings));

		public int SelectedValue
		{
			get { return (int)GetValue(SelectedValueProperty); }
			set { SetValue(SelectedValueProperty, value); }
		}

		private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is SliderSettings)
			{
				SliderSettings SliderSettingsControl = d as SliderSettings;
				SliderSettingsControl.OnSelectedValueChanged(e);
			}
		}

		private void OnSelectedValueChanged(DependencyPropertyChangedEventArgs e)
		{
			SelectedValue = (int)e.NewValue;
			RaiseEvent(new RoutedPropertyChangedEventArgs<int>((int)e.OldValue, (int)e.NewValue, ValueChangedEvent));
		}

		public event RoutedPropertyChangedEventHandler<int> ValueChanged
		{
			add { AddHandler(ValueChangedEvent, value); }
			remove { RemoveHandler(ValueChangedEvent, value); }
		}

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(string), typeof(SliderSettings), new PropertyMetadata(""));

		public int Maximum
		{
			get { return (int)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(int), typeof(SliderSettings), new PropertyMetadata(0));

		public int Minimum
		{
			get { return (int)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(int), typeof(SliderSettings), new PropertyMetadata(0));

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedValue > Minimum)
            {
				SelectedValue--;
            }
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedValue < Maximum)
            {
				SelectedValue++;
            }
        }
	}
}

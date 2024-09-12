using System.Windows.Controls;

namespace PomoWidget;

/// <summary>
/// Interaction logic for SettingsView.xaml
/// </summary>
public partial class SettingsView : UserControl
{
	public SettingsView()
	{
		InitializeComponent();
		Loaded += SettingsView_Loaded;
	}

	private void SettingsView_Loaded(object sender, System.Windows.RoutedEventArgs e)
	{
		FocusSlider.Focus();
	}
}

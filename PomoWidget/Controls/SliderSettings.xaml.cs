using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PomoWidget;

/// <summary>
/// Interaction logic for SliderSettings.xaml
/// </summary>
public partial class SliderSettings : UserControl, INotifyPropertyChanged
{
	public SliderSettings()
	{
		InitializeComponent();
		UpdateButtonStates();
		GotFocus += SliderSettings_GotFocus;
	}

	public int SelectedValue
	{
		get => (int)GetValue(SelectedValueProperty);
		set
		{
			SetValue(SelectedValueProperty, value);
			UpdateButtonStates();
		}
	}

	public static readonly DependencyProperty SelectedValueProperty =
	 DependencyProperty.Register(nameof(SelectedValue), typeof(int), typeof(SliderSettings), new PropertyMetadata(0, OnSelectedValueChanged));

	private static void OnSelectedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var control = d as SliderSettings;
		control?.UpdateButtonStates();
	}

	public int Maximum
	{
		get => (int)GetValue(MaximumProperty);
		set
		{
			SetValue(MaximumProperty, value);
			UpdateButtonStates();
		}
	}

	public static readonly DependencyProperty MaximumProperty =
		DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(SliderSettings), new PropertyMetadata(0));

	public int Minimum
	{
		get => (int)GetValue(MinimumProperty);
		set
		{
			SetValue(MinimumProperty, value);
			UpdateButtonStates();
		}
	}

	public static readonly DependencyProperty MinimumProperty =
		DependencyProperty.Register(nameof(Minimum), typeof(int), typeof(SliderSettings), new PropertyMetadata(0));

	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public static readonly DependencyProperty TitleProperty =
		DependencyProperty.Register(nameof(Title), typeof(string), typeof(SliderSettings), new PropertyMetadata(""));

	public bool DisplayAsTimeSpan
	{
		get => (bool)GetValue(DisplayAsTimeSpanProperty);
		set => SetValue(DisplayAsTimeSpanProperty, value);
	}

	public static readonly DependencyProperty DisplayAsTimeSpanProperty =
		DependencyProperty.Register(nameof(DisplayAsTimeSpan), typeof(bool), typeof(SliderSettings), new PropertyMetadata(false));

	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged(string propertyName) =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	private void SliderSettings_GotFocus(object sender, RoutedEventArgs e) => MyMainGrid.Focus();

	public bool CanDecrease => SelectedValue > Minimum;
	public bool CanIncrease => SelectedValue < Maximum;

	private void DecreaseValue()
	{
		if (CanDecrease)
		{
			SelectedValue--;
			UpdateButtonStates();
		}
	}

	private void IncreaseValue()
	{
		if (CanIncrease)
		{
			SelectedValue++;
			UpdateButtonStates();
		}
	}

	private void Decrease_Click(object sender, RoutedEventArgs e) => DecreaseValue();

	private void Increase_Click(object sender, RoutedEventArgs e) => IncreaseValue();

	private void UpdateButtonStates()
	{
		OnPropertyChanged(nameof(CanDecrease));
		OnPropertyChanged(nameof(CanIncrease));
	}

	private void Grid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		if (e.Delta > 0)
			IncreaseValue();
		else
			DecreaseValue();

		// Mark the event as handled
		e.Handled = true;
		
		// Give focus to the current slider when the mouse wheel is activated
		MyMainGrid.Focus();
	}

	private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
	{
		switch (e.Key)
		{
			case Key.Up:
			case Key.Right:
				IncreaseValue();
				e.Handled = true;
				break;

			case Key.Down:
			case Key.Left:
				DecreaseValue();
				e.Handled = true;
				break;
		}
	}
}

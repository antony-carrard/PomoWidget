using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

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

	public bool CanDecrease => SelectedValue > Minimum;
	public bool CanIncrease => SelectedValue < Maximum;

	private void Decrease_Click(object sender, RoutedEventArgs e)
	{
		if (CanDecrease)
		{
			SelectedValue--;
			UpdateButtonStates();
		}
	}

	private void Increase_Click(object sender, RoutedEventArgs e)
	{
		if (CanIncrease)
		{
			SelectedValue++;
			UpdateButtonStates();
		}
	}

	private void UpdateButtonStates()
	{
		OnPropertyChanged(nameof(CanDecrease));
		OnPropertyChanged(nameof(CanIncrease));
	}
}

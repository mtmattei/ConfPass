using System.Windows.Input;
using Microsoft.UI.Xaml.Input;

namespace ConfPass.Controls;

public sealed partial class NeumorphicToggle : UserControl
{
    public static readonly DependencyProperty IsOnProperty =
        DependencyProperty.Register(
            nameof(IsOn),
            typeof(bool),
            typeof(NeumorphicToggle),
            new PropertyMetadata(true, OnIsOnChanged));

    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(
            nameof(Command),
            typeof(ICommand),
            typeof(NeumorphicToggle),
            new PropertyMetadata(null));

    public bool IsOn
    {
        get => (bool)GetValue(IsOnProperty);
        set => SetValue(IsOnProperty, value);
    }

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public NeumorphicToggle()
    {
        this.InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateVisualState(IsOn, useTransitions: false);
    }

    private static void OnIsOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NeumorphicToggle toggle && e.NewValue is bool newValue)
        {
            toggle.UpdateVisualState(newValue, useTransitions: true);
        }
    }

    private void OnToggleTapped(object sender, TappedRoutedEventArgs e)
    {
        if (Command?.CanExecute(null) == true)
        {
            Command.Execute(null);
        }
        else
        {
            IsOn = !IsOn;
        }
    }

    private void UpdateVisualState(bool isOn, bool useTransitions)
    {
        VisualStateManager.GoToState(this, isOn ? "On" : "Off", useTransitions);
    }
}

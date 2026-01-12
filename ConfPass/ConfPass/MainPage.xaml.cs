using ConfPass.ViewModels;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;

namespace ConfPass;

public sealed partial class MainPage : Page
{
    private Brush? _successBrush;
    private Brush? _mutedBrush;
    private Storyboard? _pulseStoryboard;
    private MainViewModel? _viewModel;

    public MainPage()
    {
        this.InitializeComponent();

        var host = ((App)Application.Current).Host;
        _viewModel = host?.Services.GetRequiredService<MainViewModel>();
        DataContext = _viewModel;

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        var resources = Application.Current.Resources;
        _successBrush = (Brush)resources["NeumorphicSuccessBrush"];
        _mutedBrush = (Brush)resources["NeumorphicTextMutedBrush"];

        if (Resources.TryGetValue("ActivePulseAnimation", out var resource) && resource is Storyboard storyboard)
        {
            _pulseStoryboard = storyboard;
            storyboard.Begin();
        }

        if (_viewModel != null)
        {
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            UpdateNetworkingIcon(_viewModel.IsNetworkingAvailable);
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _pulseStoryboard?.Stop();
        _pulseStoryboard = null;

        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }
    }

    private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.IsNetworkingAvailable) && _viewModel != null)
        {
            UpdateNetworkingIcon(_viewModel.IsNetworkingAvailable);
        }
    }

    private void UpdateNetworkingIcon(bool isOn)
    {
        NetworkingIcon.Fill = isOn ? _successBrush : _mutedBrush;
    }

    private void AccessBadge_PointerEntered(object sender, PointerRoutedEventArgs e)
        => VisualStateManager.GoToState(this, "PointerOver", true);

    private void AccessBadge_PointerExited(object sender, PointerRoutedEventArgs e)
        => VisualStateManager.GoToState(this, "Normal", true);

    private void AccessBadge_PointerPressed(object sender, PointerRoutedEventArgs e)
        => VisualStateManager.GoToState(this, "Pressed", true);

    private void AccessBadge_PointerReleased(object sender, PointerRoutedEventArgs e)
        => VisualStateManager.GoToState(this, "Normal", true);

    private void Avatar_PointerPressed(object sender, PointerRoutedEventArgs e)
        => VisualStateManager.GoToState(this, "AvatarPressed", true);

    private void Avatar_PointerReleased(object sender, PointerRoutedEventArgs e)
        => VisualStateManager.GoToState(this, "AvatarNormal", true);
}

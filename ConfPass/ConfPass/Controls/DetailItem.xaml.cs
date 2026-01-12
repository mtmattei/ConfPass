namespace ConfPass.Controls;

public sealed partial class DetailItem : UserControl
{
    public static readonly DependencyProperty GlyphProperty =
        DependencyProperty.Register(
            nameof(Glyph),
            typeof(string),
            typeof(DetailItem),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(
            nameof(Label),
            typeof(string),
            typeof(DetailItem),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(DetailItem),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty TextWrappingProperty =
        DependencyProperty.Register(
            nameof(TextWrapping),
            typeof(TextWrapping),
            typeof(DetailItem),
            new PropertyMetadata(TextWrapping.NoWrap));

    public static readonly DependencyProperty MaxValueWidthProperty =
        DependencyProperty.Register(
            nameof(MaxValueWidth),
            typeof(double),
            typeof(DetailItem),
            new PropertyMetadata(double.PositiveInfinity));

    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public TextWrapping TextWrapping
    {
        get => (TextWrapping)GetValue(TextWrappingProperty);
        set => SetValue(TextWrappingProperty, value);
    }

    public double MaxValueWidth
    {
        get => (double)GetValue(MaxValueWidthProperty);
        set => SetValue(MaxValueWidthProperty, value);
    }

    public DetailItem()
    {
        this.InitializeComponent();
    }
}

# ConfPass - Conference Badge App

A cross-platform conference attendee badge built with Uno Platform, featuring neumorphic (soft UI) design with interactive shadow effects.

## What This Proves

ConfPass demonstrates that Uno Platform can render sophisticated, design-forward UI patterns that go beyond standard Material or Fluent components. The neumorphic design - with its layered shadows, inset effects, and smooth animations - runs identically across Windows, WebAssembly, iOS, Android, macOS, and Linux from a single XAML codebase.

## Prerequisites

- .NET 10 SDK
- Visual Studio 2022 or VS Code with C# Dev Kit
- Uno Platform extension

## Getting Started

### Windows

1. Clone the repository:
   ```bash
   git clone https://github.com/AIPMMAgent/ConfPass.git
   cd ConfPass/ConfPass
   ```

2. Build and run the desktop target:
   ```bash
   dotnet build -f net10.0-desktop
   dotnet run -f net10.0-desktop
   ```

### macOS / Linux

1. Clone the repository:
   ```bash
   git clone https://github.com/AIPMMAgent/ConfPass.git
   cd ConfPass/ConfPass
   ```

2. Build and run:
   ```bash
   dotnet build -f net10.0-desktop
   dotnet run -f net10.0-desktop
   ```

### WebAssembly

1. Build for browser:
   ```bash
   dotnet build -f net10.0-browserwasm
   ```

2. Run with a local server or publish to a static host.

## Features

| Feature | Description |
|---------|-------------|
| Neumorphic Card | Main pass card with layered raised/inset shadows |
| Interactive Shadows | Press effects that swap raised shadows to inset |
| Networking Toggle | Custom toggle switch with neumorphic styling |
| Schedule View | Embedded schedule with color-coded sessions |
| QR Code | Stylized QR display for badge scanning |
| Social Links | Icon buttons for Twitter, LinkedIn, GitHub, Website |
| Pulse Animation | Active indicator with breathing animation |

## Technical Highlights

### Patterns

- **MVVM** with CommunityToolkit.Mvvm
- **Records** for immutable data models
- **ObservableProperty** for reactive bindings

### Uno Platform Features

**ShadowContainer (Uno Toolkit)**

The entire neumorphic effect is built using `utu:ShadowContainer` with custom shadow definitions:

```xml
<utu:ShadowContainer Shadows="{ThemeResource NeuroRaisedXl}"
                     Background="{ThemeResource NeumorphicBgBrush}">
    <!-- Content appears raised -->
</utu:ShadowContainer>

<utu:ShadowContainer Shadows="{ThemeResource NeuroInsetMd}"
                     Background="{ThemeResource NeumorphicBgBrush}">
    <!-- Content appears pressed in -->
</utu:ShadowContainer>
```

**Shadow Definitions**

Shadows are defined as theme resources for consistency:

```xml
<ShadowCollection x:Key="NeuroRaisedXl">
    <Shadow OffsetX="-8" OffsetY="-8" BlurRadius="16"
            Color="{ThemeResource NeumorphicLightShadowColor}" />
    <Shadow OffsetX="8" OffsetY="8" BlurRadius="16"
            Color="{ThemeResource NeumorphicDarkShadowColor}" />
</ShadowCollection>
```

**Interactive Press States**

Press effects are handled by swapping opacity between raised and inset containers:

```csharp
private void Avatar_PointerPressed(object sender, PointerRoutedEventArgs e)
{
    AvatarRaised.Opacity = 0;
    AvatarInset.Opacity = 1;
}

private void Avatar_PointerReleased(object sender, PointerRoutedEventArgs e)
{
    AvatarRaised.Opacity = 1;
    AvatarInset.Opacity = 0;
}
```

### Color Palette

The neumorphic palette uses a soft gray base with carefully tuned shadow colors:

| Resource | Value | Purpose |
|----------|-------|---------|
| NeumorphicBgBrush | #E0E5EC | Base surface |
| NeumorphicLightShadowColor | #FFFFFF | Upper-left highlight |
| NeumorphicDarkShadowColor | #A3B1C6 | Lower-right shadow |
| NeumorphicAccentBrush | #6C5CE7 | Accent elements |
| NeumorphicSuccessBrush | #00B894 | Active/success states |

## Project Structure

```
ConfPass/
├── App.xaml                 # Application resources
├── MainPage.xaml            # Main pass card UI
├── MainPage.xaml.cs         # Interaction handlers
├── Models/
│   ├── ConferencePass.cs    # Pass data record
│   ├── ScheduleItem.cs      # Schedule entry
│   ├── SocialLink.cs        # Social link data
│   └── Achievement.cs       # Badge achievement
├── ViewModels/
│   └── MainViewModel.cs     # MVVM view model
└── Styles/
    ├── NeumorphicStyles.xaml    # Shadow and style definitions
    └── ColorPaletteOverride.xaml # Color resources
```

## Accessibility Considerations

Neumorphic design presents accessibility challenges due to low contrast. This implementation addresses them by:

1. **Sufficient text contrast** - Text uses darker colors against the soft gray background
2. **Clear interactive feedback** - Press states provide obvious visual response
3. **Accent colors for emphasis** - Important elements use high-contrast accent colors

For production apps, consider providing an alternative high-contrast theme.

## Next Steps

- [Uno Toolkit ShadowContainer documentation](https://platform.uno/docs/articles/external/uno.toolkit.ui/doc/controls/ShadowContainer.html)
- [Custom styling in Uno Platform](https://platform.uno/docs/articles/external/uno.themes/doc/themes-overview.html)
- [CommunityToolkit.Mvvm documentation](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)

## AI Generation Note

This app was generated using AI-assisted development with Uno Platform and Claude. The initial scaffold was created from a natural language prompt describing a conference badge with neumorphic styling, then refined through iterative Hot Reload sessions.

**Scaffold prompt summary:**
> Create a conference attendee badge app with neumorphic/soft UI design. Include attendee info, social links, schedule, QR code, and networking toggle. Use Uno Toolkit ShadowContainer for the shadow effects.

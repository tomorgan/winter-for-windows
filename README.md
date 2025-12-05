# Winter for Windows

A delightful WPF application that brings festive Christmas cheer to your Windows desktop! Transform your screen into a winter wonderland with twinkling lights, gentle snowfall, a cheerful penguin, and a countdown to Christmas Day.

## Features

- **🎄 Fairy Lights**: Colorful, twinkling Christmas lights strung elegantly across the top of your screen with a gentle swaying motion
- **❄️ Snow**: Realistic snowfall that gently drifts down across your desktop and accumulates against the taskbar
- **🐧 Penguin**: An adorable animated penguin that walks along your taskbar, occasionally waving, sliding, and jumping
- **🎅 Christmas Countdown**: A festive floating widget showing the number of days until Christmas Day, with special messaging on December 25th
- **Persistent Settings**: Your choices are saved and automatically restored when you restart the app
- **System Tray Integration**: Runs quietly in the background with easy access from your system tray
- **Click-Through Overlays**: All effects use transparent, non-intrusive windows that don't interfere with your work

## Screenshots

<img width="1152" height="720" alt="Winter For Windows" src="https://github.com/user-attachments/assets/eaa96d6b-e185-49c5-9d15-3e3fed1a51f7" />


## Installation

### Option 1: Download Pre-built Executable (Recommended)

Download the latest release from the [Releases page](https://github.com/tomorgan/winter-for-windows/releases). Look for:
- `WinterForWindows_win-x64_v{version}.exe` - Single-file executable (no installation required)
- `WinterForWindows_win-x64_v{version}.msi` - Windows installer
- `WinterForWindows_win-x64_v{version}.zip` - Portable version

The single-file executable includes everything you need - no .NET installation required!

### Option 2: Build from Source

#### Prerequisites

- Windows 10 or later
- .NET 8.0 SDK for building

#### Building from Source

1. Clone this repository:
   ```bash
   git clone https://github.com/tomorgan/winter-for-windows.git
   cd winter-for-windows
   ```

2. Build the project:
   ```bash
   cd WinterForWindows
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

#### Creating a Single-File Executable

To create a standalone executable:
```bash
cd WinterForWindows
dotnet publish -c Release -r win-x64 --self-contained
```

The executable will be in `bin\Release\net8.0-windows\win-x64\publish\`

## Usage

1. Launch `WinterForWindows.exe`
2. The application will appear in your system tray (notification area)
3. Right-click the tray icon to access the menu:
   - ✅ **Fairy Lights** - Toggle colorful twinkling lights across the top of your screen
   - ✅ **Snow** - Toggle gentle snowfall effect
   - ✅ **Penguin** - Toggle the animated penguin walking along your taskbar
   - ✅ **Christmas Countdown** - Toggle the festive countdown widget
   - ❌ **Exit** - Close the application

### Christmas Countdown Widget

The countdown window is draggable - simply click and drag it to reposition anywhere on your screen. It will:
- Show the number of days until the next December 25th
- Update automatically every hour
- Display a special "Merry Christmas!" message on Christmas Day
- Gently sparkle every few seconds for that extra festive touch

### Fairy Lights Details

The fairy lights feature creates a beautiful string of Christmas lights that:
- Displays 40 colorful bulbs in red, green, blue, yellow, orange, purple, pink, and cyan
- Each bulb twinkles independently with varying patterns
- Bulbs gently sway in the breeze
- Features realistic lighting effects with glows and gradients
- Follows a gentle curved wire across the top of your screen

### Snow Details

The snow effect provides:
- Realistic snowflakes that fall at different speeds
- Natural drifting motion with random patterns
- Snowflakes that accumulate at the bottom of the screen
- Physics-based rotation and movement
- Performance-optimized particle system

### Penguin Details

The animated penguin:
- Walks back and forth along the taskbar area
- Randomly performs different behaviors (waving, sliding, jumping, standing)
- Faces the direction it's walking
- Wraps around screen edges seamlessly
- Has a charming wobble when walking

## Technical Details

### Architecture

- **Framework**: .NET 8.0 WPF (Windows Presentation Foundation)
- **Language**: C#
- **UI**: XAML with transparent window overlays
- **Settings**: JSON-based persistent storage in `%AppData%\WinterForWindows\settings.json`

### Key Features Implementation

- **Transparent Overlays**: Uses WPF window transparency with click-through capability
- **Animation System**: DispatcherTimer-based animations for smooth 30 FPS rendering
- **Particle Physics**: Custom physics engine for snowfall and movement
- **System Tray**: Hardcodet.NotifyIcon.Wpf for professional tray integration
- **Multi-Monitor**: Automatically detects and uses primary screen dimensions

### Dependencies

- **Hardcodet.NotifyIcon.Wpf** 2.0.1 - System tray integration

## Development

### Project Structure

```
winter-for-windows/
├── WinterForWindows/
│   ├── Services/
│   │   ├── EffectManager.cs      - Manages all overlay windows
│   │   └── SettingsService.cs    - Handles persistent settings
│   ├── Overlays/
│   │   ├── OverlayWindowBase.cs        - Base class for transparent overlays
│   │   ├── FairyLightsWindow.xaml/cs   - Fairy lights implementation
│   │   ├── SnowWindow.xaml/cs          - Snow particle system
│   │   ├── PenguinWindow.xaml/cs       - Penguin animation
│   │   └── ChristmasCountdownWindow.xaml/cs - Countdown widget
│   ├── App.xaml              - Application entry point with tray icon
│   ├── App.xaml.cs           - Application startup and menu handling
│   └── WinterForWindows.csproj
└── README.md
```

### Building

Requires:
- Visual Studio 2022 or later (with .NET desktop development workload)
- Or .NET 8.0 SDK for command-line builds

## Settings Storage

Settings are automatically saved to:
```
%AppData%\WinterForWindows\settings.json
```

This includes:
- Fairy Lights enabled/disabled state
- Snow enabled/disabled state
- Penguin enabled/disabled state
- Christmas Countdown enabled/disabled state

## Version History

### v0.1.0 - Initial Release
- Fairy Lights effect with twinkling and swaying animations
- Snow particle system with realistic physics
- Animated penguin character with multiple behaviors
- Christmas Day countdown widget
- System tray integration
- Persistent settings storage
- Click-through transparent overlays

## Future Ideas

- Additional effects (reindeer, gingerbread people, presents)
- Customizable colors for lights
- Snow intensity settings
- Sound effects (optional)
- Holiday themes beyond Christmas (Hanukkah, Kwanzaa, New Year's)
- Desktop widget customization

## License

This project is provided as-is for personal and educational use.

## Contributing

Contributions are welcome! Feel free to:
- Report bugs via GitHub Issues
- Suggest new festive features
- Submit pull requests

## Acknowledgments

Inspired by the macOS application **Festivitas**. Built with love to spread holiday cheer to Windows users everywhere! 🎄❄️

---

**Note**: This application is designed for Windows only and requires the .NET 8.0 runtime (included in single-file executables).


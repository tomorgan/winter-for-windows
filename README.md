# Winter for Windows

A whimsical WPF utility application that brings festive cheer to your Windows desktop during the Christmas season!

## Features

Winter for Windows lives in your system tray and offers three delightful festive overlays:

- **🎄 Fairy Lights** - Colorful lights strung across the top of your screen
- **❄️ Snow** - Gentle snowfall across your desktop that banks against the taskbar
- **🐧 Penguin** - A playful cartoon penguin that walks along your taskbar

## Current Status

**Phase 1 Complete: Foundation** ✅
- System tray integration with context menu
- Base overlay window architecture (transparent, click-through, topmost)
- Effect manager for toggling overlays
- Updatum integration prepared (placeholder)
- Project structure with Services and Overlays

**Next Steps:**
- Phase 2: Implement Fairy Lights effect
- Phase 3: Implement Snow particle system
- Phase 4: Implement Penguin animation
- Phase 5: Polish and settings

## Technical Details

- **Framework**: .NET 8 WPF
- **Dependencies**:
  - Hardcodet.NotifyIcon.Wpf 2.0.1 (System tray)
  - Updatum 1.2.0 (Auto-updates)
- **Version**: 0.1.0

## Project Structure

```
WinterForWindows/
├── Services/
│   ├── EffectManager.cs      - Manages overlay windows
│   └── UpdateManager.cs      - Handles auto-updates
├── Overlays/
│   ├── OverlayWindowBase.cs  - Base class for transparent overlays
│   ├── FairyLightsWindow.cs  - Fairy lights effect (TODO)
│   ├── SnowWindow.cs         - Snow effect (TODO)
│   └── PenguinWindow.cs      - Penguin animation (TODO)
├── App.xaml                  - Application with system tray icon
└── App.xaml.cs              - Application startup and menu handling
```

## Building

```bash
dotnet build
```

## Running

```bash
dotnet run
```

The app will appear in your system tray. Right-click the icon to access the menu and toggle effects.

## Development Roadmap

### Phase 2: Fairy Lights
- Design light bulb graphics
- Implement string layout across screen top
- Add twinkling animation
- Add gentle swaying motion

### Phase 3: Snow Effect
- Particle system for snowflakes
- Physics (falling, drifting, rotation)
- Taskbar collision detection
- Performance optimization

### Phase 4: Penguin
- Create penguin sprite/graphics
- Walking animation
- Behavior system (random actions)
- Taskbar navigation

### Phase 5: Polish
- Settings dialog
- Save preferences
- Startup with Windows option
- Proper icon
- Complete Updatum integration

## License

TBD

## Credits

Inspired by the macOS application Festivitas.

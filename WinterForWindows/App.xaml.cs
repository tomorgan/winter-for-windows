using System.Reflection;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using WinterForWindows.Services;

namespace WinterForWindows;

public partial class App : Application
{
    private TaskbarIcon? _notifyIcon;
    private EffectManager? _effectManager;
    private UpdateManager? _updateManager;
    private SettingsService? _settingsService;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
        _effectManager = new EffectManager();
        _updateManager = new UpdateManager();
        _settingsService = new SettingsService();

        SetupMenuHandlers();
        SetVersionInTitle();
        RestoreSettings();
        
        _ = _updateManager.CheckForUpdatesAsync();
    }

    private void SetVersionInTitle()
    {
        if (_notifyIcon?.ContextMenu == null) return;

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        var versionString = version != null ? $"v{version.Major}.{version.Minor}.{version.Build}" : "v0.0.0";
        
        var menuTitle = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[0];
        menuTitle.Header = $"🎄 Winter for Windows {versionString}";
    }

    private void SetupMenuHandlers()
    {
        if (_notifyIcon?.ContextMenu == null) return;

        var menuFairyLights = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[2];
        var menuSnow = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[3];
        var menuPenguin = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[4];
        var menuCountdown = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[5];
        var menuCheckUpdates = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[7];
        var menuExit = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[8];

        menuFairyLights.Click += (s, e) => {
            _effectManager?.ToggleFairyLights(menuFairyLights.IsChecked);
            SaveSettings();
        };
        menuSnow.Click += (s, e) => {
            _effectManager?.ToggleSnow(menuSnow.IsChecked);
            SaveSettings();
        };
        menuPenguin.Click += (s, e) => {
            _effectManager?.TogglePenguin(menuPenguin.IsChecked);
            SaveSettings();
        };
        menuCountdown.Click += (s, e) => {
            _effectManager?.ToggleCountdown(menuCountdown.IsChecked);
            SaveSettings();
        };
        menuCheckUpdates.Click += async (s, e) => await _updateManager!.CheckForUpdatesAsync(true);
        menuExit.Click += OnExit;
    }

    private void RestoreSettings()
    {
        if (_notifyIcon?.ContextMenu == null || _settingsService == null) return;

        var menuFairyLights = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[2];
        var menuSnow = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[3];
        var menuPenguin = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[4];
        var menuCountdown = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[5];

        menuFairyLights.IsChecked = _settingsService.Settings.FairyLightsEnabled;
        menuSnow.IsChecked = _settingsService.Settings.SnowEnabled;
        menuPenguin.IsChecked = _settingsService.Settings.PenguinEnabled;
        menuCountdown.IsChecked = _settingsService.Settings.CountdownEnabled;

        _effectManager?.ToggleFairyLights(_settingsService.Settings.FairyLightsEnabled);
        _effectManager?.ToggleSnow(_settingsService.Settings.SnowEnabled);
        _effectManager?.TogglePenguin(_settingsService.Settings.PenguinEnabled);
        _effectManager?.ToggleCountdown(_settingsService.Settings.CountdownEnabled);
    }

    private void SaveSettings()
    {
        if (_notifyIcon?.ContextMenu == null || _settingsService == null) return;

        var menuFairyLights = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[2];
        var menuSnow = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[3];
        var menuPenguin = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[4];
        var menuCountdown = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[5];

        _settingsService.Settings.FairyLightsEnabled = menuFairyLights.IsChecked;
        _settingsService.Settings.SnowEnabled = menuSnow.IsChecked;
        _settingsService.Settings.PenguinEnabled = menuPenguin.IsChecked;
        _settingsService.Settings.CountdownEnabled = menuCountdown.IsChecked;

        _settingsService.Save();
    }

    private void OnExit(object sender, RoutedEventArgs e)
    {
        _effectManager?.Dispose();
        _notifyIcon?.Dispose();
        Shutdown();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _effectManager?.Dispose();
        _notifyIcon?.Dispose();
        base.OnExit(e);
    }
}


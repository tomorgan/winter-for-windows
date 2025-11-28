using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using WinterForWindows.Services;

namespace WinterForWindows;

public partial class App : Application
{
    private TaskbarIcon? _notifyIcon;
    private EffectManager? _effectManager;
    private UpdateManager? _updateManager;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
        _effectManager = new EffectManager();
        _updateManager = new UpdateManager();

        SetupMenuHandlers();
        
        _ = _updateManager.CheckForUpdatesAsync();
    }

    private void SetupMenuHandlers()
    {
        if (_notifyIcon?.ContextMenu == null) return;

        var menuFairyLights = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[2];
        var menuSnow = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[3];
        var menuPenguin = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[4];
        var menuCheckUpdates = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[6];
        var menuExit = (System.Windows.Controls.MenuItem)_notifyIcon.ContextMenu.Items[7];

        menuFairyLights.Click += (s, e) => _effectManager?.ToggleFairyLights(menuFairyLights.IsChecked);
        menuSnow.Click += (s, e) => _effectManager?.ToggleSnow(menuSnow.IsChecked);
        menuPenguin.Click += (s, e) => _effectManager?.TogglePenguin(menuPenguin.IsChecked);
        menuCheckUpdates.Click += async (s, e) => await _updateManager!.CheckForUpdatesAsync(true);
        menuExit.Click += OnExit;
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


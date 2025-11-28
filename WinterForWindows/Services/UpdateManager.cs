using System.ComponentModel;
using System.Windows;
using Updatum;

namespace WinterForWindows.Services;

public class UpdateManager
{
    private readonly UpdatumManager _updater;
    private UpdatumDownloadedAsset? _downloadedAsset;

    public UpdateManager()
    {
        _updater = new UpdatumManager("tomorgan", "winter-for-windows");
        _updater.PropertyChanged += OnUpdaterPropertyChanged;
    }

    private void OnUpdaterPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(UpdatumManager.DownloadedPercentage))
        {
            System.Diagnostics.Debug.WriteLine($"Downloaded: {_updater.DownloadedMegabytes} MB / {_updater.DownloadSizeMegabytes} MB ({_updater.DownloadedPercentage}%)");
        }
    }

    public async Task CheckForUpdatesAsync(bool showNoUpdateMessage = false)
    {
        try
        {
            var updateFound = await _updater.CheckForUpdatesAsync();

            if (updateFound)
            {
                await ShowUpdateDialogAsync();
            }
            else if (showNoUpdateMessage)
            {
                ShowMessageBox(
                    "You are running the latest version.",
                    "No Updates Available",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            if (showNoUpdateMessage)
            {
                ShowMessageBox(
                    $"Failed to check for updates:\n{ex.Message}",
                    "Update Check Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }

    private async Task ShowUpdateDialogAsync()
    {
        var changelog = _updater.GetChangelog(1);
        var message = $"A new version ({_updater.LatestReleaseTagVersionStr}) is available!\n\n" +
                      $"Current version: {_updater.CurrentVersion}\n\n" +
                      $"Changes:\n{changelog}\n\n" +
                      $"Would you like to download and install the update?";

        var result = ShowMessageBox(
            message,
            "Update Available",
            MessageBoxButton.YesNo,
            MessageBoxImage.Information);

        if (result == MessageBoxResult.Yes)
        {
            await DownloadAndInstallUpdateAsync();
        }
    }

    private async Task DownloadAndInstallUpdateAsync()
    {
        try
        {
            _downloadedAsset = await _updater.DownloadUpdateAsync();

            if (_downloadedAsset == null)
            {
                ShowMessageBox(
                    "Failed to download the update.",
                    "Download Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var installResult = ShowMessageBox(
                "Update downloaded successfully. Install now?",
                "Install Update",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (installResult == MessageBoxResult.Yes)
            {
                await _updater.InstallUpdateAsync(_downloadedAsset);
            }
        }
        catch (Exception ex)
        {
            ShowMessageBox(
                $"Failed to download or install update:\n{ex.Message}",
                "Update Failed",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private MessageBoxResult ShowMessageBox(string message, string title, MessageBoxButton button, MessageBoxImage icon)
    {
        var helperWindow = new Window
        {
            Width = 0,
            Height = 0,
            WindowStyle = WindowStyle.None,
            ShowInTaskbar = false,
            Topmost = true,
            Left = -10000,
            Top = -10000
        };

        helperWindow.Show();
        var result = MessageBox.Show(helperWindow, message, title, button, icon);
        helperWindow.Close();

        return result;
    }
}

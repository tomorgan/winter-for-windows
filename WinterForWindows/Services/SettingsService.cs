using System.IO;
using System.Text.Json;

namespace WinterForWindows.Services;

public class SettingsService
{
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "WinterForWindows",
        "settings.json");

    public UserSettings Settings { get; private set; } = new();

    public SettingsService()
    {
        Load();
    }

    public void Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var json = File.ReadAllText(SettingsPath);
                Settings = JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
            }
        }
        catch
        {
            Settings = new UserSettings();
        }
    }

    public void Save()
    {
        try
        {
            var directory = Path.GetDirectoryName(SettingsPath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsPath, json);
        }
        catch
        {
            // Silently fail - settings are not critical
        }
    }
}

public class UserSettings
{
    public bool FairyLightsEnabled { get; set; }
    public bool SnowEnabled { get; set; }
    public bool PenguinEnabled { get; set; }
    public bool CountdownEnabled { get; set; }
}

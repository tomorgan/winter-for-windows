using WinterForWindows.Overlays;

namespace WinterForWindows.Services;

public class EffectManager : IDisposable
{
    private FairyLightsWindow? _fairyLightsWindow;
    private SnowWindow? _snowWindow;
    private PenguinWindow? _penguinWindow;

    public void ToggleFairyLights(bool enabled)
    {
        if (enabled)
        {
            _fairyLightsWindow ??= new FairyLightsWindow();
            _fairyLightsWindow.Show();
        }
        else
        {
            _fairyLightsWindow?.Hide();
        }
    }

    public void ToggleSnow(bool enabled)
    {
        if (enabled)
        {
            _snowWindow ??= new SnowWindow();
            _snowWindow.Show();
        }
        else
        {
            _snowWindow?.Hide();
        }
    }

    public void TogglePenguin(bool enabled)
    {
        if (enabled)
        {
            try
            {
                _penguinWindow ??= new PenguinWindow();
                _penguinWindow.Show();
                System.Windows.MessageBox.Show(
                    $"Penguin window created!\n" +
                    $"Left: {_penguinWindow.Left}\n" +
                    $"Top: {_penguinWindow.Top}\n" +
                    $"Width: {_penguinWindow.Width}\n" +
                    $"Height: {_penguinWindow.Height}\n" +
                    $"IsVisible: {_penguinWindow.IsVisible}\n" +
                    $"Topmost: {_penguinWindow.Topmost}",
                    "Debug");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error creating penguin: {ex.Message}", "Error");
            }
        }
        else
        {
            _penguinWindow?.Hide();
        }
    }

    public void Dispose()
    {
        _fairyLightsWindow?.Close();
        _snowWindow?.Close();
        _penguinWindow?.Close();
    }
}

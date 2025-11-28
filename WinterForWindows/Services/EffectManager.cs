using WinterForWindows.Overlays;

namespace WinterForWindows.Services;

public class EffectManager : IDisposable
{
    private FairyLightsWindow? _fairyLightsWindow;
    private SnowWindow? _snowWindow;
    private PenguinWindow? _penguinWindow;
    private ChristmasCountdownWindow? _countdownWindow;

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
            _penguinWindow ??= new PenguinWindow();
            _penguinWindow.Show();
            _penguinWindow.Activate();
        }
        else
        {
            _penguinWindow?.Hide();
        }
    }

    public void ToggleCountdown(bool enabled)
    {
        if (enabled)
        {
            _countdownWindow ??= new ChristmasCountdownWindow();
            _countdownWindow.Show();
            _countdownWindow.Activate();
        }
        else
        {
            _countdownWindow?.Hide();
        }
    }

    public void Dispose()
    {
        _fairyLightsWindow?.Close();
        _snowWindow?.Close();
        _penguinWindow?.Close();
        _countdownWindow?.Close();
    }
}

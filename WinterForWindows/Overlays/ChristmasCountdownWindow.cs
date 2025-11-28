using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WinterForWindows.Overlays;

public partial class ChristmasCountdownWindow : Window
{
    private readonly DispatcherTimer _updateTimer;
    private readonly DispatcherTimer _sparkleTimer;

    public ChristmasCountdownWindow()
    {
        InitializeComponent();
        
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.NoResize;
        ShowInTaskbar = false;
        Topmost = true;
        AllowsTransparency = true;
        Background = System.Windows.Media.Brushes.Transparent;
        
        SizeToContent = SizeToContent.WidthAndHeight;
        
        var screenWidth = SystemParameters.PrimaryScreenWidth;
        var screenHeight = SystemParameters.PrimaryScreenHeight;
        
        Left = screenWidth - 300;
        Top = screenHeight - 250;
        
        MouseLeftButtonDown += (s, e) => DragMove();
        
        _updateTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromHours(1)
        };
        _updateTimer.Tick += (s, e) => UpdateCountdown();
        _updateTimer.Start();
        
        _sparkleTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(3)
        };
        _sparkleTimer.Tick += (s, e) => AnimateSparkle();
        _sparkleTimer.Start();
        
        Loaded += (s, e) => 
        {
            UpdateCountdown();
            AnimateSparkle();
        };
    }

    private void UpdateCountdown()
    {
        var today = DateTime.Today;
        var thisYearChristmas = new DateTime(today.Year, 12, 25);
        var nextChristmas = thisYearChristmas;
        
        if (today > thisYearChristmas)
        {
            nextChristmas = new DateTime(today.Year + 1, 12, 25);
        }
        
        var daysUntil = (nextChristmas - today).Days;
        
        if (daysUntil == 0)
        {
            DaysText.Text = "🎅";
            DaysLabel.Text = "Merry Christmas!";
            TitleText.Text = "🎄 It's Christmas! 🎄";
        }
        else
        {
            DaysText.Text = daysUntil.ToString();
            DaysLabel.Text = daysUntil == 1 ? "day until Christmas" : "days until Christmas";
        }
    }

    private void AnimateSparkle()
    {
        var scaleAnimation = new DoubleAnimation
        {
            From = 1.0,
            To = 1.1,
            Duration = TimeSpan.FromMilliseconds(500),
            AutoReverse = true,
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        };

        var opacityAnimation = new DoubleAnimation
        {
            From = 0.6,
            To = 1.0,
            Duration = TimeSpan.FromMilliseconds(500),
            AutoReverse = true
        };

        if (CountdownBorder.Effect is System.Windows.Media.Effects.DropShadowEffect shadow)
        {
            shadow.BeginAnimation(System.Windows.Media.Effects.DropShadowEffect.OpacityProperty, opacityAnimation);
        }

        var scaleTransform = new System.Windows.Media.ScaleTransform(1, 1);
        TitleText.RenderTransform = scaleTransform;
        TitleText.RenderTransformOrigin = new Point(0.5, 0.5);
        scaleTransform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, scaleAnimation);
        scaleTransform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, scaleAnimation);
    }

    protected override void OnClosed(EventArgs e)
    {
        _updateTimer.Stop();
        _sparkleTimer.Stop();
        base.OnClosed(e);
    }
}

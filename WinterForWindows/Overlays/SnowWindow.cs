using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WinterForWindows.Overlays;

public partial class SnowWindow : OverlayWindowBase
{
    private readonly Random _random = new();
    private readonly List<Snowflake> _snowflakes = new();
    private readonly DispatcherTimer _timer;
    private const int MaxSnowflakes = 150;
    private const double TaskbarHeight = 40;

    public SnowWindow()
    {
        InitializeComponent();
        Title = "Snow Overlay";

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(30)
        };
        _timer.Tick += OnTimerTick;
        _timer.Start();

        for (int i = 0; i < MaxSnowflakes; i++)
        {
            CreateSnowflake();
        }
    }

    private void CreateSnowflake()
    {
        var size = 3 + _random.NextDouble() * 5;
        var snowflake = new Ellipse
        {
            Width = size,
            Height = size,
            Fill = new RadialGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(Color.FromArgb(200, 255, 255, 255), 0.7),
                    new GradientStop(Color.FromArgb(100, 255, 255, 255), 1.0)
                }
            },
            Opacity = 0.6 + _random.NextDouble() * 0.4
        };

        var particle = new Snowflake
        {
            Shape = snowflake,
            X = _random.NextDouble() * Width,
            Y = -20 - _random.NextDouble() * Height,
            VelocityY = 0.5 + _random.NextDouble() * 1.5,
            VelocityX = -0.3 + _random.NextDouble() * 0.6,
            Rotation = _random.NextDouble() * 360,
            RotationSpeed = -1 + _random.NextDouble() * 2
        };

        Canvas.SetLeft(snowflake, particle.X);
        Canvas.SetTop(snowflake, particle.Y);
        
        SnowCanvas.Children.Add(snowflake);
        _snowflakes.Add(particle);
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        double screenHeight = SystemParameters.VirtualScreenHeight;
        double screenWidth = SystemParameters.VirtualScreenWidth;
        double groundLevel = screenHeight - TaskbarHeight;

        foreach (var snowflake in _snowflakes)
        {
            snowflake.Y += snowflake.VelocityY;
            snowflake.X += snowflake.VelocityX + Math.Sin(snowflake.Y * 0.01) * 0.5;
            snowflake.Rotation += snowflake.RotationSpeed;

            if (snowflake.Y > groundLevel)
            {
                snowflake.Y = groundLevel;
                snowflake.VelocityY *= 0.5;
                snowflake.VelocityX *= 0.5;
                
                if (Math.Abs(snowflake.VelocityY) < 0.1)
                {
                    snowflake.VelocityY = 0;
                    snowflake.VelocityX = 0;
                }
            }

            if (snowflake.X < -10)
                snowflake.X = screenWidth + 10;
            else if (snowflake.X > screenWidth + 10)
                snowflake.X = -10;

            Canvas.SetLeft(snowflake.Shape, snowflake.X);
            Canvas.SetTop(snowflake.Shape, snowflake.Y);

            var transform = new RotateTransform(snowflake.Rotation);
            snowflake.Shape.RenderTransform = transform;
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        _timer.Stop();
        base.OnClosed(e);
    }

    private class Snowflake
    {
        public Ellipse Shape { get; set; } = null!;
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public double Rotation { get; set; }
        public double RotationSpeed { get; set; }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WinterForWindows.Overlays;

public partial class FairyLightsWindow : OverlayWindowBase
{
    private readonly Random _random = new();
    private readonly Color[] _lightColors = 
    {
        Colors.Red, Colors.Green, Colors.Blue, Colors.Yellow,
        Colors.Orange, Colors.Purple, Colors.Pink, Colors.Cyan
    };

    public FairyLightsWindow()
    {
        InitializeComponent();
        Title = "Fairy Lights Overlay";
        
        Height = 200;
        Top = 0;
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        CreateFairyLights();
    }

    private void CreateFairyLights()
    {
        int numberOfLights = 40;
        double lightSpacing = SystemParameters.VirtualScreenWidth / numberOfLights;

        var wirePath = new Path
        {
            Stroke = new SolidColorBrush(Color.FromArgb(60, 50, 50, 50)),
            StrokeThickness = 2
        };

        var wireGeometry = new PathGeometry();
        var wireFigure = new PathFigure { StartPoint = new Point(0, 30) };
        
        for (int i = 0; i <= numberOfLights; i++)
        {
            double x = i * lightSpacing;
            double y = 30 + Math.Sin(i * 0.5) * 20;
            wireFigure.Segments.Add(new LineSegment(new Point(x, y), true));
        }
        
        wireGeometry.Figures.Add(wireFigure);
        wirePath.Data = wireGeometry;
        LightsCanvas.Children.Add(wirePath);

        for (int i = 0; i < numberOfLights; i++)
        {
            double x = i * lightSpacing + lightSpacing / 2;
            double y = 30 + Math.Sin(i * 0.5) * 20;
            
            var lightBulb = CreateLightBulb(x, y, _lightColors[i % _lightColors.Length]);
            LightsCanvas.Children.Add(lightBulb);
            
            AnimateLightBulb(lightBulb, i);
        }
    }

    private Ellipse CreateLightBulb(double x, double y, Color color)
    {
        var bulb = new Ellipse
        {
            Width = 24,
            Height = 32,
            Fill = new RadialGradientBrush
            {
                GradientOrigin = new Point(0.5, 0.3),
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.6,
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb(255, 255, 255, 255), 0.0),
                    new GradientStop(Color.FromArgb(255, 
                        (byte)Math.Min(255, color.R + 100),
                        (byte)Math.Min(255, color.G + 100),
                        (byte)Math.Min(255, color.B + 100)), 0.3),
                    new GradientStop(color, 0.6),
                    new GradientStop(Color.FromRgb(
                        (byte)(color.R * 0.4),
                        (byte)(color.G * 0.4),
                        (byte)(color.B * 0.4)), 1.0)
                }
            },
            Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = color,
                BlurRadius = 25,
                ShadowDepth = 0,
                Opacity = 0.9
            }
        };

        Canvas.SetLeft(bulb, x - 12);
        Canvas.SetTop(bulb, y - 16);

        return bulb;
    }

    private void AnimateLightBulb(Ellipse bulb, int index)
    {
        // More dramatic twinkling with varied patterns
        var twinkleAnimation = new DoubleAnimation
        {
            From = 0.4,
            To = 1.0,
            Duration = TimeSpan.FromSeconds(0.8 + _random.NextDouble() * 1.2),
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever,
            BeginTime = TimeSpan.FromMilliseconds(_random.Next(0, 1500)),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
        };
        
        bulb.BeginAnimation(OpacityProperty, twinkleAnimation);

        // Add glow intensity animation
        var glowAnimation = new DoubleAnimation
        {
            From = 20,
            To = 30,
            Duration = TimeSpan.FromSeconds(1.0 + _random.NextDouble() * 1.5),
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever,
            BeginTime = TimeSpan.FromMilliseconds(_random.Next(0, 1000))
        };

        if (bulb.Effect is System.Windows.Media.Effects.DropShadowEffect shadow)
        {
            shadow.BeginAnimation(System.Windows.Media.Effects.DropShadowEffect.BlurRadiusProperty, glowAnimation);
        }

        // Subtle sway
        var swayAnimation = new DoubleAnimation
        {
            From = -2,
            To = 2,
            Duration = TimeSpan.FromSeconds(3 + _random.NextDouble() * 2),
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever,
            BeginTime = TimeSpan.FromMilliseconds(_random.Next(0, 2000))
        };

        var transform = new TranslateTransform();
        bulb.RenderTransform = transform;
        transform.BeginAnimation(TranslateTransform.XProperty, swayAnimation);
    }
}

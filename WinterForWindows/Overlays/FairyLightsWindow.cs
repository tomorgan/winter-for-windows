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
            Width = 16,
            Height = 20,
            Fill = new RadialGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.White, 0.0),
                    new GradientStop(color, 0.5),
                    new GradientStop(Color.FromRgb(
                        (byte)(color.R * 0.5),
                        (byte)(color.G * 0.5),
                        (byte)(color.B * 0.5)), 1.0)
                }
            },
            Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = color,
                BlurRadius = 15,
                ShadowDepth = 0,
                Opacity = 0.8
            }
        };

        Canvas.SetLeft(bulb, x - 8);
        Canvas.SetTop(bulb, y - 10);

        return bulb;
    }

    private void AnimateLightBulb(Ellipse bulb, int index)
    {
        var twinkleAnimation = new DoubleAnimation
        {
            From = 0.6,
            To = 1.0,
            Duration = TimeSpan.FromSeconds(1.5 + _random.NextDouble() * 1.5),
            AutoReverse = true,
            RepeatBehavior = RepeatBehavior.Forever,
            BeginTime = TimeSpan.FromMilliseconds(_random.Next(0, 1000))
        };
        
        bulb.BeginAnimation(OpacityProperty, twinkleAnimation);

        var swayAnimation = new DoubleAnimation
        {
            From = -3,
            To = 3,
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

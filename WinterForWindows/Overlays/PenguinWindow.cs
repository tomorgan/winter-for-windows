using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WinterForWindows.Overlays;

public partial class PenguinWindow : Window
{
    private readonly Random _random = new();
    private readonly DispatcherTimer _animationTimer;
    private readonly DispatcherTimer _behaviorTimer;
    private Canvas? _penguinBody;
    private double _penguinX;
    private double _penguinY;
    private double _velocityX;
    private bool _facingRight = true;
    private PenguinState _currentState = PenguinState.Walking;
    private const double PenguinSize = 40;
    private const double TaskbarHeight = 40;

    private enum PenguinState
    {
        Walking,
        Waving,
        Sliding,
        Standing
    }

    public PenguinWindow()
    {
        InitializeComponent();
        Title = "Penguin Overlay";
        
        // Make it a simple window at the bottom
        WindowStyle = WindowStyle.None;
        ResizeMode = ResizeMode.NoResize;
        ShowInTaskbar = false;
        Topmost = true;
        AllowsTransparency = true;
        Background = Brushes.Transparent;
        
        var screenHeight = SystemParameters.PrimaryScreenHeight;
        var screenWidth = SystemParameters.PrimaryScreenWidth;
        
        Width = screenWidth;
        Height = 150;
        Left = 0;
        Top = screenHeight - Height;
        
        _penguinY = 50;
        _penguinX = screenWidth / 2; // Start in middle for testing
        _velocityX = 1;

        Loaded += OnLoaded;
        
        _animationTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(30)
        };
        _animationTimer.Tick += OnAnimationTick;
        _animationTimer.Start();

        _behaviorTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(3)
        };
        _behaviorTimer.Tick += OnBehaviorTick;
        _behaviorTimer.Start();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        CreatePenguin();
    }

    private void CreatePenguin()
    {
        _penguinBody = new Canvas
        {
            Width = PenguinSize,
            Height = PenguinSize
        };

        // Body (black oval)
        var body = new Ellipse
        {
            Width = 30,
            Height = 40,
            Fill = Brushes.Black,
            Stroke = Brushes.Black,
            StrokeThickness = 1
        };
        Canvas.SetLeft(body, 5);
        Canvas.SetTop(body, 0);
        _penguinBody.Children.Add(body);

        // Belly (white oval)
        var belly = new Ellipse
        {
            Width = 20,
            Height = 30,
            Fill = Brushes.White
        };
        Canvas.SetLeft(belly, 10);
        Canvas.SetTop(belly, 8);
        _penguinBody.Children.Add(belly);

        // Left eye
        var leftEye = new Ellipse
        {
            Width = 6,
            Height = 6,
            Fill = Brushes.White
        };
        Canvas.SetLeft(leftEye, 12);
        Canvas.SetTop(leftEye, 8);
        _penguinBody.Children.Add(leftEye);

        var leftPupil = new Ellipse
        {
            Width = 3,
            Height = 3,
            Fill = Brushes.Black
        };
        Canvas.SetLeft(leftPupil, 13.5);
        Canvas.SetTop(leftPupil, 9.5);
        _penguinBody.Children.Add(leftPupil);

        // Right eye
        var rightEye = new Ellipse
        {
            Width = 6,
            Height = 6,
            Fill = Brushes.White
        };
        Canvas.SetLeft(rightEye, 22);
        Canvas.SetTop(rightEye, 8);
        _penguinBody.Children.Add(rightEye);

        var rightPupil = new Ellipse
        {
            Width = 3,
            Height = 3,
            Fill = Brushes.Black
        };
        Canvas.SetLeft(rightPupil, 23.5);
        Canvas.SetTop(rightPupil, 9.5);
        _penguinBody.Children.Add(rightPupil);

        // Beak
        var beak = new Polygon
        {
            Fill = Brushes.Orange,
            Points = new PointCollection { new Point(20, 18), new Point(25, 20), new Point(20, 22) }
        };
        _penguinBody.Children.Add(beak);

        // Left foot
        var leftFoot = new Ellipse
        {
            Width = 8,
            Height = 4,
            Fill = Brushes.Orange
        };
        Canvas.SetLeft(leftFoot, 10);
        Canvas.SetTop(leftFoot, 36);
        _penguinBody.Children.Add(leftFoot);

        // Right foot
        var rightFoot = new Ellipse
        {
            Width = 8,
            Height = 4,
            Fill = Brushes.Orange
        };
        Canvas.SetLeft(rightFoot, 22);
        Canvas.SetTop(rightFoot, 36);
        _penguinBody.Children.Add(rightFoot);

        Canvas.SetLeft(_penguinBody, _penguinX);
        Canvas.SetTop(_penguinBody, _penguinY);
        PenguinCanvas.Children.Add(_penguinBody);
    }

    private void OnAnimationTick(object? sender, EventArgs e)
    {
        if (_penguinBody == null) return;

        double screenWidth = SystemParameters.PrimaryScreenWidth;

        switch (_currentState)
        {
            case PenguinState.Walking:
                _penguinX += _velocityX;
                
                if (_penguinX < -PenguinSize)
                {
                    _penguinX = screenWidth;
                }
                else if (_penguinX > screenWidth)
                {
                    _penguinX = -PenguinSize;
                }

                var wobble = Math.Sin(DateTime.Now.Millisecond / 100.0) * 2;
                Canvas.SetLeft(_penguinBody, _penguinX);
                Canvas.SetTop(_penguinBody, _penguinY + wobble);
                break;

            case PenguinState.Sliding:
                _penguinX += _velocityX * 2;
                
                if (_penguinX < -PenguinSize || _penguinX > screenWidth)
                {
                    _currentState = PenguinState.Walking;
                    _penguinBody.RenderTransform = null;
                }
                else
                {
                    Canvas.SetLeft(_penguinBody, _penguinX);
                }
                break;
        }

        UpdateDirection();
    }

    private void OnBehaviorTick(object? sender, EventArgs e)
    {
        var action = _random.Next(0, 10);

        if (action < 5)
        {
            _currentState = PenguinState.Walking;
            _penguinBody!.RenderTransform = null;
        }
        else if (action < 7)
        {
            _currentState = PenguinState.Waving;
            AnimateWave();
        }
        else if (action < 9)
        {
            _currentState = PenguinState.Sliding;
            AnimateSlide();
        }
        else
        {
            _currentState = PenguinState.Standing;
            AnimateJump();
        }
    }

    private void AnimateWave()
    {
        var waveAnimation = new DoubleAnimation
        {
            From = 0,
            To = 15,
            Duration = TimeSpan.FromMilliseconds(300),
            AutoReverse = true,
            RepeatBehavior = new RepeatBehavior(3)
        };

        waveAnimation.Completed += (s, e) => _currentState = PenguinState.Walking;

        var transform = new RotateTransform(0, PenguinSize / 2, PenguinSize / 2);
        _penguinBody!.RenderTransform = transform;
        transform.BeginAnimation(RotateTransform.AngleProperty, waveAnimation);
    }

    private void AnimateSlide()
    {
        var slideTransform = new RotateTransform(-20, PenguinSize / 2, PenguinSize / 2);
        _penguinBody!.RenderTransform = slideTransform;
    }

    private void AnimateJump()
    {
        var jumpAnimation = new DoubleAnimation
        {
            From = _penguinY,
            To = _penguinY - 30,
            Duration = TimeSpan.FromMilliseconds(300),
            AutoReverse = true
        };

        jumpAnimation.Completed += (s, e) => _currentState = PenguinState.Walking;

        var transform = new TranslateTransform();
        _penguinBody!.RenderTransform = transform;
        transform.BeginAnimation(TranslateTransform.YProperty, jumpAnimation);
    }

    private void UpdateDirection()
    {
        if (_penguinBody == null) return;

        bool shouldFaceRight = _velocityX > 0;
        if (shouldFaceRight != _facingRight)
        {
            _facingRight = shouldFaceRight;
            var scaleTransform = new ScaleTransform(_facingRight ? 1 : -1, 1, PenguinSize / 2, 0);
            
            if (_penguinBody.RenderTransform is RotateTransform)
                return;
                
            _penguinBody.RenderTransform = scaleTransform;
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        _animationTimer.Stop();
        _behaviorTimer.Stop();
        base.OnClosed(e);
    }
}
